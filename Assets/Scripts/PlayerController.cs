using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] ParticleSystem powerupParticles;

    InputAction moveAction;
    Rigidbody2D myRigidbody2D;
    SurfaceEffector2D surfaceEffector2D;
    ScoreManager scoreManager;

    Vector2 moveVector;
    public bool canControlPlayer = true;
    float previousRotation;
    float totalRotation;
    int flipCount;
    int activePowerupCount;

    void Start()
    {   
        moveAction = InputSystem.actions.FindAction("Move");
        myRigidbody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    void Update()
    {
        if(canControlPlayer == true)
        {
        RotatePlayer();
        BoostPlayer();
        }
        CalculateFlips();

        if(totalRotation > 360)
        {
            flipCount +=1;
            totalRotation -= 360;
            scoreManager.AddScore(100);
        }
        else if(totalRotation < -360)
        {
            flipCount += 1;
            totalRotation += 360;
            scoreManager.AddScore(100);
        }
    }

    void RotatePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        if(moveVector.x < 0){
            myRigidbody2D.AddTorque(torqueAmount);
        }
        if(moveVector.x > 0){
            myRigidbody2D.AddTorque(-torqueAmount);
        }
    }

    void BoostPlayer()
    {
        if(moveVector.y > 0)
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else if(moveVector.y == 0)
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;

        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);


        previousRotation = currentRotation;
    }

    public void DisableControls()
    {
        canControlPlayer = false;
    }

    public void ActivatePowerup(PowerupSO powerup)
    {
        powerupParticles.Play();
        activePowerupCount += 1;

        if(powerup.GetPowerupType() == "speed")
        {
            baseSpeed += powerup.GetValueChange();
            boostSpeed += powerup.GetValueChange();
        }
        else if(powerup.GetPowerupType() == "torque")
        {
            torqueAmount += powerup.GetValueChange();
        }
    }

    public void DeactivatePowerup(PowerupSO powerup)
    {
        activePowerupCount -= 1;
        if(activePowerupCount == 0)
        {
            powerupParticles.Stop();
        }
        if(powerup.GetPowerupType() == "speed")
        {
            baseSpeed -= powerup.GetValueChange();
            boostSpeed -= powerup.GetValueChange();
        }
        else if(powerup.GetPowerupType() == "torque")
        {
            torqueAmount -= powerup.GetValueChange();
        }
    }
}

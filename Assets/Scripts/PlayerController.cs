using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;

    InputAction moveAction;
    Rigidbody2D myRigidbody2D;
    SurfaceEffector2D surfaceEffector2D;

    Vector2 moveVector;

    void Start()
    {   
        moveAction = InputSystem.actions.FindAction("Move");
        myRigidbody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        RotatePlayer();
        BoostPlayer();
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
            Debug.Log("y > 0");
            surfaceEffector2D.speed = boostSpeed;
            Debug.Log(surfaceEffector2D.speed);
        }
        else if(moveVector.y == 0)
        {
            Debug.Log("y = 0");
            surfaceEffector2D.speed = baseSpeed;
            Debug.Log(surfaceEffector2D.speed);
        }
    }
}

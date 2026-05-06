using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float restartDelay = 2f;
    [SerializeField] ParticleSystem crashParticles;
    PlayerController playerController;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if(collision.gameObject.layer == layerIndex)
        {
            playerController.DisableControls();
            Debug.Log("Player has lost!");
            crashParticles.Play();
            Invoke("ReloadScene",restartDelay);
        }
    }


    void ReloadScene(){
        SceneManager.LoadScene(0);
    }
}

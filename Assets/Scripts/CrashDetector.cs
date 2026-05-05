using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float restartDelay = 2f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer == layerIndex)
        {
            Debug.Log("Player has lost!");
            Invoke("ReloadScene",restartDelay);
        }
    }
    void ReloadScene(){
        SceneManager.LoadScene(0);
    }
}

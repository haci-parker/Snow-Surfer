using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float restartDelay = 1f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");
        if(collision.gameObject.layer == layerIndex)
        {
            Debug.Log("The player has won!");
            Invoke("ReloadScene",restartDelay);
        }
    }
    void ReloadScene(){
        SceneManager.LoadScene(0);
    }
}

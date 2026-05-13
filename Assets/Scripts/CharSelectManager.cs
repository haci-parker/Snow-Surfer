using UnityEngine;

public class CharSelectManager : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;
    }

    public void BeginGame()
    {
        Time.timeScale = 1f;
    }
}

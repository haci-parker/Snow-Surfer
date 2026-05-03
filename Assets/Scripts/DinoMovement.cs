using UnityEngine;
using UnityEngine.InputSystem;

public class DinoMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;

    void Update()
    {
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Keyboard.current.spaceKey.isPressed)
        {
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
        }
    }

}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    InputAction moveAction;
    Rigidbody2D myRigidbody2D;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 moveVector;
        moveVector = moveAction.ReadValue<Vector2>();
        if(moveVector.x < 0){
            myRigidbody2D.AddTorque(torqueAmount);
        }
        if(moveVector.x > 0){
            myRigidbody2D.AddTorque(-torqueAmount);
        }
    }
}

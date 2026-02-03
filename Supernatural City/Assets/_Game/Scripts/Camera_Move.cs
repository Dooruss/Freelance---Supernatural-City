using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_Move : MonoBehaviour
{
    private Vector3 movement;
    private float Speed = 2f;

    public void Moves(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        movement.x = context.ReadValue<Vector2>().x;
        movement.y = context.ReadValue<Vector2>().y;
    }

    private void Update()
    {
        transform.Translate(movement * Speed * Time.deltaTime);
    }
}

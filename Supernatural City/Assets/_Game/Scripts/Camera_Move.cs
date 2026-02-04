using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Camera_Move : MonoBehaviour
{
    //movement
    private Vector3 movement;
    private float Speed = 7f;

    //zoom
    // Somewhat from https://www.youtube.com/watch?v=Iz9AMF2dZdw 
    private Camera Camera;
    private float ZoomTarget;
    [SerializeField] private float Multiplier = 2f;
    [SerializeField] private float MinZoom = 1f;
    [SerializeField] private float MaxZoom = 10f;
    [SerializeField] private float SmoothTime = 1f;
    [SerializeField] private float Velocity = 0f;

    private void Start()
    {
        Camera = GetComponent<Camera>();
        ZoomTarget = Camera.orthographicSize;
    }

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

    public void Scroll(InputAction.CallbackContext context)
    {
        ZoomTarget -= context.ReadValue<Vector2>().y * Multiplier;
        ZoomTarget = Mathf.Clamp(ZoomTarget, MinZoom, MaxZoom);
        Camera.orthographicSize = Mathf.SmoothDamp(Camera.orthographicSize, ZoomTarget, ref Velocity, SmoothTime);
    }
}

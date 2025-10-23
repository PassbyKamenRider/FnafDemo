using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [SerializeField] InputActionReference lookAction;
    [SerializeField] InputActionReference turnBackAction;
    [SerializeField] float sensitivity = 100f;
    [SerializeField] float maxAngle = 45f;
    [SerializeField] float turnSpeed = 5f;

    [HideInInspector] public bool canRotate = true;
    private float yaw = 0f;
    private bool isLookingBack = false;
    private float targetYaw = 0f;

    void OnEnable()
    {
        lookAction.action.Enable();
        turnBackAction.action.Enable();
    }

    void OnDisable()
    {
        lookAction.action.Disable();
        turnBackAction.action.Disable();
    }

    void Update()
    {
        if (!canRotate) return;

        bool turnPressed = turnBackAction.action.IsPressed();

        if (turnPressed && !isLookingBack)
        {
            isLookingBack = true;
            targetYaw = 180f;
        }

        else if (!turnPressed && isLookingBack)
        {
            isLookingBack = false;
            targetYaw = Mathf.Clamp(yaw, -maxAngle, maxAngle);
        }

        if (!isLookingBack)
        {
            Vector2 look = lookAction.action.ReadValue<Vector2>();
            yaw += look.x * sensitivity * Time.deltaTime;
            yaw = Mathf.Clamp(yaw, -maxAngle, maxAngle);
            targetYaw = yaw;
        }

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            Quaternion.Euler(0f, targetYaw, 0f),
            Time.deltaTime * turnSpeed
        );
    }
}

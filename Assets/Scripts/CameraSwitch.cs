using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] CinemachineCamera playerCam;
    [SerializeField] CinemachineCamera targetCam;
    [SerializeField] InputActionReference clickAction;
    private bool isOnTarget = false;
    private CameraLook cameraLook;

    void OnEnable()
    {
        clickAction.action.Enable();
    }

    void OnDisable()
    {
        clickAction.action.Disable();
    }

    void Start()
    {
        cameraLook = playerCam.GetComponent<CameraLook>();
    }

    void Update()
    {
        if (clickAction.action.WasPressedThisFrame())
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, 10f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ToggleView();
                }
            }
        }
    }

    void ToggleView()
    {
        isOnTarget = !isOnTarget;

        if (isOnTarget)
        {
            playerCam.Priority = 0;
            targetCam.Priority = 10;
            cameraLook.canRotate = false;
        }
        else
        {
            playerCam.Priority = 10;
            targetCam.Priority = 0;
            cameraLook.canRotate = true;
        }
    }
}
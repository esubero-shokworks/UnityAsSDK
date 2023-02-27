using UnityEngine;

public class TestTouch : MonoBehaviour
{
    private InputController inputController;
    private Camera cameraMain;

    private void Awake()
    {
        inputController = InputController.Instance;
        cameraMain = Camera.main;
    }

    private void OnEnable()
    {
        inputController.OnStartTouch += Move;
    }

    private void OnDisable()
    {
        inputController.OnStartTouch -= Move;
    }

    private void Move(Vector2 screenPosition)
    {
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, cameraMain.nearClipPlane);
        Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;
        transform.position = worldCoordinates;
    }
}

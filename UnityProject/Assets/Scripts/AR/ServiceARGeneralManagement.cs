using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ServiceARGeneralManagement : MonoBehaviour, IARGeneralManagementService
{
    [SerializeField] private ARCameraManager cameraManager;
    private CameraFacingDirection currentFacingDirection = CameraFacingDirection.World;

    public void ChangeARCamera()
    {
        try
        {
            if (currentFacingDirection == CameraFacingDirection.World)
            {
                currentFacingDirection = CameraFacingDirection.User;
            }
            else if (currentFacingDirection == CameraFacingDirection.User)
            {
                currentFacingDirection = CameraFacingDirection.World;
            }

            cameraManager.requestedFacingDirection = currentFacingDirection;
            string uiState = $"The Camera is now looking to {currentFacingDirection} direction";

            ServiceLocator.Instance.GetService<IUIService>().UpdateStatusLabel(uiState);
        }
        catch (Exception thrownException)
        {
            ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"ServiceARGeneralManagement: {thrownException.Message}");
            throw;
        }
    }
}

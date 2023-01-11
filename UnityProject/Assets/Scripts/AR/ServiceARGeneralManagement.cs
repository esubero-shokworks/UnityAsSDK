using System;
using UnityEditor;
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
            //TODO: Need more testing with a new phone
            cameraManager.requestedFacingDirection = currentFacingDirection;
            string uiState = $"The Camera is now looking to {currentFacingDirection} direction";
            Utils.UpdateUIState(uiState);
        }
        catch (Exception thrownException)
        {
            Utils.CommunicateErrorWithNative(thrownException, this);
            throw;
        }
    }
}

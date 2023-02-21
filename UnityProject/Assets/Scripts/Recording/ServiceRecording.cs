using System;
using UnityEngine;

public class ServiceRecording : MonoBehaviour, IRecordingService
{
    public void StartCameraRecording()
    {
        try
        {
            string uiState = $"Called to StartCameraRecording";
            ServiceLocator.Instance.GetService<IUIService>().UpdateStatusLabel(uiState);
        }
        catch (Exception thrownException)
        {
            ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"ServiceRecording: {thrownException.Message}");
            throw;
        }
    }

    public void StopCameraRecording()
    {
        try
        {
            string uiState = $"Called to StopCameraRecording";
            ServiceLocator.Instance.GetService<IUIService>().UpdateStatusLabel(uiState);
        }
        catch (Exception thrownException)
        {
            ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"ServiceRecording: {thrownException.Message}");
            throw;
        }
    }
}

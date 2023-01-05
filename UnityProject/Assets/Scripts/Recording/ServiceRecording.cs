using UnityEngine;

public class ServiceRecording : MonoBehaviour, IRecordingService
{
    public void StartCameraRecording()
    {
        string uiState = $"Called to StartCameraRecording";
        Utils.UpdateUIState(uiState);
    }

    public void StopCameraRecording()
    {
        string uiState = $"Called to StopCameraRecording";
        Utils.UpdateUIState(uiState);
    }
}

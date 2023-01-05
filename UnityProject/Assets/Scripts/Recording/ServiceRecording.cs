using UnityEngine;

public class ServiceRecording : MonoBehaviour, IRecordingService
{
    public void StartCameraRecording()
    {
        string uiState = $"Called to StartCameraRecording";
        ServiceLocator.Instance.GetService<IUIService>().UpdateState(uiState);
    }

    public void StopCameraRecording()
    {
        string uiState = $"Called to StopCameraRecording";
        ServiceLocator.Instance.GetService<IUIService>().UpdateState(uiState);
    }
}

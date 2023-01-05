using UnityEngine;

public class ServiceRecordingInstaller : MonoBehaviour
{
    [SerializeField] private ServiceRecording recordingService;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IRecordingService>(recordingService);
    }
}

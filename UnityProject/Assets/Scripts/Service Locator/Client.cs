using UnityEngine;

public class Client : MonoBehaviour
{
    public void DoChangeColor(string newColor)
    {
        Debug.Log("Receiving the ChangeColor command");
        ServiceLocator.Instance.GetService<ICubeService>().ChangeColor(newColor);
    }

    public void DoInstantiateARObject(string pathObjectToInstantiate)
    {
        Debug.Log("Receiving the InstantiateARObject command");
        ServiceLocator.Instance.GetService<IARObjectManagementService>().InstantiateARObject(pathObjectToInstantiate);
    }

    public void DoDestroyARObject()
    {
        Debug.Log("Receiving the DestroyARObject command");
        ServiceLocator.Instance.GetService<IARObjectManagementService>().DestroyARObject();
    }

    public void DoChangeARCamera()
    {
        Debug.Log("Receiving the ChangeARCamera command");
        ServiceLocator.Instance.GetService<IARGeneralManagementService>().ChangeARCamera();
    }

    public void DoStartCameraRecording()
    {
        Debug.Log("Receiving the StartCameraRecording command");
        ServiceLocator.Instance.GetService<IRecordingService>().StartCameraRecording();
    }

    public void DoStopCameraRecording()
    {
        Debug.Log("Receiving the StopCameraRecording command");
        ServiceLocator.Instance.GetService<IRecordingService>().StopCameraRecording();
    }

}

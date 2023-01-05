using UnityEngine;

public class Client : MonoBehaviour
{
    public void DoChangeColor(string newColor)
    {
        Debug.Log("Receiving the ChangeColor command");
        ServiceLocator.Instance.GetService<ICubeService>().ChangeColor(newColor);
    }

    public void DoInstantiateARObject(string objectNameToInstantiate)
    {
        Debug.Log("Receiving the InstantiateARObject command");
        ServiceLocator.Instance.GetService<IARService>().InstantiateARObject(objectNameToInstantiate);
    }

    public void DoDestroyARObject(string objectNameToDestroy)
    {
        Debug.Log("Receiving the DestroyARObject command");
        ServiceLocator.Instance.GetService<IARService>().DestroyARObject(objectNameToDestroy);
    }

    public void DoChangeARCamera(string targetCamera) //maybe an internal switch
    {
        Debug.Log("Receiving the ChangeARCamera command");
        ServiceLocator.Instance.GetService<IARService>().ChangeARCamera(targetCamera);
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

using UnityEngine;

public class ServiceAR : MonoBehaviour, IARService
{
    public void ChangeARCamera(string targetCamera)
    {
        string uiState = $"Called to ChangeARCamera with the attribute {targetCamera}";
        ServiceLocator.Instance.GetService<IUIService>().UpdateState(uiState);
    }

    public void DestroyARObject(string objectNameToDestroy)
    {
        string uiState = $"Called to DestroyARObject with the attribute {objectNameToDestroy}";
        ServiceLocator.Instance.GetService<IUIService>().UpdateState(uiState);
    }

    public void InstantiateARObject(string objectNameToInstantiate)
    {
        string uiState = $"Called to InstantiateARObject with the attribute {objectNameToInstantiate}";
        ServiceLocator.Instance.GetService<IUIService>().UpdateState(uiState);
    }
}

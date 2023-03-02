using UnityEngine;

public class TestUIPanelBehaviour : MonoBehaviour
{
    private bool isLoadingActivated;
    private bool isDownloadActivated;
    private bool isPutObjectActivated;

    public void ShowLoadingGizmo()
    {
        isLoadingActivated = !isLoadingActivated;
        ServiceLocator.Instance.GetService<IUIService>().LoadingGizmoActivation(isLoadingActivated);
    }

    public void ShowDownloadGizmo()
    {
        isDownloadActivated = !isDownloadActivated;
        ServiceLocator.Instance.GetService<IUIService>().DownloadGizmoActivation(isDownloadActivated);
    }

    public void ShowPutObjectGizmo()
    {
        isPutObjectActivated = !isPutObjectActivated;
        ServiceLocator.Instance.GetService<IUIService>().PutObjectGizmoActivation(isPutObjectActivated);
    }
}

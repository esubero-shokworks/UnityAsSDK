internal interface IUIService
{
    void UpdateStatusLabel(string currentState);
    void LoadingGizmoActivation(bool imageSwitch);
    void DownloadGizmoActivation(bool isDownloading);
}

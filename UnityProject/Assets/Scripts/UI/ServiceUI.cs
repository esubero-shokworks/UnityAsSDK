using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServiceUI : MonoBehaviour, IUIService
{
    [SerializeField] private TMP_Text stateLabel;
    [Header("Loading Gizmo")]
    [SerializeField] private Image loadingImage;
    [Header("Download Gizmo")]
    [SerializeField] private Image downloadImage;
    [SerializeField] private GameObject downloadImageParent;
    [Header("Put Object Gizmo")]
    [SerializeField] private Image putObjectImage;

    private void Awake()
    {
        LoadingGizmoActivation(false);
        DownloadGizmoActivation(false);
        PutObjectGizmoActivation(false);
    }

    public void UpdateStatusLabel(string currentState)
    {
        stateLabel.text = currentState;
    }

    public void PutObjectGizmoActivation(bool isActive)
    {
        if (putObjectImage.enabled == isActive)
        {
            return;
        }

        putObjectImage.enabled = isActive;

        if (isActive)
        {
            LeanTween.scale(putObjectImage.gameObject, Vector3.one / 1.5f, 0.8f).setEase(LeanTweenType.easeOutSine).setLoopPingPong();
        }
    }

    public void LoadingGizmoActivation(bool imageSwitch)
    {
        if (loadingImage.enabled == imageSwitch)
        {
            return;
        }

        loadingImage.enabled = imageSwitch;

        if (imageSwitch)
        {
            float targetDestination = loadingImage.transform.position.x + 55;
            LeanTween.moveX(loadingImage.gameObject, targetDestination, 1f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        }
    }

    public void DownloadGizmoActivation(bool isDownloading)
    {
        if (downloadImageParent.activeInHierarchy == isDownloading)
        {
            return;
        }

        downloadImageParent.SetActive(isDownloading);

        if (isDownloading)
        {
            LeanTween.value(downloadImage.gameObject, UpdateDownloadGizmo, 0f, 1f, 1f).setLoopType(LeanTweenType.easeInOutSine);
        }
    }

    private void UpdateDownloadGizmo(float newValue)
    {
        downloadImage.fillAmount = newValue;
    }
}

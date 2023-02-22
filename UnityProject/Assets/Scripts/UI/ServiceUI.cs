using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServiceUI : MonoBehaviour, IUIService
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private Image downloadImage;
    [SerializeField] private GameObject downloadImageParent;
    [SerializeField] private TMP_Text stateLabel;

    private void Awake()
    {
        LoadingGizmoActivation(false);
        DownloadGizmoActivation(false);
    }

    public void UpdateStatusLabel(string currentState)
    {
        stateLabel.text = currentState;
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
            float targetDestination = loadingImage.transform.position.x * 2;
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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServiceUI : MonoBehaviour, IUIService
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private TMP_Text stateLabel;

    private void Awake()
    {
        LoadingActivation(false);
    }

    public void UpdateStatusLabel(string currentState)
    {
        stateLabel.text = currentState;
    }

    public void LoadingActivation(bool imageSwitch)
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
}

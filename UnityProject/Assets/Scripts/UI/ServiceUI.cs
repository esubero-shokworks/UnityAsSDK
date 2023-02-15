using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServiceUI : MonoBehaviour, IUIService
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private TMP_Text stateLabel;
    private Coroutine loadingCoroutine;

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

        if (loadingCoroutine != null) 
        { 
            StopCoroutine(loadingCoroutine);
        }

        loadingImage.enabled = imageSwitch;

        if (imageSwitch)
        {
            loadingCoroutine = StartCoroutine(MoveLoadingImage());
        }
    }

    private IEnumerator MoveLoadingImage()
    {
        while (true)
        {
            yield return null;
            LeanTween.rotateAround(loadingImage.gameObject, Vector3.forward, 5f, 0.05f);
        }
    }
}

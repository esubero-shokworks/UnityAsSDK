using TMPro;
using UnityEngine;

public class ServiceUI : MonoBehaviour, IUIService
{
    [SerializeField] private TMP_Text stateLabel;

    public void UpdateState(string currentState)
    {
        stateLabel.text = currentState;
    }
}

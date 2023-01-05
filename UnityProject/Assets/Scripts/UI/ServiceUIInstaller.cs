using UnityEngine;

public class ServiceUIInstaller : MonoBehaviour
{
    [SerializeField] private ServiceUI uiService;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IUIService>(uiService);
    }
}

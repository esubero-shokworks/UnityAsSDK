using UnityEngine;

public class ServiceARObjectManagementInstaller : MonoBehaviour
{
    [SerializeField] private ServiceARObjectManagement arObjectService;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IARObjectManagementService>(arObjectService);
    }
}

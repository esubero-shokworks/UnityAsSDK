using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceARObjectManagementInstaller : MonoBehaviour
{
    [SerializeField] private ServiceARObjectManagement arObjectService;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IARObjectManagementService>(arObjectService);
    }
}

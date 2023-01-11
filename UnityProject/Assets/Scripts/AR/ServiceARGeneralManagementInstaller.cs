using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceARGeneralManagementInstaller : MonoBehaviour
{
    [SerializeField] private ServiceARGeneralManagement arService;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IARGeneralManagementService>(arService);
    }
}

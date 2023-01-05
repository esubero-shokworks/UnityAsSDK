using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceARInstaller : MonoBehaviour
{
    [SerializeField] private ServiceAR arService;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IARService>(arService);
    }
}

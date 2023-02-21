using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceCallbackManagerInstaller : MonoBehaviour
{
    [SerializeField] private ServiceCallbackManager callbackManager;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<ICallbackManagerService>(callbackManager);
    }
}

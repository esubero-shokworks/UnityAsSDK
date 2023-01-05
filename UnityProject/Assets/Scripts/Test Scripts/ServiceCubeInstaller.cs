using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceCubeInstaller : MonoBehaviour
{
    [SerializeField] private ServiceCube cubeService;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<ICubeService>(cubeService);
    }
}

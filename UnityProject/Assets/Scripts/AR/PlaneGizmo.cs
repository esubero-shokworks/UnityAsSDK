using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlaneGizmo : MonoBehaviour
{
    [SerializeField] private GameObject decoyPrefab;
    [SerializeField] private ARRaycastManager aRRaycastManager;
    [SerializeField] private ARPlaneManager aRPlaneManager;
    [SerializeField] private Camera arCamera;
    private string uiState;
    private Vector2 screenPosition = new Vector2(0, -180);
    private GameObject decoyInstantiated;
    private List<ARRaycastHit> hits;
  
    private void Start()
    {
        hits = new List<ARRaycastHit>();
        ServiceLocator.Instance.GetService<IUIService>().LoadingActivation(true);
    }

    private void Update()
    {
        screenPosition = new Vector3(Screen.width / 2, Screen.height / 2, arCamera.nearClipPlane);

        if (aRRaycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            ServiceLocator.Instance.GetService<IUIService>().LoadingActivation(false);
            foreach (ARRaycastHit hit in hits)
            {
                try
                {
                    Pose pose = hit.pose;

                    if (decoyInstantiated == null)
                    {
                        decoyInstantiated = Instantiate(decoyPrefab, pose.position, pose.rotation);
                        uiState = $"ObjectInstantiated {DateTime.Now}";
                    }
                    else
                    {
                        decoyInstantiated.transform.SetPositionAndRotation(pose.position, pose.rotation);
                        uiState = $"Object moved {DateTime.Now}";
                    }
                    ServiceLocator.Instance.GetService<IUIService>().UpdateStatusLabel(uiState);
                }
                catch (Exception thrownException)
                {
                    ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"PlaneGizmo: {thrownException.Message}");
                    throw;
                }
            }
        }
    }

}

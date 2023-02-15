using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneGizmo : MonoBehaviour
{
    [SerializeField] private GameObject decoyPrefab;
    [SerializeField] private ARRaycastManager aRRaycastManager;
    [SerializeField] private ARPlaneManager aRPlaneManager;
    [SerializeField] private Camera arCamera;
    private int numberOfPlanes;
    private int biggerPlaneIndex;
    private string uiState;
    private Vector2 screenPosition = new Vector2(0, -180);
    private GameObject decoyInstantiated;
    private List<ARRaycastHit> hits;
  
    private void Awake()
    {
        numberOfPlanes = -1;
        biggerPlaneIndex = -1;
        hits = new List<ARRaycastHit>();
    }

    private void OnEnable()
    {
        aRPlaneManager.planesChanged += CheckNewPlane;
    }

    private void OnDisable()
    {
        aRPlaneManager.planesChanged -= CheckNewPlane;
    }

    private void Update()
    {
        //screenPosition = arCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, arCamera.nearClipPlane));
        screenPosition = new Vector3(Screen.width / 2, Screen.height / 2, arCamera.nearClipPlane);

        if (aRRaycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            foreach (ARRaycastHit hit in hits)
            {
                Pose pose = hit.pose;
                if (decoyInstantiated == null)
                {
                    decoyInstantiated = Instantiate(decoyPrefab, pose.position, pose.rotation);
                    uiState = $"ObjectInstantiated {DateTime.Now}";
                    Utils.UpdateUIState(uiState);
                }
                else
                {
                    decoyInstantiated.transform.SetPositionAndRotation(pose.position, pose.rotation);
                    uiState = $"Object moved {DateTime.Now}";
                    Utils.UpdateUIState(uiState);
                }
            }
        }
    }

    private void CheckNewPlane(ARPlanesChangedEventArgs obj)
    {
        //if (obj.added.Count >= numberOfPlanes)
        //{
        //    int bestIndex = 0;

        //    numberOfPlanes = obj.added.Count;
            
        //    if (obj.added.Count >= 1)
        //    {
        //        Vector3 bestSize = Vector3.zero;
        //        float bestDistance = float.MinValue;

        //        for (int i = 0; i < obj.added.Count; i++)
        //        {
        //            if (i == 0)
        //            {
        //                bestSize = obj.added[i].GetComponent<MeshRenderer>().bounds.size;
        //            }
        //            else
        //            {
        //                Vector3 currentSize = obj.added[i].GetComponent<MeshRenderer>().bounds.size;
        //                float currentDistance = Vector3.Distance(bestSize, currentSize);
        //                if (currentDistance >= bestDistance)
        //                {
        //                    bestDistance = currentDistance;
        //                    bestIndex = i;
        //                }
        //            }
        //        }
        //    }

        //    biggerPlaneIndex = bestIndex;
        //}
    }
}

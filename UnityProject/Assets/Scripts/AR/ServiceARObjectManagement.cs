using Dummiesman;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ServiceARObjectManagement : MonoBehaviour, IARObjectManagementService
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Transform objectLocation;
    [SerializeField] private Material basicMaterial;
    private GameObject objectToInstantiate;
    private GameObject objectInstantiated;
    private bool readyToInstantiateObject = true;
    private int callCount;

    private void Start()
    {
        readyToInstantiateObject = true;
        InputController.Instance.OnHoldingTouch += DetectedDraggingCommand;
        InputController.Instance.OnEndTouch += DetectPutCommand;
    }

    private void OnDisable()
    {
        InputController.Instance.OnHoldingTouch -= DetectedDraggingCommand;
        InputController.Instance.OnEndTouch -= DetectPutCommand;
    }

    private void DetectPutCommand(Vector2 position)
    {
        if (readyToInstantiateObject)
        {
            InstantiateARObject(position);
        }
    }

    private void DetectedDraggingCommand(Vector2 position)
    {
        callCount++;
        if (callCount > 4 && callCount % 2 == 0)
        {
            RelocateARObject(position);
        }
    }

    public void InstantiateARObject(string pathObjectToInstantiate)
    {
        try
        {
            DestroyARObject();
            var url = $"{Application.persistentDataPath}/{pathObjectToInstantiate}";
            StartCoroutine(GetFileAndInstantiate(url));

            string uiState = $"Called InstantiateARObject of {pathObjectToInstantiate}";
            ServiceLocator.Instance.GetService<IUIService>().UpdateStatusLabel(uiState);
        }
        catch (Exception thrownException)
        {
            ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"InstantiateARObject from path: {thrownException.Message}");
            throw;
        }
    }

    public void InstantiateARObject(Vector3 touchScreenPosition)
    {
        try
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            objectToInstantiate = GameObject.CreatePrimitive(PrimitiveType.Capsule);

            if (raycastManager.Raycast(touchScreenPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                foreach (ARRaycastHit hit in hits)
                {
                    Pose pose = hit.pose;
                    if (objectInstantiated == null)
                    {
                        objectInstantiated = InstantiateARObject(objectToInstantiate, objectLocation, pose.position, pose.rotation);
                        objectInstantiated.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    }
                    else
                    {
                        objectInstantiated.transform.SetPositionAndRotation(pose.position, pose.rotation);
                    }
                }
            }

            string uiState = $"Called InstantiateARObject of {objectToInstantiate.name} with Touch Location: {touchScreenPosition}";
            ServiceLocator.Instance.GetService<IUIService>().UpdateStatusLabel(uiState);
        }
        catch (Exception thrownException)
        {
            ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"InstantiateARObject with Touch: {thrownException.Message}");
            throw;
        }
    }

    public void RelocateARObject(Vector3 touchScreenPosition)
    {
        try
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if (raycastManager.Raycast(touchScreenPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                foreach (ARRaycastHit hit in hits)
                {
                    Pose pose = hit.pose;
                    objectInstantiated.transform.SetPositionAndRotation(pose.position, pose.rotation);
                }
            }

            string uiState = $"Called RelocateARObject of {objectInstantiated.name} with Touch Location: {touchScreenPosition}";
            ServiceLocator.Instance.GetService<IUIService>().UpdateStatusLabel(uiState);
        }
        catch (Exception thrownException)
        {
            ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"RelocateARObject: {thrownException.Message}");
            throw;
        }
    }

    public void DestroyARObject()
    {
        try
        {
            foreach (Transform arObject in objectLocation)
            {
                Destroy(arObject.gameObject);
            }
            string uiState = $"Called to DestroyARObject";
            ServiceLocator.Instance.GetService<IUIService>().UpdateStatusLabel(uiState);
        }
        catch (Exception thrownException)
        {
            ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"ServiceARObjectManagement: {thrownException.Message}");
            throw;
        }
    }

    private IEnumerator GetFileAndInstantiate(string uri)
    {
        Debug.Log(uri);
        GameObject objectFromPath;
        try
        {
            objectFromPath = new OBJLoader().Load(uri);
            //Texture oldTexture = objectFromPath.GetComponent<Renderer>().material.mainTexture;
            //objectFromPath.GetComponent<Renderer>().material = basicMaterial;
            //objectFromPath.GetComponent<Renderer>().material.SetTexture("oldTexture", oldTexture);
        }
        catch (Exception thrownException)
        {
            ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"ServiceARObjectManagement: {thrownException.Message}");
            throw;
        }

        yield return null;

        if (objectFromPath != null)
        {
            GameObject objectInUnity = InstantiateARObject(objectFromPath, objectLocation, Vector3.zero, Quaternion.identity);
            objectInUnity.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

    private GameObject InstantiateARObject(GameObject arObject, Transform parentTransform, Vector3 targetPosition, Quaternion targetRotation)
    {
        GameObject objectToInstantiate;

        objectToInstantiate = Instantiate(arObject, parentTransform);
        objectToInstantiate.transform.SetLocalPositionAndRotation(targetPosition, targetRotation);

        return objectToInstantiate;
    }
}

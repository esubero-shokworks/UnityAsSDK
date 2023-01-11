using Dummiesman;
using System;
using System.Collections;
using UnityEngine;

public class ServiceARObjectManagement : MonoBehaviour, IARObjectManagementService
{
    [SerializeField] private Transform objectLocation;
    [SerializeField] private Material basicMaterial;

    public void InstantiateARObject(string pathObjectToInstantiate)
    {
        try
        {
            DestroyARObject();
            var url = $"{Application.persistentDataPath}/{pathObjectToInstantiate}";
            StartCoroutine(GetFileAndInstantiate(url));

            string uiState = $"Called InstantiateARObject of {pathObjectToInstantiate}";
            Utils.UpdateUIState(uiState);

        }
        catch (Exception thrownException)
        {
            Utils.CommunicateErrorWithNative(thrownException, this);
            throw;
        }
    }

    public void DestroyARObject()
    {
        foreach (Transform arObject in objectLocation)
        {
            Destroy(arObject.gameObject);
        }
        string uiState = $"Called to DestroyARObject";
        Utils.UpdateUIState(uiState);
    }

    private IEnumerator GetFileAndInstantiate(string uri)
    {
        Debug.Log(uri);

        GameObject objectFromPath = new OBJLoader().Load(uri);
        //Texture oldTexture = objectFromPath.GetComponent<Renderer>().material.mainTexture;
        //objectFromPath.GetComponent<Renderer>().material = basicMaterial;
        //objectFromPath.GetComponent<Renderer>().material.SetTexture("oldTexture", oldTexture);
        yield return null;
        GameObject objectInUnity = Instantiate(objectFromPath, objectLocation);
        objectInUnity.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}

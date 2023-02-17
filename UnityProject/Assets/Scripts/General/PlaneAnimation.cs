using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnimation : MonoBehaviour
{
    private Material planeMaterial;

    private void Awake()
    {
        planeMaterial = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        planeMaterial.mainTextureOffset = new Vector2(planeMaterial.mainTextureOffset.x + Time.deltaTime, planeMaterial.mainTextureOffset.y + Time.deltaTime);
    }
}

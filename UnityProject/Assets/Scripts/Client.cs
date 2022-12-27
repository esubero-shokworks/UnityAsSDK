using UnityEngine;

public class Client : MonoBehaviour
{
    private void Awake()
    {
        var serviceCube = FindObjectOfType<ServiceCube>();
        ServiceLocator.Instance.RegisterService<ICubeService>(serviceCube);
    }

    public void DoChangeColor(string newColor)
    {
        Debug.Log("Receiving the change color command");
        ServiceLocator.Instance.GetService<ICubeService>().ChangeColor(newColor);
    }
}

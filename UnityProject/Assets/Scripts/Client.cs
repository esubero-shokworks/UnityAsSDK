using UnityEngine;

public class Client : MonoBehaviour
{
    public void DoChangeColor(string newColor)
    {
        Debug.Log("Receiving the change color command");
        ServiceLocator.Instance.GetService<ICubeService>().ChangeColor(newColor);
    }
}

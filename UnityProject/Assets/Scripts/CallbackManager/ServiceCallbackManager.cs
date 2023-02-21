using System;
using UnityEngine;

public class ServiceCallbackManager : MonoBehaviour, ICallbackManagerService
{
    public void SendCallbackMessage(string message)
    {
        string uiState = $"Sending Callback Message to Native: {message}";
        ServiceLocator.Instance.GetService<IUIService>().UpdateStatusLabel(uiState);

        try
        {
#if UNITY_ANDROID
            AndroidJavaClass jc = new AndroidJavaClass("com.company.product.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            overrideActivity.Call("GetCallbackMessage", message);
#elif UNITY_IOS || UNITY_TVOS
            NativeAPI.GetCallbackMessage(message);
#endif
        }
        catch (Exception e)
        {
            Debug.Log($"ServiceCallbackManager: {e.Message}");
        }
    }
}


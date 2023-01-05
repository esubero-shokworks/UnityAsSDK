using System;
using UnityEngine;

public static class Utils
{
    public static void UpdateUIState(string uiState)
    {
        try
        {
            ServiceLocator.Instance.GetService<IUIService>().UpdateState(uiState);
        }
        catch (Exception thrownException)
        {
            CommunicateErrorWithNative(thrownException);
            throw;
        }
    }

    public static void CommunicateErrorWithNative(Exception thrownException, UnityEngine.Object objectToPoint = null)
    {
        Debug.LogException(thrownException, objectToPoint);

#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.company.product.OverrideUnityActivity");
        AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
        //TODO: Define function on AndroidStudio
        overrideActivity.Call("showMainActivity", thrownException.Message);
#elif UNITY_IOS || UNITY_TVOS
            //TODO: Define function on AndroidStudio
            NativeAPI.showHostMainWindow(thrownException.Message);
#endif
    }
}

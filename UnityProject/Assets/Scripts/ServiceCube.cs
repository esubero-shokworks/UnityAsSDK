using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine;

#if UNITY_IOS || UNITY_TVOS
public class NativeAPI {
    [DllImport("__Internal")]
    public static extern void showHostMainWindow(string lastStringColor);
}
#endif

public class ServiceCube : MonoBehaviour, ICubeService
{
    public Text text;
    private string lastStringColor = "";

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * 10, 0);
		
		if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    private void AppendToText(string line) 
    { 
        text.text += line + "\n"; 
    }

    public void ChangeColor(string targetColor)
    {
        AppendToText( "Chancing Color to " + targetColor );

        lastStringColor = targetColor;

        if (targetColor == "red")
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (targetColor == "blue")
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (targetColor == "yellow")
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
    }


    private void ShowHostMainWindow()
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.company.product.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            overrideActivity.Call("showMainActivity", lastStringColor);
        } 
        catch(Exception e)
        {
            AppendToText("Exception during ShowHostMainWindow");
            AppendToText(e.Message);
        }
#elif UNITY_IOS || UNITY_TVOS
        NativeAPI.showHostMainWindow(lastStringColor);
#endif
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle("button");
        style.fontSize = 30;
        if (GUI.Button(new Rect(10, 10, 200, 100), "Red", style))
        {
            ChangeColor("red");
        }
        if (GUI.Button(new Rect(10, 110, 200, 100), "Blue", style))
        {
            ChangeColor("blue");
        }
        if (GUI.Button(new Rect(10, 300, 400, 100), "Show Main With Color", style))
        {
            ShowHostMainWindow();
        }

        if (GUI.Button(new Rect(10, 400, 400, 100), "Unload", style))
        {
            Application.Unload();
        }
        if (GUI.Button(new Rect(440, 400, 400, 100), "Quit", style))
        {
            Application.Quit();
        }
    }
}


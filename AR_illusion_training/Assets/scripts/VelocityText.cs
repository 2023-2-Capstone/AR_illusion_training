using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class VelocityText : MonoBehaviour
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _startTracking(string objectName);
#endif
    public TextMeshProUGUI OutputText;
    float Velocity;
    bool isTracking = false;

    
    void Start()
    {
        StartSpeedTracking();
        Velocity = 0f;
    }

    void Update()
    {
        
    }


    public void StartSpeedTracking()
    {
        if (this.isTracking == false)
        {
            this.isTracking = true;
#if UNITY_IOS
            Debug.Log($"GameObjectName: {gameObject.name}");
            _startTracking(gameObject.name);
#else
            Debug.Log("No iOS Device Found");
#endif
        }

    }

    public void SpeedCallBackMethod(string speed)
    {
        Debug.Log($"유니티에서 받은 스피드: {speed}");
        //아이폰이 없어서 실험불가. 되는지 확인해주세요.
        OutputText.text = speed;
        Velocity = float.Parse(speed);
    }
}

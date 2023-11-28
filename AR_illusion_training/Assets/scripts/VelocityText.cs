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
    [DllImport("__Internal")]
    private static extern void _stopTracking();
    [DllImport("__Internal")]
    private static extern void _makeVibrate();
    [DllImport("__Internal")]
    private static extern void _getEnergeBurned(string objectName);
    [DllImport("__Internal")]
    private static extern void _getHeartRateData(string objectName);
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
            _makeVibrate();
            _getEnergeBurned(gameObject.name); // 오늘 하루 소모한 칼로리량 요청 (실시간 데이터 아님)
            _getHeartRateData(gameObject.name); // 최근 심장 박동 데이터 요청 (실시간 데이터 아님)
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

    public void DirectionCallBackMethod(string direction)
    {
        Debug.Log($"유니티에서 받은 방향 데이터: {direction}");
    }

    public void CalorieCallBackMethod(string calorie)
    {
        Debug.Log($"유니티에서 받은 칼로리: {calorie}");
    }

    public void HeartRateCallBackMethod(string RecentHeartRate)
    {
        Debug.Log($"유니티에서 받은 최근 심장 박동 데이터: {RecentHeartRate}");
    }
}

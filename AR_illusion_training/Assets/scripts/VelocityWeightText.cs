using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class VelocityWeightText : MonoBehaviour
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _startTracking(string objectName);
#endif

    public TextMeshProUGUI OutputText;
    public Scrollbar VelocityWeightScrollbar;
    float VelocityWeight = 1f;
    bool isTracking = false;

    void Start(){
        StartSpeedTracking();
    }
    void Update(){
        UpdateVelocity();
    }

    private void UpdateVelocity() {
        VelocityWeight = VelocityWeightScrollbar.value * 2;
        VelocityWeight = (float)Math.Round(VelocityWeight, 1);
        OutputText.text = VelocityWeight.ToString(); // Velocity 값을 문자열로 변환하여 표시
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
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class VelocityButton : MonoBehaviour
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _startTracking(string objectName);
#endif

    public TextMeshProUGUI VelText;
    float Velocity;
    bool isTracking = false;

    void Start(){
        Velocity = 0;
        UpdateVelocityText();
        StartSpeedTracking();
    }

    public void AddVelocity(){
        Velocity += 1;
        UpdateVelocityText();
    }
    public void SubVelocity(){
        Velocity -= 1;
        UpdateVelocityText();
    }

    private void UpdateVelocityText() {
        VelText.text = Velocity.ToString(); // Velocity 값을 문자열로 변환하여 표시
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

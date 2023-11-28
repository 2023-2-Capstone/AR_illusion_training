using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSystem : MonoBehaviour
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _startTracking(string objectName);
    [DllImport("__Internal")]
    private static extern void _stopTracking();
    [DllImport("__Internal")]
    private static extern void _makeVibrate();
    /*
    애플 건강앱에서 가져오는 칼로리와 심장박동 데이터를 가져온다.
    하지만 몇분 전의 데이터를 가져오기 때문에 유용성이 떨어진다.

    [DllImport("__Internal")]
    private static extern void _getEnergeBurned(string objectName);
    [DllImport("__Internal")]
    private static extern void _getHeartRateData(string objectName);
    */
#endif
    public int UserWeight = 0;

    private bool isTracking = false;
    private int Reps = 0; //운동 횟수.
    private float Timer = 0f;
    private bool TimerActivated = false;
    private float RepsPerTimer = 0f;
    private float pullupkcal = 1f; //77kg 성인남성 기준
    private float pushupkcal = 0.47f; //77kg 성인남성 기준
    private float DeviceSpeed = 0f;
    private double DeviceDirX = 0f;
    private double DeviceDirY = 0f;
    private double DeviceDirZ = 0f;

    void Start()
    {
        StartSpeedTracking();
    }

    
    void Update()
    {
        //타이머 작동 명령어
        Timer += Time.deltaTime;
        Timer = Mathf.Round(Timer * 100f) / 100f;


    }

    public void StartSpeedTracking()
    {
        if (this.isTracking == false)
        {
            this.isTracking = true;
#if UNITY_IOS
            Debug.Log($"GameObjectName: {gameObject.name}");
            _startTracking(gameObject.name);
            //_makeVibrate();
            //_getEnergeBurned(gameObject.name); // 오늘 하루 소모한 칼로리량 요청 (실시간 데이터 아님)
            //_getHeartRateData(gameObject.name); // 최근 심장 박동 데이터 요청 (실시간 데이터 아님)
#else
            Debug.Log("No iOS Device Found");
#endif
        }

    }

    public void SpeedCallBackMethod(string speed)
    {
        Debug.Log($"유니티에서 받은 스피드: {speed}");
        //speed float로 변환 후 소수점 3자리에서 반올림
        DeviceSpeed = Mathf.Round(float.Parse(speed) * 100f) / 100f;
    }

    public void DirectionCallBackMethod(string direction)
    {
        Debug.Log($"유니티에서 받은 방향 데이터: {direction}");
        /*
        direction은
        "x:-0.0227203369140625 y:-0.9516754150390625 z:-0.2386932373046875"
        와 같은 string 형태로 들어온다.
        */

    }

    /*
    ***사용하지 않음***

    public void CalorieCallBackMethod(string calorie)
    {
        Debug.Log($"유니티에서 받은 칼로리: {calorie}");
    }

    public void HeartRateCallBackMethod(string RecentHeartRate)
    {
        Debug.Log($"유니티에서 받은 최근 심장 박동 데이터: {RecentHeartRate}");
    }
    */


}

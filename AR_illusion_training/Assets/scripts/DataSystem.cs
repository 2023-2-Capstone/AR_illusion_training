using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    GameObject WeightData;

    private int UserWeight = 0;
    private bool isTracking = false;
    private int Reps = 0; //운동 횟수.
    private float SpeedThresholdForReps = 1.5f;
    private float Timer = 0f;
    private bool TimerActivated = false;
    private float RepsPerTime = 0f;
    private float Pullupkcal = 1f; //77kg 성인남성 기준
    private float Pushupkcal = 0.47f; //77kg 성인남성 기준
    private float DeviceSpeed = 0f;
    private double DeviceDirX = 0f;
    private double DeviceDirY = 0f;
    private double DeviceDirZ = 0f;

    void Start()
    {
        StartSpeedTracking();

        //이전 씬에서 파괴되지 않은 오브젝트에서 값 추출 후 그 오브젝트 파괴
        WeightData = GameObject.Find("WeightData");
        UserWeight = WeightData.GetComponent<WeightData>().Weight;
        Destroy(WeightData);

        //몸무게에 알맞게 운동 1회당 소모 칼로리 변경
        Pullupkcal = (Pullupkcal / 77f) * UserWeight;
        Pushupkcal = (Pushupkcal / 77f) * UserWeight;
    }

    
    void Update()
    {
        //타이머 제어 명령어
        if(TimerActivated){
            Timer += Time.deltaTime;
        }

        //Reps 증가 명령어
        
    }

    /*
    타이머 제어 함수
    */
    public void PlayTimer(){
        TimerActivated = true;
    }

    public void StopTimer(){
        TimerActivated = false;
    }

    public void ResetTimer(){
        Timer = 0f;
        TimerActivated = false;
    }

    /*
    Reps 제어 함수
    */
    public void ResetReps(){
        Reps = 0;
    }

    /*
    iOS 관련 함수
    */
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
        DeviceSpeed = float.Parse(speed);
    }

    public void DirectionCallBackMethod(string direction)
    {
        Debug.Log($"유니티에서 받은 방향 데이터: {direction}");
        /*
        direction은
        "x:-0.0227203369140625 y:-0.9516754150390625 z:-0.2386932373046875"
        와 같은 string 형태로 들어온다.
        */
        string[] parts = direction.Split(' ');

            foreach (string part in parts)
            {
                if (part.StartsWith("x:"))
                {
                    DeviceDirX = double.Parse(part.Substring(2));
                }
                else if (part.StartsWith("y:"))
                {
                    DeviceDirY = double.Parse(part.Substring(2));
                }
                else if (part.StartsWith("z:"))
                {
                    DeviceDirZ = double.Parse(part.Substring(2));
                }
            }
    }

    public void PlayVibrate(){
#if UNITY_IOS
        _makeVibrate();
#else
        Debug.Log("No iOS Device Found");
#endif
    }

    /*
    사용하지 않음
    *
    public void CalorieCallBackMethod(string calorie)
    {
        Debug.Log($"유니티에서 받은 칼로리: {calorie}");
    }

    public void HeartRateCallBackMethod(string RecentHeartRate)
    {
        Debug.Log($"유니티에서 받은 최근 심장 박동 데이터: {RecentHeartRate}");
    }
    */

    /*
    변수 반환 함수들

    float나 double의 경우 소수점 3자리에서 반올림. 소수점 2자리까지 출력
    */
    public float GetTime()
    {
        return Mathf.Round(Timer * 100f) / 100f;
    }

    public float GetRepsPerTime()
    {
        return RepsPerTime;
    }

    public float GetDeviceSpeed()
    {
        return Mathf.Round(DeviceSpeed * 100f) / 100f;
    }

    public double GetDeviceDirX()
    {
        return Math.Round(DeviceDirX, 2);
    }

    public double GetDeviceDirY()
    {
        return Math.Round(DeviceDirY, 2);
    }

    public double GetDeviceDirZ()
    {
        return Math.Round(DeviceDirZ, 2);
    }
    
    public float GetSumOfPullupkcal(){
        return Mathf.Round(Reps * Pullupkcal * 100f) / 100f;
    }

    public float GetSumOfPushupkcal(){
        return Mathf.Round(Reps * Pushupkcal * 100f) / 100f;
    }

}

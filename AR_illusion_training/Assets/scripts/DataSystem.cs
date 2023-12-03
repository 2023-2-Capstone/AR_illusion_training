using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Linq;


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
    /*
    NotPlaying 상태
    - 운동 모드 설정 가능
    - 가중치 설정 가능
    - 타이머, 횟수, 칼로리 전부 초기화 상태

    Playing 상태
    - 운동 모드 변경 불가
    - 가중치 설정 불가
    - 타이머 지속적으로 증가
    - 횟수 증가, 그에 맞게 칼로리 증가

    운동 모드 설정과 가중치 설정의 활성화/비활성화는 버튼으로 이루어지므로 그 쪽 코드에서 처리
    */
    enum GamePlayState{
        NotPlaying,
        Playing
    }
    enum ExerciseMode{
        Pullup,
        Pushup
    }

    GameObject UserWeightData;

    private int UserWeight = 0;
    private bool isTracking = false;
    private bool CanCountAgain = true; //프레임마다 카운팅 방지
    private GamePlayState PlayState = GamePlayState.NotPlaying; //기본모드 : NotPlaying
    private ExerciseMode Exercise = ExerciseMode.Pullup; //기본모드 : 풀업
    private int HalfReps = 0; //올라갔다 내려가야 횟수 인정을 위함
    private int Reps = 0; //운동 횟수.
    private float SpeedThresholdForPullup = 1.5f;
    private float SpeedThresholdForPushup = 1.5f;
    private float Timer = 0f; //단위는 초
    private bool TimerActivated = false;
    private float RepsPerTime = 0f;
    private float[] RepsData = new float[5];
    private int TmpTimer = 0; // RepsData 구하는데 사용
    private float Burnedkcals = 0f;
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
        UserWeightData = GameObject.Find("WeightData");
        UserWeight = UserWeightData.GetComponent<WeightData>().Weight;
        Destroy(UserWeightData);

        //몸무게에 알맞게 운동 1회당 소모 칼로리 변경
        Pullupkcal = (Pullupkcal / 77f) * UserWeight;
        Pushupkcal = (Pushupkcal / 77f) * UserWeight;
    }

    
    void Update()
    {

        //NotPlaying일 때는 Update에서 수행할 함수 없음.

        if(PlayState == GamePlayState.Playing)
        {
            //타이머 작동 명령어
            //Playing 상태가 아니면 아예 타이머를 조작할 수 없다.
            //단, 일시정지 기능을 추가하기 위해 TimerActivated를 사용
            if(TimerActivated)
                Timer += Time.deltaTime;


            //theshold를 넘으면 카운트, theshold 아래로 다시 내려올 때까지 카운트 중지
            //Z축 처리를 어떻게 할 지 몰라 일단 가속계로만 측정.
            if(Exercise == ExerciseMode.Pullup){
                if(DeviceSpeed >= SpeedThresholdForPullup){
                    if(CanCountAgain){
                        HalfReps++;
                        CanCountAgain = false;
                    }
                }else{
                    CanCountAgain = true;
                }
            }else if(Exercise == ExerciseMode.Pushup){
                if(DeviceSpeed >= SpeedThresholdForPushup){
                    if(CanCountAgain){
                        HalfReps++;
                        CanCountAgain = false;
                    }
                }else{
                    CanCountAgain = true;
                }
            }
            
            //HalfReps가 2개면 Reps 한 개로 변환
            if(HalfReps == 2){
                Reps++;
                HalfReps = 0;
            }

            //칼로리 측정
            if(Exercise == ExerciseMode.Pullup){
                Burnedkcals = Reps * Pullupkcal;
            }else if(Exercise == ExerciseMode.Pushup){
                Burnedkcals = Reps * Pushupkcal;
            }

            //시간당 운동 횟수 처리
            /*
            지난 n초간 RepsPerTime은?
            n초간 한 reps / n
            음 시파
            5크기배열을 만들고,
            1초당 한번씩 저장해야겠다.

            */
            calculateReps(Timer);
        }
    }

    /*
    운동 시작 및 종료
    */
    public void StartExercise(){
        PlayState = GamePlayState.Playing;
        ResetAllValues();
        TimerActivated = true;
    }

    public void EndExercise(){
        PlayState = GamePlayState.NotPlaying;
        ResetAllValues();
    }

    void ResetAllValues(){
        ResetTimer();
        ResetReps();
        ResetBurnedkcals();
        ResetRepsData();
        ResetTmpTimer();
        HalfReps = 0;
        CanCountAgain = true;
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
    void ResetRepsData(){
        RepsData = new float[5] {0,0,0,0,0};
    }
    void ResetTmpTimer(){
        TmpTimer = 0;
    }

    void calculateReps(float currentTime){
        /*
        1초, 2초와 같이 정수초일 때만 실행해야한다.
        단, currentTime이 0.99초 후 다음 틱(프레임)이 1.01초가 되는 경우를 고려해야 한다.

        인덱스의 값들은 그 순간의 총 Reps

        5초전 (index + 1)의 총 Reps와 비교해서 구한다.
        */
        int tmpTime = (int)currentTime;
        if(TmpTimer != tmpTime){
            TmpTimer = tmpTime;
            tmpTime %= 5;

            RepsData[tmpTime] = Reps;

            if(tmpTime == 4){
                RepsPerTime = RepsData[tmpTime] - RepsData[0];
            }else{
                RepsPerTime = RepsData[tmpTime] - RepsData[tmpTime + 1];
            }
            RepsPerTime /= 5;
        }
    }

    /*
    kcal 제어 함수
    */
    public void ResetBurnedkcals(){
        Burnedkcals = 0f;
    }

    /*
    iOS 관련 함수. 가속도, 방향, 진동
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

    public void PlayVibration(){
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
    State 제어 함수
    */
    //ExerciseModeButton이 사용
    public void ChangeExerciseMode(){
        if(Exercise == ExerciseMode.Pullup){
            Exercise = ExerciseMode.Pushup;
        }else if(Exercise == ExerciseMode.Pushup){
            Exercise = ExerciseMode.Pullup;
        }
    }

    //PlayButton 클릭 시 발생
    public void ChangePlayState(){
        if(PlayState == GamePlayState.NotPlaying){
            StartExercise();
        }else if(PlayState == GamePlayState.Playing){
            EndExercise();
        }
    }

    /*
    변수 반환 함수들

    float나 double의 경우 소수점 3자리에서 반올림. 소수점 2자리까지 출력
    */
    public string GetPlayState(){
        if(PlayState == GamePlayState.NotPlaying){
            return "NotPlaying";
        }else if(PlayState == GamePlayState.Playing){
            return "Playing";
        }else{
            return "WrongPlayState";
        }
    }

    public string GetExerciseState(){
        if(Exercise == ExerciseMode.Pullup){
            return "Pull-up";
        }else if(Exercise == ExerciseMode.Pushup){
            return "Push-up";
        }else{
            return "WrongExerciseMode";
        }
    }

    public int GetWeight(){
        return UserWeight;
    }
    public float GetTime()
    {
        return Mathf.Round(Timer * 100f) / 100f;
    }
    public int GetReps(){
        return Reps;
    }
    public float GetRepsPerTime()
    {
        return Mathf.Round(RepsPerTime * 100f) / 100f;
    }
    public float GetBurnedkcals(){
        return Mathf.Round(Burnedkcals* 100f) / 100f;
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
}

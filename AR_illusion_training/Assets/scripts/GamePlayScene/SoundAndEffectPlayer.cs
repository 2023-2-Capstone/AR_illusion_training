using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class SoundAndEffectPlayer : MonoBehaviour
{
    public DataSystem dataSystem;
    public Scrollbar EffectWeightScrollbar;
    public TextMeshProUGUI WarningText;
    [SerializeField] private AudioClip[] AudioClips;
    private AudioSource currentAudio;
    GameObject soundPlayer;

    public GameObject Effect1;
    public GameObject Effect2;

    public GameObject Effect3;
    public Camera ARCamera;
    public GameObject Ceiling;
    private GameObject SpawnCeiling;
    private GameObject SpawnEffect1;
    private GameObject SpawnEffect2;
    private GameObject SpawnEffect3;
    public Animator animator;
    private bool isPlay=false;
    public GameObject Alien;
    private GameObject SpawnAlien;
    private int cur=0;
    private float approachSpeed = 0.1f;
    private bool Spawn=true;
    private float initDistance = 0;
    private float currentDistance = 0;
    private int WarnStack = 0;
    private bool GameEndFlag = false;


    void Start()
    {
        soundPlayer = GameObject.Find("SoundPlayer");
        currentAudio = GetComponent<AudioSource>();
        WarningText.text = "";
        // SpawnAlien = Instantiate(Alien, ARCamera.transform.position+ARCamera.transform.forward*10, Quaternion.Euler(0,180,0));

    }

    void Update()
    {
        
        if(dataSystem.GetPlayState()=="Playing" && Spawn){
            SpawnAlien = Instantiate(Alien, ARCamera.transform.position+ARCamera.transform.forward*10-ARCamera.transform.up*2, Quaternion.Euler(0,180,0));
            SpawnCeiling = Instantiate(Ceiling, ARCamera.transform.position+ARCamera.transform.up*10, Quaternion.Euler(90,0,0));
            
            //초기거리 계산
            initDistance = Vector3.Distance(ARCamera.transform.position,SpawnAlien.transform.position);
            //경고 스택 초기화
            WarnStack = 3;
            GameEndFlag = false;
            Spawn=false;
        }
        if(dataSystem.GetPlayState()=="NotPlaying"){
            Destroy(SpawnAlien);
            Destroy(SpawnCeiling);
            Destroy(SpawnEffect1);
            Destroy(SpawnEffect2);
            Destroy(SpawnEffect3);

            Spawn=true;
        }

        if(cur!=dataSystem.GetReps()){
            isPlay=true;
            cur=dataSystem.GetReps();
        }

        if (isPlay) {
            SpawnEffect();
            if(dataSystem.GetReps()%3==0){
                SpawnEffect1 = Instantiate(Effect1, SpawnAlien.transform.position, Quaternion.Euler(0,0,0));
                StartCoroutine(FadeOut(SpawnEffect1, 3f)); // 3초 동안 페이드 아웃
            }
            isPlay=false;
        }

        if(dataSystem.GetReps()>=(EffectWeightScrollbar.value*47+3)){
            StartCoroutine(FadeOut(SpawnAlien,3f));
            if(GameEndFlag == false){
                GameClear();
                GameEndFlag = true;
            }
        }

        //EffectWeightScrollbar.value 0~1    dataSystem.GetReps()%(EffectWeightScrollbar.value*10)==0
        Vector3 directionToCamera = (ARCamera.transform.position - SpawnAlien.transform.position).normalized;

        // 목표 위치 계산
        Vector3 targetPosition = ARCamera.transform.position - directionToCamera;

        // 외계인의 현재 위치에서 목표 위치까지 서서히 다가오도록 보간
        SpawnAlien.transform.position = Vector3.Lerp(SpawnAlien.transform.position, targetPosition, Time.deltaTime * approachSpeed);
        
        //현재 거리와 경고 스택를 활용해서 경고메세지 출력
        currentDistance = Vector3.Distance(ARCamera.transform.position,SpawnAlien.transform.position);
        if(currentDistance <= initDistance * WarnStack / 5){
            if(WarnStack <= 3 && WarnStack >= 1){    
                Warning(WarnStack);
                WarnStack -= 1;
            }
        }
        //현재 거리가 특정거리 이하면 게임오버
        if(currentDistance <= initDistance / 6 && GameEndFlag == false){
            GameOver();
            StartCoroutine(FadeOut(SpawnAlien,3f));
            GameEndFlag = true;
        }
    }


    //Play 버튼에서 사용
    public void PlayButtonSound(){
        soundPlayer.GetComponent<SoundPlayer>().PlayButton2Audio();
        //경고메세지 초기화
        WarningText.text = "";
    }

    //경고 함수. 1단계 ~ 3단계가 있음
    void Warning(int WarningLevel){
        currentAudio.clip = AudioClips[0];
        WarningText.color = Color.red;
        switch (WarningLevel){
            case 3:
                currentAudio.pitch = 0.65f;
                WarningText.text = "적이 가까워지고 있습니다.\n안전거리를 확보하세요.";
                break;
            case 2:
                currentAudio.pitch = 1.1f;
                WarningText.text = "적이 지나치게 가깝습니다.\n늦게 전에 적을 몰아내세요";
                break;

            case 1:
                currentAudio.pitch = 2.02f;
                WarningText.text = "행운을 빕니다.";
                break;
            default:
                break;
        }
        currentAudio.Play();
        dataSystem.PlayVibration();

    }

    //게임오버 함수
    void GameOver(){
        WarningText.color = Color.red;
        currentAudio.clip = AudioClips[1];
        currentAudio.pitch = 1;
        currentAudio.Play();
        dataSystem.PlayVibration();
        WarningText.text = "당신은 죽었습니다.";
        dataSystem.StopTimer();
    }

    //게임클리어 함수
    void GameClear(){
        WarningText.color = Color.green;
        currentAudio.clip = AudioClips[2];
        currentAudio.pitch = 1;
        currentAudio.Play();
        dataSystem.PlayVibration();
        WarningText.text = "생존에 성공하셨습니다.";
        dataSystem.StopTimer();
    }


    public void SpawnEffect(){

        SpawnEffect2 = Instantiate(Effect2, SpawnAlien.transform.position, Quaternion.Euler(0,0,0));
        SpawnEffect3 = Instantiate(Effect3, SpawnAlien.transform.position, Quaternion.Euler(0,0,0));
                    
        StartCoroutine(FadeOut(SpawnEffect2, 2f));
        StartCoroutine(FadeOut(SpawnEffect3, 2f));
    }
    IEnumerator FadeOut(GameObject obj, float duration)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;
            Color initialColor = mat.color;
            for (float t = 0; t < 1; t += Time.deltaTime / duration)
            {
                Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, Mathf.Lerp(1, 0, t));
                mat.color = newColor;
                yield return null;
            }
        }
        Destroy(obj);
    }

}
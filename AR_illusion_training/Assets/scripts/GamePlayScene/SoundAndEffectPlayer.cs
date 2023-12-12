using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    private GameObject SpawnEffect1;
    private GameObject SpawnEffect2;
    private GameObject SpawnEffect3;

    private bool isPlay=false;

  
    void Start()
    {
        soundPlayer = GameObject.Find("SoundPlayer");
        currentAudio = GetComponent<AudioSource>();
        WarningText.text = "";
    }

    void Update()
    {
        /*
        if(dataSystem.GetPlayState() == "Playing")
        {
           
        }
        else if(dataSystem.GetPlayState() == "NotPlaying")
        {
            
        }
        */

        if(dataSystem.GetReps()%(EffectWeightScrollbar.value*10)==EffectWeightScrollbar.value*10-1){
            isPlay=true;
        }
        if (dataSystem.GetReps()%(EffectWeightScrollbar.value*10)==0 && isPlay) {
            SpawnEffect();
            isPlay=false;
        }
        //EffectWeightScrollbar.value 0~1    
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
        switch (WarningLevel){
            case 1:
                currentAudio.pitch = 0.65f;
                WarningText.text = "적이 가까워지고 있습니다.\n안전거리를 확보하세요.";
                break;
            case 2:
                currentAudio.pitch = 1.1f;
                WarningText.text = "적이 지나치게 가깝습니다.\n늦게 전에 적을 몰아내세요";
                break;

            case 3:
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
        currentAudio.clip = AudioClips[1];
        currentAudio.Play();
        currentAudio.pitch = 1;
        dataSystem.PlayVibration();
        WarningText.text = "당신은 죽었습니다.";
        dataSystem.ChangePlayState();
    }


    public void SpawnEffect(){
        SpawnEffect1 = Instantiate(Effect1, ARCamera.transform.position-ARCamera.transform.up*13, Quaternion.Euler(0,0,0));

        SpawnEffect2 = Instantiate(Effect2, ARCamera.transform.position-ARCamera.transform.up*13, Quaternion.Euler(0,0,0));
        SpawnEffect3 = Instantiate(Effect3, ARCamera.transform.position-ARCamera.transform.up*13, Quaternion.Euler(0,0,0));
        SpawnEffect1.transform.localScale *= 4;
        SpawnEffect2.transform.localScale *= 4;
        SpawnEffect3.transform.localScale *= 4;
                    
        StartCoroutine(FadeOut(SpawnEffect1, 10f)); // 3초 동안 페이드 아웃
        StartCoroutine(FadeOut(SpawnEffect2, 10f));
        StartCoroutine(FadeOut(SpawnEffect3, 10f));

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

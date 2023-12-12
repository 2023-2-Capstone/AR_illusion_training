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
    }

    void Update()
    {
        if(dataSystem.GetPlayState() == "Playing")
        {
            if(dataSystem.GetExerciseState() == "Pull-up"){
                
            }else if(dataSystem.GetExerciseState() == "Push-up"){
                
            }
        }
        else if(dataSystem.GetPlayState() == "NotPlaying")
        {
            
        }
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
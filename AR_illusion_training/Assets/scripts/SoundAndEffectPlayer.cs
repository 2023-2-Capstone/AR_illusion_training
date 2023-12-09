using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class SoundAndEffectPlayer : MonoBehaviour
{
    public DataSystem dataSystem;
    public Scrollbar EffectWeightScrollbar;
    [SerializeField] private AudioClip[] AudioClips;
    public GameObject Effect1;
    public GameObject Effect2;

    public GameObject Effect3;
    public Camera ARCamera;

    private GameObject SpawnEffect1;
    private GameObject SpawnEffect2;
    private GameObject SpawnEffect3;

    private bool isPlay=false;

    /*

    오디오 클립 배열에 할당된 오디오 
    index 0 : 우주 사운드

    */
    private AudioSource currentAudio;

    /*
    Stop 상태는 앵커 박기?

    Play 상태여야 모든것이 작동
    이때 pullup인지 pushup인지에 따라 재생할 것이 다름

    pullup이면 천장에 이펙트 재생, 소리도
    pushup이면 바닥에 이펙트 재생, 소리도

    일단 여기까지
    */
    
    void Start()
    {
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
    public void PlayStopBGM(){
        //오디오 설정
        if(dataSystem.GetExerciseState() == "Pull-up"){
            currentAudio.clip = AudioClips[0];
        }else{
            
        }

        //오디오 재생 또는 정지
        if(dataSystem.GetPlayState() == "Playing"){
            currentAudio.Play();
        }else if(dataSystem.GetPlayState() == "NotPlaying"){
            currentAudio.Stop();
        }
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

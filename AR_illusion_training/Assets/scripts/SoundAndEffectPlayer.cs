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
        
    }

    //Play 버튼에서 사용
    public void PlayStopBGM(){
        //오디오 설정
        if(dataSystem.GetExerciseState() == "Pull-up"){
            currentAudio.clip = AudioClips[0];
            Debug.Log("sdfsewrewrwerf");
        }else{

        }
        Debug.Log("sdfsf");
        //오디오 재생 또는 정지
        if(dataSystem.GetPlayState() == "Playing"){
            currentAudio.Play();
            Debug.Log("asdfas");
        }else if(dataSystem.GetPlayState() == "NotPlaying"){
            currentAudio.Stop();
        }
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    GameObject soundPlayer;


    void Start(){
        soundPlayer = GameObject.Find("SoundPlayer");
        soundPlayer.GetComponent<SoundPlayer>().PlayInvasionAudio();
    }

    public void OnClickedPlay(){
        
        soundPlayer.GetComponent<SoundPlayer>().PlayButton2Audio();
        SceneManager.LoadScene("GamePlayScene", LoadSceneMode.Single);
    }
}

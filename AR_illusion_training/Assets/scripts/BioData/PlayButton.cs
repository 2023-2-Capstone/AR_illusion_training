using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class PlayButton : MonoBehaviour
{
    public Button Button;
    public TextMeshProUGUI PlayText;
    public TextMeshProUGUI WeightText;
    GameObject soundPlayer;
    void Start()
    {
        soundPlayer = GameObject.Find("SoundPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        if(WeightText.text.Length == 1){
            Button.interactable = false;
            PlayText.color = new Color32(197,197,197,255);
        }else{
            Button.interactable = true;
            PlayText.color = new Color32(0,0,0,255);
        }
    }
    public void OnClickedPlay(){
        soundPlayer.GetComponent<SoundPlayer>().PlayButtonAudio();
        SceneManager.LoadScene("StoryText", LoadSceneMode.Single);
    }

}

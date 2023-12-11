using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayText : MonoBehaviour
{
    
    public TextMeshProUGUI OutputText;
    public DataSystem DataSys;
    public Button ExerciseModeButton;
    public Scrollbar EffectWeightScrollbar;
    string tmpstr = "";


    void Start(){
        OutputText.SetText("PLAY");
    }

    public void OnClikedPlay(){
        tmpstr = DataSys.GetPlayState();
        if(tmpstr == "NotPlaying"){
            OutputText.SetText("PLAY");
            ExerciseModeButton.interactable = true;
            EffectWeightScrollbar.interactable = true;
        }else if(tmpstr == "Playing"){
            OutputText.SetText("STOP");
            ExerciseModeButton.interactable = false;
            EffectWeightScrollbar.interactable = false;
        }
    }
}

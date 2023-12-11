using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExerciseModeText : MonoBehaviour
{
    public TextMeshProUGUI OutputText;
    public GameObject DataSys;
    
    public void OnClikedPlay(){
        OutputText.text = string.Format("Mode : {0}", DataSys.GetComponent<DataSystem>().GetExerciseState());
    }
}

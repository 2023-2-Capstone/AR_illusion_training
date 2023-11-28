using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExerciseModeText : MonoBehaviour
{
    public TextMeshProUGUI OutputText;
    public GameObject DataSys;
    
    public void OnClikedPlay(){
        OutputText.text = DataSys.GetComponent<DataSystem>().GetExerciseState();
    }
    
}

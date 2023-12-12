using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDataText : MonoBehaviour
{
    public TextMeshProUGUI OutputText;
    public DataSystem DataSys;
    
    void Start()
    {
        OutputTextFormat(0f, 0, 0f, 0f);
    }

    void Update()
    {   
        OutputTextFormat(DataSys.GetTime(), DataSys.GetReps(), DataSys.GetRepsPerTime(), DataSys.GetBurnedkcals());
    }

    void OutputTextFormat(float a, int b, float c, float d){
        OutputText.text = string.Format(
            "{0,-8}\n{1,-8}\n{2,-8}\n{3,-8}\n{4,-9}\n{5,-8}\n{6,-9}\n{7,-8}",
            "Time:",a,"Reps:",b,"R/T:",c,"kcal:",d
            );
    }
}

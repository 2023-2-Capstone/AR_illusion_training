using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class VelocityWeightText : MonoBehaviour
{
    public TextMeshProUGUI OutputText;
    public Scrollbar VelocityWeightScrollbar;
    private float VelocityWeight = 1f;
 
    void Update(){
        UpdateVelocity();
    }

    private void UpdateVelocity() {
        VelocityWeight = VelocityWeightScrollbar.value * 2;
        VelocityWeight = (float)Math.Round(VelocityWeight, 1);
        OutputText.text = VelocityWeight.ToString(); // Velocity 값을 문자열로 변환하여 표시
    }


   
}

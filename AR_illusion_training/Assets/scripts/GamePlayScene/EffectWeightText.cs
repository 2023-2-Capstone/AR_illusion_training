using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class EffectWeightText : MonoBehaviour
{
    public TextMeshProUGUI OutputText;
    public Scrollbar VelocityWeightScrollbar;
    private float VelocityWeight = 1f;

    public void UpdateVelocity() {
        VelocityWeight = VelocityWeightScrollbar.value * 47 + 3;
        VelocityWeight = (float)Math.Round(VelocityWeight);
        OutputText.text = VelocityWeight.ToString(); // Velocity 값을 문자열로 변환하여 표시
        OutputText.text = string.Format("목표 횟수 : {0}", VelocityWeight);
    }
}

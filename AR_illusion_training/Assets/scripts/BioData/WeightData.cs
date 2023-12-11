using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeightData : MonoBehaviour
{
    public TextMeshProUGUI WeightText;
    public int Weight;

    private void Awake(){
        DontDestroyOnLoad(gameObject);
    }

    // Play버튼 클릭 시 호출
    public void ApplyWeightTextToWeight(){
        Weight = int.Parse(WeightText.text.Trim((char)8203));
    }
}

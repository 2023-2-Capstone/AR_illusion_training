using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WeightData : MonoBehaviour
{
    public TextMeshProUGUI WeightText;

    void Update(){
        Debug.Log(WeightText.text);
    }
    private void Awake(){
        DontDestroyOnLoad(gameObject);
    }
}

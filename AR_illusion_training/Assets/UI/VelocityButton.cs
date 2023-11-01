using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VelocityButton : MonoBehaviour
{
    public TextMeshProUGUI VelText;
    float Velocity;

    void Start(){
        Velocity = 0;
        UpdateVelocityText();
    }

    //ref은 c++의 &이다.
    public void AddVelocity(){
        Velocity += 1;
        UpdateVelocityText();
    }
    public void SubVelocity(){
        Velocity -= 1;
        UpdateVelocityText();
    }

    private void UpdateVelocityText() {
        VelText.text = Velocity.ToString(); // Velocity 값을 문자열로 변환하여 표시
    }
}

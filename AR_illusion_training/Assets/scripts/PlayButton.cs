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
    // Start is called before the first frame update
    void Start()
    {

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
        SceneManager.LoadScene("GamePlayScene", LoadSceneMode.Single);
    }

}

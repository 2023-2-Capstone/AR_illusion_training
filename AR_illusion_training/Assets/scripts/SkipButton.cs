using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    public void OnClickedPlay(){
        SceneManager.LoadScene("GamePlayScene", LoadSceneMode.Single);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    public void OnClickedStart(){
        SceneManager.LoadScene("BioData", LoadSceneMode.Single);
    }
    public void OnClickedQuit(){
    //유니티 에디터에서는 Application.Quit()가 동작하지 않기 때문에 다음과 같이 처리
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

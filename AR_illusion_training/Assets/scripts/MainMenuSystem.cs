using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickedStart(){
        Debug.Log("게임 시작");
        SceneManager.LoadScene("BioData", LoadSceneMode.Single);
    }
    public void OnClickedQuit(){
        Debug.Log("종료");
    //유니티 에디터에서는 Application.Quit()가 동작하지 않기 때문에 다음과 같이 처리
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

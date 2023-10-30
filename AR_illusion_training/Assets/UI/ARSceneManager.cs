using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARSceneManager : MonoBehaviour
{
    /*
    씬을 전환하고 싶을 때 사용

    public void GotoMain(){
        GotoScene("Main");
    }
    public void GotoScene(string sceneName){
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    */

    //ref은 c++의 &이다.
    public void AddVelocity(ref float vel){
        vel += 1;
    }
    public void SubVelocity(ref float vel){
        vel -= 1;
    }
}

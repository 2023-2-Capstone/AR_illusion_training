using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ContentManager : MonoBehaviour
{
    public Toggle BirdToggle;
    public GameObject MamaBirdPrefab;
    public GameObject BabyBirdPrefab;
    private GameObject SpawnedBird;
    public Camera ARCamera;
    // Start is called before the first frame update
    private List<RaycastResult> raycastResults=new List<RaycastResult>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("Mouse Down!!");

            Ray ray = ARCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray); 

            if(IsPointerOverUI(Input.mousePosition)){
                Debug.Log("Do nothing!");
            }
            else{

                SpawnedBird = Instantiate(WhichBird(),ray.origin,Quaternion.identity);
                SpawnedBird.GetComponent<Rigidbody>().AddForce(ray.direction*100);
            }
        }
    }

    public GameObject WhichBird(){
        if(BirdToggle.isOn){
            return MamaBirdPrefab;
        }
        else{
            return BabyBirdPrefab;
        }
    }

    private bool IsPointerOverUI(Vector2 fingerPosition){
        PointerEventData eventDataPosition=new PointerEventData(EventSystem.current);
        eventDataPosition.position=fingerPosition;
        EventSystem.current.RaycastAll(eventDataPosition,raycastResults);
        return raycastResults.Count>0;
    }

}

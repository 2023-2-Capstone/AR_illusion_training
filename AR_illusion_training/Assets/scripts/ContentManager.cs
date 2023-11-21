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

                Vector3 spawnPosition = ARCamera.transform.position + ARCamera.transform.forward * 10; // 카메라로부터 10미터 앞
                //Vector3 spawnPosition = ARCamera.transform.position - ARCamera.transform.right; 오른쪽으로 이동
                //Vector3 spawnPosition = ARCamera.transform.position - ARCamera.transform.left; 왼쪽으로 이동
                SpawnedBird = Instantiate(WhichBird(), spawnPosition, Quaternion.identity);
                Destroy(SpawnedBird, 3f); // 3초 후에 파괴
                // SpawnedBird.GetComponent<Rigidbody>().AddForce(ray.direction * 100);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;


public class ContentManager : MonoBehaviour
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _startTracking(string objectName);
#endif
    public Toggle BirdToggle;
    public GameObject MamaBirdPrefab;
    public GameObject BabyBirdPrefab;
    private GameObject SpawnedBird;
    private GameObject SpawnEffect;
    public Camera ARCamera;
    // Start is called before the first frame update
    private List<RaycastResult> raycastResults=new List<RaycastResult>();
    float Velocity;
    bool isTracking=false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;


    public void StartSpeedTracking()
    {
        if (this.isTracking == false)
        {
            this.isTracking = true;
#if UNITY_IOS
            Debug.Log($"GameObjectName: {gameObject.name}");
            _startTracking(gameObject.name);
#else
            Debug.Log("No iOS Device Found");
#endif
        }

    }
    // public void SpeedCallBackMethod(string speed)
    // {
    //     double num=double.Parse(speed);
    //     Debug.Log($"유니티에서 받은 스피드: {speed}");
    //     if (num > 3) {
    //             Vector3 spawnPosition = ARCamera.transform.position + ARCamera.transform.up*10+ARCamera.transform.right*10; //카메라에서 위쪽으로 이동
    //             //Vector3 spawnPosition = ARCamera.transform.position - ARCamera.transform.right; 오른쪽으로 이동
    //             //Vector3 spawnPosition = ARCamera.transform.position - ARCamera.transform.left; 왼쪽으로 이동
    //             // SpawnedBird = Instantiate(WhichBird(), spawnPosition, Quaternion.identity);
    //             SpawnedBird = Instantiate(MamaBirdPrefab, spawnPosition, Quaternion.Euler(90, 0, 0));
    //             SpawnEffect = Instantiate(BabyBirdPrefab,ARCamera.transform.position-ARCamera.transform.up,Quaternion.Euler(0,0,0));
                
    //             StartCoroutine(FadeOut(SpawnedBird, 3f)); // 3초 동안 페이드 아웃
    //             StartCoroutine(FadeOut(SpawnEffect, 3f));
    //             // SpawnedBird.GetComponent<Rigidbody>().AddForce(ray.direction * 100);
    //         // 이펙트 생성
    //     }
    // }
    public void SpeedCallBackMethod(string speed)
    {
        double num = double.Parse(speed);
        Debug.Log($"유니티에서 받은 스피드: {speed}");
        if (num > 3) {
            Vector3 spawnPosition = initialPosition + Vector3.up*10; // 시작 위치 바로 위

            

            SpawnedBird = Instantiate(MamaBirdPrefab, spawnPosition, Quaternion.Euler(90, 0, 0));
            SpawnEffect = Instantiate(BabyBirdPrefab, ARCamera.transform.position-ARCamera.transform.up, Quaternion.Euler(0,0,0));
                    
            StartCoroutine(FadeOut(SpawnedBird, 3f)); // 3초 동안 페이드 아웃
            StartCoroutine(FadeOut(SpawnEffect, 3f));
        }
    }
    void Start()
    {
        initialPosition = ARCamera.transform.position;
        initialRotation = ARCamera.transform.rotation;
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
                StartSpeedTracking();
                // Vector3 spawnPosition = ARCamera.transform.position + ARCamera.transform.up*5; //카메라에서 위쪽으로 이동
                // //Vector3 spawnPosition = ARCamera.transform.position - ARCamera.transform.right; 오른쪽으로 이동
                // //Vector3 spawnPosition = ARCamera.transform.position - ARCamera.transform.left; 왼쪽으로 이동
                // // SpawnedBird = Instantiate(WhichBird(), spawnPosition, Quaternion.identity);
                // SpawnedBird = Instantiate(MamaBirdPrefab, spawnPosition, Quaternion.Euler(90, 0, 0));
                // SpawnEffect = Instantiate(BabyBirdPrefab,ARCamera.transform.position-ARCamera.transform.up,Quaternion.Euler(0,0,0));
                
                // StartCoroutine(FadeOut(SpawnedBird, 3f)); // 3초 동안 페이드 아웃
                // StartCoroutine(FadeOut(SpawnEffect, 3f));
                // // SpawnedBird.GetComponent<Rigidbody>().AddForce(ray.direction * 100);
            }
        }
    }

    // public GameObject WhichBird(){
    //     if(BirdToggle.isOn){
    //         return MamaBirdPrefab;
    //     }
    //     else{
    //         return BabyBirdPrefab;
    //     }
    // }

    private bool IsPointerOverUI(Vector2 fingerPosition){
        PointerEventData eventDataPosition=new PointerEventData(EventSystem.current);
        eventDataPosition.position=fingerPosition;
        EventSystem.current.RaycastAll(eventDataPosition,raycastResults);
        return raycastResults.Count>0;
    }
    IEnumerator FadeOut(GameObject obj, float duration)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;
            Color initialColor = mat.color;
            for (float t = 0; t < 1; t += Time.deltaTime / duration)
            {
                Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, Mathf.Lerp(1, 0, t));
                mat.color = newColor;
                yield return null;
            }
        }
        Destroy(obj);
    }
}
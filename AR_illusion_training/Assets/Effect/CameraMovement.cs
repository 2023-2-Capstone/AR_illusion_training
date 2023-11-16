using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; // AR Foundation 네임스페이스

[RequireComponent(typeof(ARCameraManager))] // AR 카메라 매니저 컴포넌트
public class CameraMovement : MonoBehaviour
{
    public ParticleSystem particleEffect; // 에디터에서 할당할 입자 시스템.
    private ARCameraManager arCameraManager;
    private Vector3 previousPosition; // 이전 프레임에서의 카메라 위치.
    public float movementThreshold = 0.05f; // 입자 효과를 발생시키기 위한 움직임 임계값.

    void Start()
    {
        arCameraManager = GetComponent<ARCameraManager>();
        previousPosition = arCameraManager.transform.position;
    }

    void Update()
    {
        // 카메라의 움직임을 계산
        float movement = Vector3.Distance(previousPosition, arCameraManager.transform.position);

        // 움직임이 임계값 이상일 경우 입자 시스템을 활성화
        if (movement > movementThreshold)
        {
            if (!particleEffect.isPlaying)
            {
                particleEffect.Play();
            }
        }
        else
        {
            if (particleEffect.isPlaying)
            {
                particleEffect.Stop();
            }
        }

        // 현재 위치를 이전 위치로 업데이트
        previousPosition = arCameraManager.transform.position;
    }
}



















// using System.Collections; //파티클 시스템
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraMovement : MonoBehaviour
// {
//     private Vector3 lastPosition;
//     public ParticleSystem movementParticles; //파티클시스템 빛 입자가 나오는 시스템
//     void Start(){
//         lastPosition=transform.position; // 게임 시작할 때, 사용자 위치 할당
//         if(movementParticles.isPlaying) //파티클 시스템이 재생중이라면 파티클 시스템을 동작하지 않게 초기화함.
//             movementParticles.Stop();
//     }
//     //언리얼이랑 똑같음 update함수를 계속해서 실행하는듯 근데 tick이 없어도 그냥 update만해도 실행이 되는지 실행해봐야 알듯함.
//     void Update(){
//         // 카메라의 이동이 0.01f이상인지 확인함s
//         if(Vector3.Distance(lastPosition,transform.position)>0.01f){
//             if(!movementParticles.isPlaying)
//                 movementParticles.Play(); //움직임 감지되면 파티클 시스템 재생, 빛 입자가 보일거임
//         }
//         else{
//             if(movementParticles.isPlaying)
//                 movementParticles.Stop(); // 파티클 시스템 정지.
//         }
//         // 현재 위치를 갱신
//         lastPosition=transform.position;

//     }
    
// }

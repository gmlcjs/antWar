using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaterCamara : MonoBehaviour
{
     // 시네마신 안의 Body를 설정함으로서 이건 사용안함
    //public GameObject Target;               // 카메라가 따라다닐 타겟

    //public float offsetX = 0.0f;            // 카메라의 x좌표
    //public float offsetY = 10.0f;           // 카메라의 y좌표
    //public float offsetZ = 0.0f;          // 카메라의 z좌표

    //public float CameraSpeed = 10.0f;       // 카메라의 속도
    //Vector3 TargetPos;                      // 타겟의 위치

    //void FixedUpdate()
    //{
    //    // 타겟의 x, y, z 좌표에 카메라의 좌표를 더하여 카메라의 위치를 결정
    //    TargetPos = new Vector3(
    //        Target.transform.position.x + offsetX,
    //        Target.transform.position.y + offsetY,
    //        Target.transform.position.z + offsetZ
    //    );

    //    // 카메라의 움직임을 부드럽게 하는 함수(Lerp)
    //    transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
    //}

}

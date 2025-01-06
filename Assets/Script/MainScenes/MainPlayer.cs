using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{

    public float rotationSpeed = 50f; // 회전 속도 조정
    public string childName = "Player"; // 회전시킬 자식의 이름 (이름을 지정하여 찾음)

    // Update is called once per frame
    void Update()
    {
        // 모든 자식 오브젝트에 대해 반복문 실행
        foreach (Transform child in transform)
        {
            // 현재 자식 오브젝트의 회전 값 가져오기
            Vector3 currentRotation = child.localEulerAngles;

            // Y축 회전 값 계산
            float newYRotation = currentRotation.y + rotationSpeed * Time.deltaTime;

            // 새로운 회전 값 설정 (Y축만 변경)
            child.localEulerAngles = new Vector3(currentRotation.x, newYRotation, currentRotation.z);
        }

    }
}

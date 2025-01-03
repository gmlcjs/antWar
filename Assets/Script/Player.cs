using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed; // 이동 스피드
    public float rotationSpeed; // 회전속도
    public string name; // 이름

    /// <summary> 문제점
    /// 1. 움직이는것을 카메라기준이 아닌 플레이어기준으로 이동할것.
    /// 2. 움직이면서 회전이안됨
    /// </summary>

    void Update()
    {
        // PC 환경에서 키보드 입력을 사용하는 이동
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer){MoveWithKeyboard();}
        // 모바일 환경에서 터치 입력을 사용하는 이동
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer){MoveWithTouch();}

    }
    // PC 환경에서 키보드 입력을 통한 이동
    private void MoveWithKeyboard()
    {
        float vertical =  Input.GetAxis("Vertical");      // W/S 또는 화살표 위아래
        Vector3 direction = new Vector3(0f, 0f, vertical).normalized; // 좌우 / 앞뒤 / z값

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        float horizontal = Input.GetAxis("Horizontal");  // A/D 또는 화살표 좌우
         // 입력값에 따라 회전 (좌우)
        if (horizontal != 0f)
        {
            Rotate(horizontal);
        }

    }

    // 모바일 환경에서 터치 입력을 통한 이동
    private void MoveWithTouch(){}


    // 회전 함수
    private void Rotate(float direction)
    {
        // Y축 회전만 수정 (오른쪽/왼쪽 회전)

        // 기존 회전 값에서 Y값만 수정하여 회전
        transform.Rotate(Vector3.right, direction * rotationSpeed * Time.deltaTime);

    }
}

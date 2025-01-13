using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButPlayer : MonoBehaviour
{
    [Header("플레이어 설정")]
    public float playerSpeed = 5f; // 플레이어 이동 속도

    private Rigidbody rb; // Rigidbody 참조
    private Vector2 moveInput; // 이동 입력 벡터

    [Header("버튼 키")]
    private bool isMovingUp = false;
    private bool isMovingDown = false;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void OnMove(InputAction.CallbackContext value)
    {
        // 입력 벡터 설정
        moveInput = value.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        // 입력 벡터를 이용한 이동 방향
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 movement = direction * playerSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    // 버튼 위로이동
    public void StartMoveUp() => moveInput = new Vector2(0, 1);
    public void StopMoveUp() => moveInput = new Vector2(0, 0);
    // // 버튼 왼쪽 이동
    // public void StartMoveLeft() => moveInput = new Vector2(-1, 0);
    // public void StopMoveLeft() => moveInput = new Vector2(0, 0);
    // // 버튼 오른쪽 이동
    // public void StartMoveRight() => moveInput = new Vector2(1, 0); 
    // public void StopMoveRight() => moveInput = new Vector2(0, 0); 
    // //버튼 아래 이동
    // public void StartMoveDown() => moveInput = new Vector2(0, -1);
    // public void StopMoveDown() => moveInput = new Vector2(0, -1);


}

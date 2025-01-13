using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerS : MonoBehaviour
{
    [Header("플레이어 설정")]
    public float playerSpeed = 5f; // 플레이어 이동 속도

    private Rigidbody rb; // Rigidbody 참조
    private Vector2 moveInput; // 이동 입력 벡터

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

    
}

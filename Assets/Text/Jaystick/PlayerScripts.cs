using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScripts : MonoBehaviour
{
    [Header("플레이어 설정")]
    public float playerSpeed; // 플레이어 이동 속도

    [Header("플레이어 컴포넌트")]
    private Rigidbody rb; // Rigidbody 참조  

    [SerializeField] private Jaystick joystick; // 조이스틱


    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        // 플레이어 이동 함수 호출
        Vector3 direction = new Vector3(joystick.InputVector2.x, 0, joystick.InputVector2.y);   
        Vector3 movement = direction * playerSpeed * Time.deltaTime; // * update에서 사용시 Time.deltaTime 제거 필요
        rb.MovePosition(transform.position + movement);        
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using System.Security.Cryptography;

public class Player : MonoBehaviour
{
    [Header("플레이어 설정")]
    [SerializeField] private string playerID = ""; // 고유 플레이어 ID  
    private static HashSet<int> usedPlayerIDs = new HashSet<int>(); // 사용된 플레이어 ID 목록  

    public string name; // 캐릭터 이름  
    public float moveSpeed; // 이동 속도  
    public float rotationSpeed; // 회전 속도  
    public float rigidbodyMass; // Rigidbody 질량 설정  

    [Header("컴포넌트 참조")]
    private Rigidbody rb; // Rigidbody 참조  
    private GameObject bodyObject; // 플레이어 body 오브젝트 참조  

    [Header("입력 값")]
    private Vector2 movementInput; // 사용자 입력에 따른 이동 방향  
    private float vertical = 0; // Z축 입력 값  
    private float horizontal = 0; // X축 입력 값  

    [Header("상태 변수")]
    private bool hasEntered = false; // 충돌 상태 체크  


    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerID = GetUniquePlayerID().ToString(); //랜덤값 부여
    }
    private void Start()
    {
        // 바디오브젝트 선언
        Transform bodyTransform = transform.Find("body");
        bodyObject = bodyTransform.gameObject;

    }
    
    // 사용자 입력에 따른 이동 방향을 저장
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        // 🚗 1. 전후 이동 (Z축 기준)
        Vector3 move = transform.forward * movementInput.y * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // 🚗 2. 좌우 회전 (Y축 기준)
        float turn = movementInput.x * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

   

    // 사용자 입력에 따른 회전 방향을 저장
    public void OnLockBack(InputAction.CallbackContext context)
    {
        // 뒤로 보기
        if (context.performed)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y +180f, 0f);
            transform.rotation = targetRotation;
        }
    }

    // 고유한 playerID 생성 함수
    private int GetUniquePlayerID()
    {
        int id;
        do
        {
            //id = Random.Range(1, 3);  // Random.Range(1, 3)는 1과 2 두 가지 숫자만 생성  3개 이상의 Player 객체가 존재하면 중복 ID를 피할 수 없어서 무한 루프에 빠짐
            id = Random.Range(1, 1001);  // 1부터 1000 사이의 랜덤 숫자 생성 
        } while (usedPlayerIDs.Contains(id));  // 이미 사용된 ID라면 다시 생성

        usedPlayerIDs.Add(id);  // 새로 생성된 ID를 사용 목록에 추가
        return id;  // 고유한 ID 반환
    }


    async void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("Player") && collider.gameObject.name == "head")
        {
            gameObject.SetActive(false);
            await Task.Delay((int)(10000));
        }

    }


}

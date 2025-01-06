using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System.Threading.Tasks;

public class PlayerInput : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private string playerID = "";
    private static HashSet<int> usedPlayerIDs = new HashSet<int>(); // 랜덤값을 부여하는 임시 변수
    public string name; // 캐릭터 이름
    public float moveSpeed; // 이동 스피드
    public float rotationSpeed; // 회전속도
    public float rigidbodyMass; // 리기드바디 질량설정

    private GameObject bodyObject; // 플레이어 body 오브젝트 참조

    [Header("이동 키")]
    float vertical = 0; // Z축
    float horizontal = 0; // X 축

    private bool hasEntered = false;  // 충돌했는지 체크하는 변수

    Rigidbody rigidbody;

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
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerID = GetUniquePlayerID().ToString(); //랜덤값 부여
    }
    private void Start()
    {
        // 바디오브젝트 선언
        Transform bodyTransform = transform.Find("body");
        bodyObject = bodyTransform.gameObject;

    }

    void Update()
    {

        // PC 환경에서 키보드 입력을 사용하는 이동
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer) {
            vertical = Input.GetAxis("Vertical"); // Z축
            horizontal = Input.GetAxis("Horizontal"); // X 축
        }
        // 모바일 환경에서 터치 입력을 사용하는 이동
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer){
            //MoveWithTouch();
        }

    }

    private void FixedUpdate()
    {
        // 이동 
        MoveWithKeyboard();

        // 충돌 확인 로직
        Crash();
    }

    // PC 환경에서 키보드 입력을 통한 이동
    private void MoveWithKeyboard()
    {

        // 앞뒤 이동 Z
        Vector3 moveDirection = transform.forward * vertical * moveSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + moveDirection);


        // 좌우 회전 (Y축 기준)
        if (horizontal != 0f)
        {
            transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime, Space.Self);
        }

    }

    // 모바일 환경에서 터치 입력을 통한 이동
    private void MoveWithTouch(){}

    void Crash()
    {
        // 바디에 대미지가 가해졌을때,

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

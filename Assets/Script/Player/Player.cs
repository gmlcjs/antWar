using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Photon.Pun;

public class Player : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    public int score = 0 ; // // 플레이어 점수 업데이트 시 호출될 함수
    [Header("플레이어 설정")]
    [SerializeField] private string playerID = ""; // 플레이어 고유 ID
    private static HashSet<int> userPlayerIDs = new HashSet<int>(); // 사용된 플레이어 ID 목록  

    public string name ="플레이어"; // 캐릭터 이름  
    public float playerSpeed = 15; // 플레이어 이동 속도
    public float rotationSpeed = 100; // 회전 속도  
    public float rigidbodyMass; // Rigidbody 질량 설정  

    [Header("컴포넌트 참조")]
    private Rigidbody rb; // Rigidbody 참조  
    // private GameObject bodyObject; // 플레이어 body 오브젝트 참조  
    private bool isUIButtonPressed = false; // UI 버튼 눌림 상태 체크 달리기때 ture

    public GameObject PlaberCanvas; // 플레이어 캠퍼스

    [Header("입력 값")]
    private Vector2 movementInput; // 사용자 입력에 따른 이동 방향  
    private float vertical = 0; // Z축 입력 값  
    private float horizontal = 0; // X축 입력 값  

    [Header("상태 변수")]
    private bool hasEntered = false; // 충돌 상태 체크  
    [SerializeField] private float moveSpeed; //  게임하는 동안의 플레이어 이동속도 

    [SerializeField] private JoystickCode joystick; // 조이스틱 ***조이스틱 클래스를 참조하기 때문에 개인별 들어가는 클래스명이 다름 
 

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerID = GetUniquePlayerID().ToString(); //랜덤값 부여
        moveSpeed = playerSpeed; 
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            PlaberCanvas.gameObject.SetActive(true);
        }
        else
        {
            PlaberCanvas.gameObject.SetActive(false);
        }
    }
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // 이 객체가 생성될 때, 내 TagObject를 Player 스크립트로 연결
        info.Sender.TagObject = this;
    }

    
    // 사용자 입력에 따른 이동 방향을 저장
    // public void OnMove(InputAction.CallbackContext context)
    // {
    //     movementInput = context.ReadValue<Vector2>();
    // }

    void FixedUpdate()
    {
        // 다른 플레이어가 조작하는 경우 이동하지 않음
        if (!photonView.IsMine)return;        

        // 쉬프트 or 버튼 달리기
        if (Input.GetKey(KeyCode.LeftShift) || isUIButtonPressed) 
        moveSpeed = playerSpeed * 1.5f; else moveSpeed = playerSpeed;

        // 🚗 1. 전후 이동 (Z축 기준)
        if (joystick.InputVector2.magnitude != 0)  // 조이스틱 이동
        {
            movementInput = new Vector2(joystick.InputVector2.x, joystick.InputVector2.y);   
        }else if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) // 키보드 이동
        {
            movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }else{ // 아무입력이없으면
            return;
        }

        Vector3 move = transform.forward * movementInput.y * moveSpeed;// rb.AddForce(move, ForceMode.VelocityChange);
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z); // Y축 속도는 유지하면서 이동

        // 🚗 2. 좌우 회전 (Y축 기준)
        float turn = movementInput.x * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
    // 버튼을 누를 때 실행 달리기,
    public void OnUIButtonDown()
    {
        isUIButtonPressed = true;
    }

    // 버튼을 뗄 때 실행 걷기
    public void OnUIButtonUp()
    {
        isUIButtonPressed = false;
    }
   


    // 고유한 playerID 생성 함수
    private int GetUniquePlayerID()
    {
        int id;
        do
        {
            //id = Random.Range(1, 3);  // Random.Range(1, 3)는 1과 2 두 가지 숫자만 생성  3개 이상의 Player 객체가 존재하면 중복 ID를 피할 수 없어서 무한 루프에 빠짐
            id = Random.Range(1, 1001);  // 1부터 1000 사이의 랜덤 숫자 생성 
        } while (userPlayerIDs.Contains(id));  // 이미 사용된 ID라면 다시 생성

        userPlayerIDs.Add(id);  // 새로 생성된 ID를 사용 목록에 추가
        return id;  // 고유한 ID 반환
    }

    // [PunRPC]
    void OnTriggerEnter(Collider collider)
    {
        if (!photonView.IsMine) return; // 내 플레이어 객체가 아니면 무시

        // 공격자의 PhotonView 찾기
        PhotonView attackerView = collider.GetComponentInParent<PhotonView>();
        if (attackerView == null || attackerView.IsMine) return; // 자기 무기면 무시

        Debug.Log($"충돌한 오브젝트 : {collider.gameObject.name}"); // 충돌한 오브젝트 이름 출력
        // 플레이어가 충돌한 오브젝트가 "damageSource" 태그를 가진 경우
        if (collider.CompareTag("damageSource") )
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.ChangeCamera();

            // 사망 UI 화면       
            GameOver gameOver = FindObjectOfType<GameOver>();
            gameOver.ShowGameOverUI(); // 게임 오버 UI 활성화

            // 관전 모드로 전환
            PhotonNetwork.LocalPlayer.TagObject = null; // 플레이어를 관전자로 설정
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "IsSpectator", true } });
            
            // gameObject.SetActive(false); // 게임 오버 UI를 비활성화
            PhotonNetwork.Destroy(this.gameObject);  // Destroy(gameObject); // 플레이어 오브젝트 삭제
        }

    }


}

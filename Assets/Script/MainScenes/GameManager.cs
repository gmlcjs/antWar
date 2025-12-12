using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    // private static GameManager m_instance; // 싱글톤이 할당될 static 변수
    public GameObject playerPrefab; // 생성할 플레이어 캐릭터 프리팹

    public Sprite[] groundSprites; // 땅 이미지 스프라이트 
    public GameObject Ground; // 땅 오브젝트
    public GameObject WatchCamera; // 관전 카메라 오브젝트

    // 외부에서 싱글톤 오브젝트를 가져올때 사용할 프로퍼티
    // public static GameManager instance
    // {
    //     get
    //     {
    //         // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
    //         if (m_instance == null)
    //         {
    //             // 씬에서 GameManager 오브젝트를 찾아 할당
    //             m_instance = FindObjectOfType<GameManager>();
    //         }

    //         // 싱글톤 오브젝트를 반환
    //         return m_instance;
    //     }
    // }

    // private void Awake() {
    //     // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
    //     if (instance != this)
    //     {
    //         // 자신을 파괴
    //         Destroy(gameObject);
    //     }
    // }

     // 게임 시작과 동시에 플레이어가 될 게임 오브젝트를 생성
    private void Start() {
        
        // 카메라 비활성화
        WatchCamera.SetActive(false);

        Vector3 center = new Vector3(0f, 1f, 0f); // 원하는 중심 위치
        // 생성할 랜덤 위치 지정
        Vector3 randomSpawnPos = Random.insideUnitSphere * 1f;
        // 위치 y값은 0으로 변경
        randomSpawnPos.y = 0f;
        Vector3 spawnPos = center + randomSpawnPos;
        // 네트워크 상의 모든 클라이언트들에서 생성 실행
        // 단, 해당 게임 오브젝트의 주도권은, 생성 메서드를 직접 실행한 클라이언트에게 있음
        var playerObject = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
        // 캐릭터 생성됨
        ChangeGroundImage(); // 땅 이미지 랜덤 교체
        // photonView.Owner.TagObject = playerObject;  // Player 객체를 TagObject에 할당
    }
    
    // 게임 땅 이미지 랜덤 교체
    public void ChangeGroundImage()
    {
        // 랜덤으로 땅 이미지 스프라이트 선택
        int randomIndex = Random.Range(0, groundSprites.Length);
        // 땅 오브젝트의 마테리얼을 랜덤으로 선택한 스프라이트로 변경
        Ground.GetComponent<Renderer>().material.mainTexture = groundSprites[randomIndex].texture;
    }

    public void ChangeCamera(){
        // photonView가 내 로컬 플레이어의 것이라면
        Debug.Log("ChangeCamera() 실행 ");
        WatchCamera.SetActive(true); // 카메라 활성화
    }

}

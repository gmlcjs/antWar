using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Unity.VisualScripting;

public class GameOver : MonoBehaviourPun
{
    public GameObject gameOverUI; // 게임 오버 UI 오브젝트

    // 게임 오버 UI를 활성화하는 메서드
    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
    // 게임 오버 UI를 비활성화하는 메서드
    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }
    // 관전하기 버튼 클릭
    public void OnSpectateButton()
    {
        // 게임 오버 UI 숨기기
        // HideGameOverUI();
        PhotonNetwork.DestroyAll(gameObject); // 모든 네트워크 오브젝트 파괴
        // 오브젝트 삭제
        Destroy(gameObject); // 오브젝트 삭제

    }

    // Lobby 이동
    public void GoToLobby()
    {
        PhotonNetwork.DestroyAll(gameObject); // 모든 네트워크 오브젝트 파괴
        PhotonNetwork.LeaveRoom(); // 룸을 나감
        SceneManager.LoadScene("Lobby");
    }

   


  
}

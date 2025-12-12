using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime;
using TMPro;
using UnityEngine.UI; // 포톤 서비스 관련 라이브러리
using UnityEngine;

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class LobbyManager : MonoBehaviourPunCallbacks {
    private string gameVersion = "1"; // 게임 버전

    public TextMeshProUGUI connectionInfoText; // 네트워크 정보를 표시할 텍스트
    public Button joinButton; // 룸 접속 버튼 

    // 게임 실행과 동시에 마스터 서버 접속 시도
    private void Start() {
        // 접속에 필요한 정보 (게임 버전) 설정
        PhotonNetwork.GameVersion = gameVersion;
        // 설정한 정보로 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

        // 룸 접속 버튼 비활성화
        joinButton.interactable = false;
        // 접속 시도 중임을 텍스트로 표시
        connectionInfoText.text = "마스터 서버에 접속 중...";
    }

    // 마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster() {
        // 룸접속 버튼 활성화
        joinButton.interactable = true;
        // 접속 정보 표시
        connectionInfoText.text = "마스터 서버에 접속 완료";
    }

    // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause) {
        // 룸접속 버튼 비활성화
        joinButton.interactable = false;
        // 접속 정보 표시
        connectionInfoText.text = "마스터 서버 접속 실패: " + cause.ToString();
        
        Invoke("Reconnect", 1f); // 1초 후 재접속 시도

        // 마스터 서버 접속 실패 시 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
        // 접속 시도 중임을 텍스트로 표시
        connectionInfoText.text = "마스터 서버에 재 접속 중...";
    }

    // 룸 접속 시도
    public void Connect() {
        // 중복 접속 시도를 막기위해 접속 버튼 잠시 비활성화
        joinButton.interactable = false;
        // 마스터 서버에 접속 중이라면
        if (PhotonNetwork.IsConnected) {
            // 랜덤 룸에 참가 시도
            PhotonNetwork.JoinRandomRoom();
            // 접속 시도 중임을 텍스트로 표시
            connectionInfoText.text = "랜덤 룸에 참가 중...";
        } else {
            // 마스터 서버에 접속 중이 아니라면
            connectionInfoText.text = "마스터 서버에 접속 중이 아닙니다. / 마스터 서버에 재 접속 중..";
            // 마스터 서버 접속 실패 시 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
            // 접속 시도 중임을 텍스트로 표시
        }
    }

    // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message) {
        // 접속 상태 표시
        connectionInfoText.text = "빈 방이 없음, 새로운 방 생성: " + message;
        // 최대 8명을 수용한 빈 방 생성
        PhotonNetwork.CreateRoom("RoomName", new RoomOptions { MaxPlayers = 8 }); // 방 이름은 "Room"
    }

    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom() {
        // 룸에 접속 완료 후 씬 전환
        PhotonNetwork.LoadLevel("Main"); // 씬 이름은 "GameScene"
        // 접속 상태 표시
        connectionInfoText.text = "룸에 접속 완료";
    }

}
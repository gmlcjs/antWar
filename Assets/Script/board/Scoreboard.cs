using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Unity.VisualScripting;
using TMPro;

public class Scoreboard : MonoBehaviourPun
{
    // public GameObject textPrefab;  // 점수 텍스트 프리팹
    // public GameObject parentPanel; // 부모 UI 패널 (Vertical Layout Group)
    // private List<Player> players = new List<Player>(); // 플레이어들의 리스트

    // void Start()
    // {
    //     // 점수판 갱신 (게임 시작 시)
    //     UpdateScoreboard();
    // }

    // // 점수판 업데이트 함수
    // public void UpdateScoreboard()
    // {
    //     // 기존 UI 요소들을 제거
    //     foreach (Transform child in parentPanel.transform)
    //     {
    //         Destroy(child.gameObject);
    //     }

    //     // Photon 네트워크에서 모든 플레이어의 점수를 가져와서 리스트로 저장
    //     players = PhotonNetwork.PlayerList.Select(p => p.TagObject as Player).ToList();
    //     // Debug.Log("플레이어: " + players[players.Count - 1]);
    //     // Debug.Log("플레이어: " + players[0].score);

    //     // TagObject가 Player가 아닌 경우를 필터링
    //     players = PhotonNetwork.PlayerList
    //         .Select(p => p.TagObject as Player)
    //         .Where(p => p != null)  // null 필터링 추가
    //         .ToList();

    //     // 점수를 내림차순으로 정렬하고, 상위 5명만 가져옴
    //      var topPlayers = players
    //     .OrderByDescending(player => player.score)
    //     .Take(5)
    //     .ToList();
    //     Debug.Log("상위 플레이어 수: " + topPlayers.Count);

    //     // 상위 5명의 점수 UI 업데이트
    //     // foreach (var player in topPlayers)
    //     // {
    //     //     Debug.Log("플레이어 점수: " + player.score);
    //     //     Debug.Log("플레이어 이름: " + player.photonView.Owner.NickName);

    //     //     GameObject newText = Instantiate(textPrefab, parentPanel.transform);
    //     //     TextMeshProUGUI textComponent = newText.GetComponent<TextMeshProUGUI>();
    //     //     textComponent.text = player.photonView.Owner.NickName + ": " + player.score.ToString(); // 이름 + 점수 표시
    //     // }
    // }

    // // 플레이어 점수 업데이트 시 호출될 함수
    // public void OnPlayerScoreUpdated()
    // {
    //     UpdateScoreboard();
    // }
    
}

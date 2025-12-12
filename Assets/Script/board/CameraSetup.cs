using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraSetup : MonoBehaviourPunCallbacks
{
    void Start() {
        // photonView가 내 로컬 플레이어의 것이라면
        if (photonView.IsMine)
        {
            // 메인 카메라를 이 오브젝트(플레이어) 아래로 이동
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0, 13f, -8f);
            Camera.main.transform.localRotation = Quaternion.Euler(48, 0, 0); // 살짝 위에서 보는 시야
        }
    }

}

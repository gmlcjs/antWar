using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aluert : MonoBehaviour
{
    public GameObject aluert; // 알럿창 오브젝트

    // 알럿창을 띄우는 함수
    public void AluertMethod()
    {
        Debug.Log("알럿창을 띄웁니다.");
        aluert.SetActive(!aluert.activeSelf);
    }

}

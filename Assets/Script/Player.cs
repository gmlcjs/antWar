using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed; // �̵� ���ǵ�
    public float rotationSpeed; // ȸ���ӵ�
    public string name; // �̸�

    /// <summary> ������
    /// 1. �����̴°��� ī�޶������ �ƴ� �÷��̾�������� �̵��Ұ�.
    /// 2. �����̸鼭 ȸ���̾ȵ�
    /// </summary>

    void Update()
    {
        // PC ȯ�濡�� Ű���� �Է��� ����ϴ� �̵�
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer){MoveWithKeyboard();}
        // ����� ȯ�濡�� ��ġ �Է��� ����ϴ� �̵�
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer){MoveWithTouch();}

    }
    // PC ȯ�濡�� Ű���� �Է��� ���� �̵�
    private void MoveWithKeyboard()
    {
        float vertical =  Input.GetAxis("Vertical");      // W/S �Ǵ� ȭ��ǥ ���Ʒ�
        Vector3 direction = new Vector3(0f, 0f, vertical).normalized; // �¿� / �յ� / z��

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        float horizontal = Input.GetAxis("Horizontal");  // A/D �Ǵ� ȭ��ǥ �¿�
         // �Է°��� ���� ȸ�� (�¿�)
        if (horizontal != 0f)
        {
            Rotate(horizontal);
        }

    }

    // ����� ȯ�濡�� ��ġ �Է��� ���� �̵�
    private void MoveWithTouch(){}


    // ȸ�� �Լ�
    private void Rotate(float direction)
    {
        // Y�� ȸ���� ���� (������/���� ȸ��)

        // ���� ȸ�� ������ Y���� �����Ͽ� ȸ��
        transform.Rotate(Vector3.right, direction * rotationSpeed * Time.deltaTime);

    }
}

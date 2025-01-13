using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickCode : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform joystickBackground; // 조이스틱 배경
    [SerializeField] private RectTransform joystick; // 조이스틱 컨트롤 이미지
    private Vector2 inputVector; // 입력 벡터
    private float radius = 100f; // 조이스틱 배경의 반지름
    
    public Vector2 InputVector2 => inputVector; // 입력 벡터 반환


    private void Start()
    {
        // 조이스틱 배경과 조이스틱 컨트롤 이미지 RectTransform 컴포넌트 가져오기
        joystickBackground = GetComponent<RectTransform>();
         // 조이스틱 배경의 자식 오브젝트로 조이스틱 컨트롤 이미지가 있음
        joystick = transform.GetChild(0).GetComponent<RectTransform>();
    }
    // PointerDown 이벤트 발생시 호출(터치 입력 시작)
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // OnDrag 함수 호출
    }
    
    public virtual void OnDrag(PointerEventData eventData)
    { // 터치 입력이 조이스틱 배경 내부에 있으면 입력 벡터 설정
 
        Vector2 pos;
        // 터치 입력 위치를 조이스틱 배경 내부 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out pos );
        
        pos = Vector2.ClampMagnitude(pos, radius); // 입력 벡터의 크기를 조이스틱 배경의 반지름보다 작게 설정
        joystick.anchoredPosition = pos; // 조이스틱 컨트롤 이미지 이동
        inputVector = pos / radius; // 입력 벡터 설정 (0 ~ 1 사이의 값)
    }

    public void OnPointerUp(PointerEventData eventData)
    { // PointerUp 이벤트 발생시 호출(터치 입력 종료)
        inputVector = Vector2.zero; // 입력 벡터 초기화
        joystick.anchoredPosition = Vector2.zero; // 조이스틱 컨트롤 이미지 초기화
    }

}

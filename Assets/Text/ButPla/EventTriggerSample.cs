using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerSample : MonoBehaviour
{
    void Start () {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        // 이벤트 트리거에 이벤트를 추가
        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        // 이벤트의 종류를 설정
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        // 이벤트가 발생했을 때 실행할 메소드를 설정
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        //  이벤트 트리거에 이벤트를 추가
        eventTrigger.triggers.Add(entry_PointerDown);

        // 이벤트 트리거에 이벤트를 추가
        EventTrigger.Entry entry_Drag = new EventTrigger.Entry();
        // 이벤트의 종류를 설정
        entry_Drag.eventID = EventTriggerType.Drag;
        // 이벤트가 발생했을 때 실행할 메소드를 설정
        entry_Drag.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        // 이벤트 트리거에 이벤트를 추가
        eventTrigger.triggers.Add(entry_Drag);

        // 이벤트 트리거에 이벤트를 추가
        EventTrigger.Entry entry_EndDrag = new EventTrigger.Entry();
        // 이벤트의 종류를 설정
        entry_EndDrag.eventID = EventTriggerType.EndDrag;
        // 이벤트가 발생했을 때 실행할 메소드를 설정
        entry_EndDrag.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
        // 이벤트 트리거에 이벤트를 추가
        eventTrigger.triggers.Add(entry_EndDrag);
    }

    void OnPointerDown(PointerEventData data)
    { // 이벤트 발생 시 처리할 내용
        Debug.Log("Pointer Down");
    }

    void OnDrag(PointerEventData data)
    { // 이벤트 발생 시 처리할 내용
        Debug.Log("Drag");
    }

    void OnEndDrag(PointerEventData data)
    { // 이벤트 발생 시 처리할 내용
        Debug.Log("End Drag");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Joystick : MonoBehaviour
{
    
    [SerializeField] RectTransform innerCircle;
    Vector2 posInput;
    RectTransform rectTransform;
    [SerializeField] float val;
    CanvasGroup canvasGroup;
    bool gameStarted = false;
    Spawner spawner;


    private void OnDestroy()
    {
        spawner.gameStarted -= GameStarted;

    }

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        spawner.gameStarted += GameStarted;
        rectTransform = GetComponent<RectTransform>();
         canvasGroup = GetComponent<CanvasGroup>();
         DisactiveJoystick();
    }


    private void GameStarted()
    {
        gameStarted = true;
    }
    void Update()
    {
        if (!gameStarted) { return; }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase==TouchPhase.Began)
            {
                ActiveJoystick(touch);
            }
            if (touch.phase== TouchPhase.Moved)
            {
                Drag(touch);
            }
            if (touch.phase== TouchPhase.Ended)
            {
                DisactiveJoystick();
            }
        }
  
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void DisactiveJoystick()
    {
        innerCircle.anchoredPosition = Vector2.zero;
        posInput = Vector2.zero;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }

    private void ActiveJoystick(Touch touch)
    {
        if (IsPointerOverUIObject()) { return; }
        transform.position = touch.position;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    public void Drag(Touch touch)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
             rectTransform,
             touch.position,
             null,
             out posInput))
        {
            posInput.x = posInput.x / (rectTransform.sizeDelta.x/2);
            posInput.y = posInput.y/  (rectTransform.sizeDelta.y/2);

        }

        if (posInput.magnitude > 1f)
        {
            posInput = posInput.normalized;
        }
       innerCircle.anchoredPosition = new Vector2(posInput.x * rectTransform.sizeDelta.x/val, posInput.y * rectTransform.sizeDelta.y/val);

    }

    public Vector3 GetDirection()
    {
        return new Vector3(posInput.x, 0, posInput.y);

    }

 
}

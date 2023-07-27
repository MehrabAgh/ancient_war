using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image bgImg, joystick;
    static Vector3 inputVector, startVector;
    public static float rotationCharacter;
    PointerEventData EventData;

    void Start()
    {
        bgImg = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
        startVector = bgImg.rectTransform.position;
    }
    void FixedUpdate()
    {       
        if (Input.touchCount >= 1)
        {
            Vector2 pos = new Vector2();
            var touch = Input.touches[0];
            if (touch.position.x < Screen.width / 3 && touch.phase == TouchPhase.Began)
            {
                bgImg.rectTransform.position = touch.position;
                ActionJS(pos);
            }
            if (touch.position.x < Screen.width / 3 && touch.phase == TouchPhase.Moved)
            {
                OnDrag(EventData);
            }

        }
        else bgImg.rectTransform.position = startVector;
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        EventData = eventData;
        OnDrag(eventData);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (bgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos)) ActionJS(pos);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        joystick.rectTransform.anchoredPosition = inputVector;
    }

    public void ActionJS(Vector2 pos)
    {        
        pos.x = pos.x / bgImg.rectTransform.sizeDelta.x;
        pos.y = pos.y / bgImg.rectTransform.sizeDelta.y;
        inputVector = new Vector3(pos.x * 2, pos.y * 2, 0);
        inputVector = inputVector.magnitude > 1.0f ? inputVector.normalized : inputVector;
        rotationCharacter = inputVector.x;
        joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3), inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3));
        
    }
    public static float Horizontal()
    {
        var x = inputVector.x != 0 ? inputVector.x : 0;
        return x;
    }
    public static float Vertical()
    {
        var y = inputVector.y != 0 ? inputVector.y : 0;
        return y;
    }
}

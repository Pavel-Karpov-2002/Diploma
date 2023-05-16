using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : Singleton<MovementJoystick>
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject joystickBG;
    [SerializeField] private Vector2 joystickDirection;

    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    public Vector2 JoystickDirection => joystickDirection;

    void Start()
    {
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public void PointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickDirection = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
        {
            ChangeJoystickPosition(joystickDist);
        }
        else
        {
            ChangeJoystickPosition(joystickRadius);
        }
    }

    private void ChangeJoystickPosition(float distance)
    {
        joystick.transform.position = joystickTouchPos + joystickDirection * distance;
    }

    public void PointerUp()
    {
        joystickDirection = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        joystickBG.transform.position = joystickOriginalPos;
    }
}

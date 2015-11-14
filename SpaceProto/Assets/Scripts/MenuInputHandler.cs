using UnityEngine;
using System.Collections;

public class MenuInputHandler : MonoBehaviour {

    public enum TouchType
    {
        DRAG,
        TAP,
        NULL,
    }

    private TouchType touchType;

    public GameObject plane;

    private float touchTime = 0.0f;

    private bool isTouching = false;

    private Vector2 touchStart;
    private Vector2 touchEnd;

    private DeviceType deviceType;

	void Start () {

        deviceType = SystemInfo.deviceType;

	}
	
	void Update () {

        if (isTouching)
            touchTime += Time.deltaTime;

        if (deviceType == DeviceType.Handheld)
            GetTouchInput();
        else if (deviceType == DeviceType.Desktop)
            GetMouseInput();


        if (isTouching)
        {
            if ((touchEnd - touchStart).magnitude > 0.2f && touchType == TouchType.NULL)
            {
                touchType = TouchType.DRAG;
            }

            if (touchType == TouchType.DRAG)
            {
                Vector2 delta = touchStart - touchEnd;

                plane.transform.position += new Vector3(0, -delta.y, 0) / 50;

                touchStart = touchEnd;
            }
        }

        if (touchType == TouchType.TAP)
        {
            int layermask = 1 << 5;
            //raycast for collision with planet
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, layermask))
            {
                GameObject gameObject = hit.collider.gameObject;
                MenuLevel script = gameObject.GetComponent<MenuLevel>();
                script.Hit();
            }

            touchType = TouchType.NULL;
        }
	}

    void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isTouching = true;

                touchStart = Input.touches[0].position;
                touchEnd = touchStart;
            }

            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                touchEnd = Input.touches[0].position;
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                isTouching = false;

                touchEnd = Input.touches[0].position;

                touchType = TouchType.NULL;

                if (touchTime < 0.3f)
                {
                    touchType = TouchType.TAP;
                }

                touchTime = 0.0f;
            }
        }
    }

    void GetMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isTouching)
            {
                isTouching = true;

                touchStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                touchEnd = touchStart;
            }

            touchEnd = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        }
        else if (isTouching)
        {
            isTouching = false;

            touchEnd = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            touchType = TouchType.NULL;

            if (touchTime < 0.3f)
            {
                touchType = TouchType.TAP;
            }

            touchTime = 0.0f;
        }
    }
}

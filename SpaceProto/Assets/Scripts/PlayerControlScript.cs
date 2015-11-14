using UnityEngine;
using System.Collections;

public class PlayerControlScript : MonoBehaviour {

    Rigidbody2D playerBox;

    bool alive;

    [Range(0.0f, 20.0f)]
    public float torque;

    [Range(0.0f, 20.0f)]
    public float acceleration;

    private ParticleSystem[] boosters;

    private bool rotatingLeft = false;
    private bool rotatingRight = false;
    private bool thrusting = false;

    RuntimePlatform platform = Application.platform;

	// Use this for initialization
	void Start () {
        alive = true;
        playerBox = GetComponent<Rigidbody2D>();
        boosters = gameObject.GetComponentsInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (alive)
        {
            TakePlayerInput();
        }
	}

    void StartTurningLeft()
    {
        boosters[1].Play();
        boosters[3].Play();
        boosters[0].Stop();
        boosters[2].Stop();
        boosters[4].Stop();
    }

    void StartTurningRight()
    {
        boosters[0].Play();
        boosters[2].Play();
        boosters[1].Stop();
        boosters[3].Stop();
        boosters[4].Stop();
    }

    void StartThrusting()
    {
        for (int i = 0; i < 4; i++)
        {
            boosters[i].Stop();
        }
        boosters[4].Play();
    }

    void StopThrusters()
    {
        for (int i = 0; i < 5; i++)
        {
            boosters[i].Stop();
        }
    }

    void RotateLeft()
    {
        if (rotatingLeft == false)
        {
            rotatingLeft = true;
            StartTurningLeft();
        }
        playerBox.AddTorque(torque);
    }

    void RotateRight()
    {
        if (rotatingRight == false)
        {
            rotatingRight = true;
            StartTurningRight();
        }
        playerBox.AddTorque(-torque);
    }

    void Thrust()
    {
        if (!thrusting)
        {
            thrusting = true;
            StartThrusting();
        }

        playerBox.AddForce(playerBox.transform.up * acceleration);

        rotatingLeft = false;
        rotatingRight = false;
    }

    void TakePlayerInput()
    {
        switch(platform)
        {
            case RuntimePlatform.Android:
                TouchControls();
                break;

            default:
                MouseControls();
                break;
        }
    }

    void TouchControls()
    {
        if (Input.touchCount != 0)
        {
            if (Input.touchCount == 2)
            {
                Thrust();
            }
            else if (Input.GetTouch(0).position.x < Screen.width / 2)
            {
                RotateLeft();
            }
            else if (Input.GetTouch(0).position.x > Screen.width / 2)
            {
                RotateRight();
            }
        }
    }

    void MouseControls()
    {
        if (Input.GetMouseButtonUp(0))
        {
            rotatingLeft = false;
            thrusting = false;
            StopThrusters();
        }
        if (Input.GetMouseButtonUp(1))
        {
            rotatingRight = false;
            thrusting = false;
            StopThrusters();
        }

        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            RotateLeft();
        }
        else if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
        {
            RotateRight();
        }
        else if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            Thrust();
        }
    }
}

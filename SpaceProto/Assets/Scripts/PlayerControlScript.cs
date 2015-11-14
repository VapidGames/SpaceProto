using UnityEngine;
using System.Collections;

public class PlayerControlScript : MonoBehaviour {

    Rigidbody2D playerBox;

    bool alive;

    bool inAtmosphere;

    [Range(0.0f, 20.0f)]
    public float torque;

    [Range(0.0f, 20.0f)]
    public float acceleration;

    [Range(0.0001f, 0.1f)]
    public float accelerationBurstInverse;

    [Range(0.1f, 10.0f)]
    public float maxSpeed;

    [Range(10.0f, 400.0f)]
    public float maxAngularVelocity;

    private ParticleSystem[] boosters;

    private bool rotatingLeft = false;
    private bool rotatingRight = false;
    private bool thrusting = false;

    private float thrustingStart;

    RuntimePlatform platform = Application.platform;

	// Use this for initialization
	void Start () {
        alive = true;
        playerBox = GetComponent<Rigidbody2D>();
        boosters = gameObject.GetComponentsInChildren<ParticleSystem>();
        inAtmosphere = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (alive)
        {
            TakePlayerInput();
            CheapVelocityCap();
            AngularVelocityCap();
        }
	}

    public bool IsInAtmosphere()
    {
        return inAtmosphere;
    }

    public void EnterAtmosphere()
    {
        inAtmosphere = true;
    }

    public void LeaveAtmosphere()
    {
        inAtmosphere = false;
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

        thrustingStart = Time.time;
    }

    void StopThrusters()
    {
        for (int i = 0; i < 5; i++)
        {
            boosters[i].Stop();
        }
        thrusting = false;
        rotatingLeft = false;
        rotatingRight = false;
    }

    void RotateLeft()
    {
        if (rotatingLeft == false)
        {
            rotatingLeft = true;
            StartTurningLeft();
        }
        //playerBox.AddTorque(torque);
        playerBox.rotation += torque;
    }

    void RotateRight()
    {
        if (rotatingRight == false)
        {
            rotatingRight = true;
            StartTurningRight();
        }
        //playerBox.AddTorque(-torque);
        playerBox.rotation -= torque;
    }

    void Deccelerate()
    {

        float currentSpeed = playerBox.velocity.magnitude;
        if (currentSpeed > maxSpeed)
        {
            // apply correcting force in opposite direction

            // strength of force: magnitude - maxSpeed
            // direction of force: opposite to rigidbody velocity
            playerBox.AddForce(playerBox.velocity.normalized * -1 * (currentSpeed - maxSpeed));
        }
    }

    void CheapVelocityCap()
    {
        float currentSpeed = playerBox.velocity.magnitude;
        if (currentSpeed > maxSpeed)
        {
            playerBox.velocity = playerBox.velocity.normalized * maxSpeed;
        }
    }

    void AngularVelocityCap()
    {
        float currentAngularVelocity = playerBox.angularVelocity;
        if (currentAngularVelocity > maxAngularVelocity)
        {
            playerBox.angularVelocity = maxAngularVelocity;
        }
        if (currentAngularVelocity < -1 * maxAngularVelocity)
        {
            playerBox.angularVelocity = -1 * maxAngularVelocity;
        }
    }

    void Thrust()
    {
        if (!thrusting)
        {
            thrusting = true;
            StartThrusting();
        }

        float timeSinceStartedThrusting = Time.time - thrustingStart + accelerationBurstInverse;

        playerBox.AddForce(playerBox.transform.up * acceleration * Mathf.Max((1/timeSinceStartedThrusting), 1));
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

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                thrusting = false;
                StopThrusters();
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

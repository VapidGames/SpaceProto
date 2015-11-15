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

    [Range(0.1f, 20.0f)]
    public float maxSpeed;

    [Range(10.0f, 400.0f)]
    public float maxAngularVelocity;

    private ParticleSystem boosters;

    private ParticleSystem explosion;

    private bool thrusting = false;

    private float thrustingStart;

    RuntimePlatform platform = Application.platform;

	// Use this for initialization
	void Start () {
        alive = true;
        playerBox = GetComponent<Rigidbody2D>();
        boosters = gameObject.GetComponentInChildren<ParticleSystem>();
        explosion = GetComponent<ParticleSystem>();
        inAtmosphere = true;
        StopThrusters();
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

    public void ZeroVelocity()
    {
        playerBox.velocity = new Vector2(0, 0);
        playerBox.angularVelocity = 0;
        playerBox.rotation = 0;
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

    void StopThrusters()
    {
        thrusting = false;
        boosters.Stop();
    }

    void RotateLeft()
    {
        //playerBox.AddTorque(torque);
        playerBox.rotation += torque;
        thrusting = false;
    }

    void RotateRight()
    {
        //playerBox.AddTorque(-torque);
        playerBox.rotation -= torque;
        thrusting = false;
    }

    void StartThrusting()
    {
        boosters.Play();
    }

    public void Explode()
    {
        explosion.Play();
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

        //float timeSinceStartedThrusting = Time.time - thrustingStart + accelerationBurstInverse;

        //playerBox.AddForce(playerBox.transform.up * acceleration * Mathf.Max((1/timeSinceStartedThrusting), 1));

        playerBox.AddForce(playerBox.transform.up * acceleration);
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

    //void TouchControls()
    //{
    //    if (Input.touchCount != 0)
    //    {
    //        foreach (Touch t in Input.touches)
    //        {
    //            if (t.position.x < Screen.width/2)
    //            {
    //                if (t.phase == TouchPhase.Began)
    //                {
    //                    leftThumbDown = true;
    //                }
    //                else if (t.phase == TouchPhase.Ended)
    //                {
    //                    leftThumbDown = false;
    //                }
    //            }

    //            if (t.position.x > Screen.width/2)
    //            {
    //                if (t.phase == TouchPhase.Began)
    //                {
    //                    rightThumbDown= true;
    //                }
    //                else if (t.phase == TouchPhase.Ended)
    //                {
    //                    rightThumbDown = false;
    //                }
    //            }
    //        }
    //    }

    //    if (leftThumbDown && !rightThumbDown)
    //    {
    //        RotateLeft();
    //    }
    //    else if (rightThumbDown && !leftThumbDown)
    //    {
    //        RotateRight();
    //    }
    //    else if (rightThumbDown && leftThumbDown)
    //    {
    //        Thrust();
    //    }
    //    else
    //    {
    //        StopThrusters();
    //    }
    //}

    void TouchControls()
    {
        if (Input.touchCount != 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                thrusting = false;
                StopThrusters();
            }
            if (Input.touchCount == 2)
            {
                Thrust();
            }
            else if (Input.GetTouch(0).position.x < Screen.width / 2)
            {
                RotateLeft();
                StopThrusters();
            }
            else if (Input.GetTouch(0).position.x > Screen.width / 2)
            {
                RotateRight();
                StopThrusters();
            }
        }
    }

    void MouseControls()
    {
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            RotateLeft();
            StopThrusters();
        }
        else if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
        {
            RotateRight();
            StopThrusters();
        }
        else if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            Thrust();
        }
    }
}

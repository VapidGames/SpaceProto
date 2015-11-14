using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour
{
    Rigidbody2D playerObject;

    PlayerControlScript playerScript;

    [Range(0.0f, 5.0f)]
    public float playerVelocityInheritance;

    [Range(0.0f, 1.0f)]
    public float lerpValue;

    [Range(0.0f, 10.0f)]
    public float fixedCameraVelocity;

    public bool focusedOnPlayer;
    public Vector3 planetPosition;



    // Use this for initialization
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (focusedOnPlayer)
        {
            if (!playerScript.IsInAtmosphere())
            {
                FollowCameraUpdate();
            }
            else
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, -40), 0.15f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, planetPosition, 0.05f);
        }
    }

    public void SetInitialHeight(float initialY)
    {
        transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
    }

    void FixedCameraUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + fixedCameraVelocity, transform.position.z);
    }

    void FollowCameraUpdate()
    {
        transform.position = Vector3.Slerp(transform.position,
            new Vector3(transform.position.x, playerObject.transform.position.y + playerObject.velocity.y * playerVelocityInheritance, -40), lerpValue);
    }
}

using UnityEngine;
using System.Collections;

public class CannonScript : MonoBehaviour {

    float currentRotationTarget;
    float currentAngle;

    public GameObject laserObject;

    public bool onTheRight;

    [Range (10.0f, 50.0f)]
    public float rotationSpeed;

	// Use this for initialization
	void Start () {
        ResetTarget();
	}

    void ResetTarget()
    {
        currentRotationTarget = Random.Range(-30.0f, 30.0f);

        Shoot();
    }
	
	// Update is called once per frame
	void Update () {

        currentAngle = transform.rotation.eulerAngles.x;
        if (currentAngle > 180)
        {
            currentAngle -= 360;
        }

        if (onTheRight)
        {
            Debug.Log(currentAngle);
        }

        if (currentAngle > currentRotationTarget)
        {
            if (onTheRight)
                RotateLeft();
            else
                RotateRight();
        }
        else
        {
            if (onTheRight)
                RotateRight();
            else
                RotateLeft();
        }
	}

    void Shoot()
    {
        if (!onTheRight)
        {
            Instantiate(laserObject, transform.position - transform.forward * 3, Quaternion.AngleAxis(90 - currentAngle, transform.up));
        }
        else
        {
            Instantiate(laserObject, transform.position - transform.forward * 3, Quaternion.AngleAxis(-90 + currentAngle, transform.up));
        }
        
    }

    void RotateRight()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        if (Mathf.Abs(currentAngle - currentRotationTarget) < 0.1f)
        {
            ResetTarget();
        }
    }

    void RotateLeft()
    {
        transform.Rotate(Vector3.up, -1 * rotationSpeed * Time.deltaTime);
        if (Mathf.Abs(currentAngle - currentRotationTarget) < 0.1f)
        {
            ResetTarget();
        }
    }
}

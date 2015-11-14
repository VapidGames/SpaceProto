using UnityEngine;
using System.Collections;

public class CannonScript : MonoBehaviour {

    float currentRotationTarget;
    float currentAngle;

    public GameObject laserObject;

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

        if (currentAngle > currentRotationTarget)
        {
            RotateRight();
        }
        else
        {
            RotateLeft();
        }
	}

    void Shoot()
    {
        Instantiate(laserObject, transform.position, Quaternion.AngleAxis(90 - currentAngle, transform.up));
    }

    void RotateRight()
    {
        transform.Rotate(Vector3.up, 12.0f * Time.deltaTime);
        if (Mathf.Abs(currentAngle - currentRotationTarget) < 0.1f)
        {
            ResetTarget();
        }
    }

    void RotateLeft()
    {
        transform.Rotate(Vector3.up, -12.0f * Time.deltaTime);
        if (Mathf.Abs(currentAngle - currentRotationTarget) < 0.1f)
        {
            ResetTarget();
        }
    }
}

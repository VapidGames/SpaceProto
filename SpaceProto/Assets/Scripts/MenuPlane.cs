using UnityEngine;
using System.Collections;

public class MenuPlane : MonoBehaviour {

    public float maxHeight;
    public float minHeight;

    void LateUpdate()
    {

        if (transform.position.y > maxHeight)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, maxHeight, 0), 0.1f);
        }
        if (transform.position.y < minHeight)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, minHeight, 0), 0.1f);
        }
    }
}

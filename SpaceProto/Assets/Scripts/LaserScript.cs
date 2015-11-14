using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    public float velocity;

	// Use this for initialization
	void Start () {
	    
	}

	// Update is called once per frame
	void Update () {
        transform.position += transform.up * Time.deltaTime * velocity;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // kill player
        }
    }
}

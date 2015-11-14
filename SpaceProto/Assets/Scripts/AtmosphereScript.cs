using UnityEngine;
using System.Collections;

public class AtmosphereScript : MonoBehaviour {

    PlayerControlScript playerScript;

    public bool startingPlanet;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlScript>();
	}

    public float GetTopOfAtmosphereYPosition()
    {
        return transform.position.y + transform.lossyScale.y/2;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerScript.EnterAtmosphere();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerScript.LeaveAtmosphere();
        }
    }

}

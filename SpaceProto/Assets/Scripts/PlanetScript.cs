using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour {

    PlayerControlScript playerScript;

    AtmosphereScript atmoScript;

    LevelManager managerScript;

    public bool startingPlanet;

	// Use this for initialization
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlScript>();

        managerScript = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

	// Update is called once per frame
    void Update()
    {

    }

    public bool IsStartingPlanet()
    {
        return startingPlanet;
    }

    public void BecomeStartingPlanet()
    {
        startingPlanet = true;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    public void BecomeEndingPlanet()
    {
        startingPlanet = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !startingPlanet)
        {
            managerScript.ViewPlanet();
        }
    }
}

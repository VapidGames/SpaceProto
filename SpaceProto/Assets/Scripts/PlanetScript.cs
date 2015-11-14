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

    void UpdateAtmosphereState(bool isStartingPlanet)
    {
        atmoScript.UpdateStartingPlanet(isStartingPlanet);
    }
	
	// Update is called once per frame
    void Update()
    {

    }

    public void BecomeStartingPlanet()
    {
        startingPlanet = true;
        UpdateAtmosphereState(startingPlanet);
    }

    public void BecomeEndingPlanet()
    {
        startingPlanet = false;
        UpdateAtmosphereState(startingPlanet);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !startingPlanet)
        {
            managerScript.NextLevel();
        }
    }
}

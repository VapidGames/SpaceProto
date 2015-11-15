using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    public const int levelCount = 5;

    public LevelScript[] leveldata;

    public int currentLevel;

    public PersistentData persistentData;

    public GameObject mediumAsteroid;
    public GameObject largeAsteroid;
    public GameObject enemyShip;
    public GameObject planet;

    private List<GameObject> objects;
    private GameObject[] planets;

    private GameObject player;

    private GameObject camera;

    private GameObject go;

    public Material[] planetMats;

    public GameObject backgroundPlanet;

    public GameObject shipBattle;

	void Start () {

        objects = new List<GameObject>();

        planets = new GameObject[2];

        planets[0] = GameObject.Find("StartingPlanet");

        player = GameObject.Find("Player");

        camera = GameObject.Find("Main Camera");

        go = GameObject.Find("Canvas");

        leveldata = new LevelScript[levelCount];
        for (int i = 0; i < levelCount; ++i)
        {
            leveldata[i] = new LevelScript();
        }

		CreateLevelData ();

        persistentData = GameObject.Find("PersistentData").GetComponent<PersistentData>();

        currentLevel = persistentData.currentLevelID - 1;

        go.SetActive(false);

        planets[0].GetComponent<PlanetScript>().planetID = currentLevel;
        planets[0].GetComponent<Renderer>().material = planetMats[currentLevel];

        BuildLevel();
	}

	void Update () {
	
	}

    private void BuildLevel()
    {
        if (currentLevel == -1)
        {
			SwitchToMenu();
            return;
        }
        //get the data from levelData[currentLevel]
        float levelWidth = 20.0f;
        float levelHeight = leveldata[currentLevel].levelLength;

        int mediumAsteroids = leveldata[currentLevel].mediumAsteroids;
        int largeAsteroids = leveldata[currentLevel].largeAsteroids;
        int enemyShips = leveldata[currentLevel].enemyShips;

        if (leveldata[currentLevel].shipBattle)
        {
            GameObject ships = (GameObject)Instantiate(shipBattle, new Vector3(0, 81.7f, 17.1f), transform.rotation);

            objects.Add(ships);
        }

        for (int i = 0; i < mediumAsteroids; ++i)
        {
            float randX = Random.Range(-levelWidth / 2, levelWidth / 2);
            float randY = Random.Range(0, levelHeight);

            GameObject medium = (GameObject)Instantiate(mediumAsteroid, new Vector3(randX, randY, 0), transform.rotation);
            medium.GetComponent<AsteroidScript>().SetVelocity(new Vector2(5, 0));

            objects.Add(medium);
        }

        for (int i = 0; i < largeAsteroids; ++i)
        {
            float randX = Random.Range(-levelWidth / 2, levelWidth / 2);
            float randY = Random.Range(0, levelHeight);

            GameObject large = (GameObject)Instantiate(largeAsteroid, new Vector3(randX, randY, 0), transform.rotation);
            large.GetComponent<AsteroidScript>().SetVelocity(new Vector2(-5, 0));

            objects.Add(large);
        }

        for (int i = 0; i < leveldata[currentLevel].backgroundPlanets.Length; ++i)
        {
            GameObject newBGPlanet = (GameObject)Instantiate(backgroundPlanet, leveldata[currentLevel].backgroundPlanets[i], transform.rotation);
            newBGPlanet.transform.localScale = leveldata[currentLevel].backgroundPlanetsSize[i];
            newBGPlanet.GetComponent<Renderer>().material = leveldata[currentLevel].backgroundPlanetsMaterials[i];

            objects.Add(newBGPlanet);
        }

        GameObject newPlanet = (GameObject)Instantiate(planet, new Vector3(0, levelHeight + 85.0f, 0), transform.rotation);

        planets[1] = newPlanet;
        planets[1].GetComponent<PlanetScript>().planetID = currentLevel + 1;
        planets[1].GetComponent<Renderer>().material = planetMats[currentLevel + 1];

        camera.transform.position = new Vector3(camera.transform.position.x, 0, camera.transform.position.z);

    }

    private void DestroyLevel()
    {
        //remove all asteroids, enemy ships etc
        for (int i = objects.Count - 1; i >= 0; --i)
        {
            if (objects[i] != null)
                Destroy(objects[i]);
        }

        //move current planet and ship back to (0, 0, 0)
        Destroy(planets[0]);
        planets[0] = planets[1];
        planets[1] = null;

        planets[0].transform.position = new Vector3(0, -81.4f, 0);

        planets[0].GetComponent<PlanetScript>().BecomeStartingPlanet();

        player.transform.position = new Vector3(0, -16.5f, 0);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void NextLevel()
    {
        camera.GetComponent<CameraFollowScript>().focusedOnPlayer = true;
        DestroyLevel();
        if (currentLevel == levelCount - 1)
            currentLevel = -1;
        else
            currentLevel++;
        BuildLevel();

        if (currentLevel < levelCount && currentLevel != -1)
        {
            //persistentData.levelUnlockedStatus[currentLevel] = 1;
            //persistentData.Save();

            go.SetActive(false);
        }

        player.GetComponent<PlayerControlScript>().canMove = true;
    }

    public void ViewPlanet(int ID)
    {

        if (ID == currentLevel) {
			planets [1] = planets [0];
		}

        player.GetComponent<PlayerControlScript>().canMove = false;

        //DestroyLevel();
        CameraFollowScript script = camera.GetComponent<CameraFollowScript>();
        script.planetPosition = new Vector3(planets[1].transform.position.x, planets[1].transform.position.y, -200);
        script.focusedOnPlayer = false;

        //show ui
        go.SetActive(true);

		if (currentLevel + 1 < levelCount) {
			persistentData.levelUnlockedStatus [currentLevel + 1] = 1;
			persistentData.Save ();
		}

        currentLevel = ID - 1;

    }

    public void ResetLevel()
    {
        currentLevel--;
        NextLevel();
    }

    public void SwitchToMenu()
    {
		persistentData.SwitchTrack (0);
        Destroy(persistentData.gameObject);
        Application.LoadLevel("Menu");
    }

    void CreateLevelData()
    {
        leveldata[0].mediumAsteroids = 7;
        leveldata[0].largeAsteroids = 3;
        leveldata[0].enemyShips = 0;
        leveldata[0].levelLength = 100;
        leveldata[0].backgroundPlanets = new Vector3[2];
        leveldata[0].backgroundPlanetsSize = new Vector3[2];
        leveldata[0].backgroundPlanetsMaterials = new Material[2];
        leveldata[0].shipBattle = false;

        leveldata[0].backgroundPlanets[0] = new Vector3(43.8f, -20.9f, 225.9f);
        leveldata[0].backgroundPlanetsSize[0] = new Vector3(91.8f, 91.8f, 91.8f);
        leveldata[0].backgroundPlanetsMaterials[0] = planetMats[4];

        leveldata[0].backgroundPlanets[1] = new Vector3(-58.4f, 105.6f, 225.9f);
        leveldata[0].backgroundPlanetsSize[1] = new Vector3(139.3f, 139.3f, 139.3f);
        leveldata[0].backgroundPlanetsMaterials[1] = planetMats[3];

        leveldata[1].mediumAsteroids = 10;
        leveldata[1].largeAsteroids = 4;
        leveldata[1].enemyShips = 0;
        leveldata[1].levelLength = 100;
		leveldata[1].backgroundPlanets = new Vector3[1];
		leveldata[1].backgroundPlanetsSize = new Vector3[1];
		leveldata[1].backgroundPlanetsMaterials = new Material[1];
        leveldata[1].shipBattle = false;

        leveldata[1].backgroundPlanets[0] = new Vector3(0.9f, -13.0f, 347.3f);
        leveldata[1].backgroundPlanetsSize[0] = new Vector3(300.1463f, 300.1463f, 300.1463f);
        leveldata[1].backgroundPlanetsMaterials[0] = planetMats[6];
       
        leveldata[2].mediumAsteroids = 13;
        leveldata[2].largeAsteroids = 5;
        leveldata[2].enemyShips = 0;
        leveldata[2].levelLength = 100;
		leveldata[2].backgroundPlanets = new Vector3[0];
		leveldata[2].backgroundPlanetsSize = new Vector3[0];
		leveldata[2].backgroundPlanetsMaterials = new Material[0];
        leveldata[2].shipBattle = false;

        leveldata[3].mediumAsteroids = 16;
        leveldata[3].largeAsteroids = 6;
        leveldata[3].enemyShips = 0;
        leveldata[3].levelLength = 100;
		leveldata[3].backgroundPlanets = new Vector3[1];
		leveldata[3].backgroundPlanetsSize = new Vector3[1];
		leveldata[3].backgroundPlanetsMaterials = new Material[1];
        leveldata[3].shipBattle = false;

		leveldata[3].backgroundPlanets[0] = new Vector3(10.8f, 40.9f, 225.9f);
		leveldata[3].backgroundPlanetsSize[0] = new Vector3(120.8f, 120.8f, 120.8f);
		leveldata[3].backgroundPlanetsMaterials[0] = planetMats[1];

        leveldata[4].mediumAsteroids = 0;
        leveldata[4].largeAsteroids = 0;
        leveldata[4].enemyShips = 0;
        leveldata[4].levelLength = 100;
		leveldata[4].backgroundPlanets = new Vector3[0];
		leveldata[4].backgroundPlanetsSize = new Vector3[0];
		leveldata[4].backgroundPlanetsMaterials = new Material[0];
        leveldata[4].shipBattle = true;
    }
}

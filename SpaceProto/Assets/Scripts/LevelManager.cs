using UnityEngine;
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

	void Start () {

        objects = new List<GameObject>();

        planets = new GameObject[2];

        planets[0] = GameObject.Find("StartingPlanet");

        player = GameObject.Find("Player");

        leveldata = new LevelScript[levelCount];
        for (int i = 0; i < levelCount; ++i)
        {
            leveldata[i] = new LevelScript();
        }

        leveldata[0].mediumAsteroids = 10;
        leveldata[0].largeAsteroids = 5;
        leveldata[0].enemyShips = 0;
        leveldata[0].levelLength = 100;

        leveldata[1].mediumAsteroids = 10;
        leveldata[1].largeAsteroids = 5;
        leveldata[1].enemyShips = 0;
        leveldata[1].levelLength = 100;

        leveldata[2].mediumAsteroids = 10;
        leveldata[2].largeAsteroids = 5;
        leveldata[2].enemyShips = 0;
        leveldata[2].levelLength = 100;

        leveldata[3].mediumAsteroids = 10;
        leveldata[3].largeAsteroids = 5;
        leveldata[3].enemyShips = 0;
        leveldata[3].levelLength = 100;

        leveldata[4].mediumAsteroids = 10;
        leveldata[4].largeAsteroids = 5;
        leveldata[4].enemyShips = 0;
        leveldata[4].levelLength = 100;

        persistentData = GameObject.Find("PersistentData").GetComponent<PersistentData>();

        currentLevel = persistentData.currentLevelID - 1;

        BuildLevel();
	}

	void Update () {
	
	}

    private void BuildLevel()
    {
        if (currentLevel == -1)
        {
            Application.LoadLevel("Menu");
        }
        //get the data from levelData[currentLevel]
        float levelWidth = 20.0f;
        float levelHeight = leveldata[currentLevel].levelLength;

        int mediumAsteroids = leveldata[currentLevel].mediumAsteroids;
        int largeAsteroids = leveldata[currentLevel].largeAsteroids;
        int enemyShips = leveldata[currentLevel].enemyShips;

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

        GameObject newPlanet = (GameObject)Instantiate(planet, new Vector3(0, levelHeight + 80.0f, 0), transform.rotation);

        planets[1] = newPlanet;

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
        planets[0] = planets[1];
        planets[1] = null;

        planets[0].transform.position = new Vector3(0, -81.4f, 0);

        player.transform.position = new Vector3(0, -17.7f, 0);
    }

    public void NextLevel()
    {
        persistentData.levelUnlockedStatus[currentLevel] = 1;
        persistentData.Save();

        DestroyLevel();
        if (currentLevel == levelCount - 1)
            currentLevel = -1;
        BuildLevel();
    }
}

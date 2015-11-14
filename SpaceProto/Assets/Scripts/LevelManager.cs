using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public const int levelCount = 5;

    public LevelScript[] leveldata;

    public int currentLevel;

    public PersistentData persistentData;

    public GameObject mediumAsteroid;
    public GameObject largeAsteroid;
    public GameObject enemyShip;
    public GameObject planet;

	void Start () {

        leveldata = new LevelScript[levelCount];
        for (int i = 0; i < levelCount; ++i)
        {
            leveldata[i] = new LevelScript();
        }

        leveldata[0].mediumAsteroids = 20;
        leveldata[0].largeAsteroids = 5;
        leveldata[0].enemyShips = 0;
        leveldata[0].levelLength = 100;

        persistentData = GameObject.Find("PersistentData").GetComponent<PersistentData>();

        currentLevel = persistentData.currentLevelID - 1;

        BuildLevel();
	}

	void Update () {
	
	}

    public void BuildLevel()
    {
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
        }

        for (int i = 0; i < largeAsteroids; ++i)
        {
            float randX = Random.Range(-levelWidth / 2, levelWidth / 2);
            float randY = Random.Range(0, levelHeight);

            GameObject large = (GameObject)Instantiate(largeAsteroid, new Vector3(randX, randY, 0), transform.rotation);
            large.GetComponent<AsteroidScript>().SetVelocity(new Vector2(-5, 0));
        }

        Instantiate(planet, new Vector3(0, levelHeight + 64.0f, 0), transform.rotation);

    }

    public void DestroyLevel()
    {
        //remove all asteroids, enemy ships etc
        //leave the current planet but destoy previous planet (move previous planet?)
        //move current planet and ship back to (0, 0, 0)
    }
}

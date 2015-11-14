using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public const int levelCount = 5;

    public LevelScript[] leveldata;

    public int currentLevel;

    public PersistentData persistentData;

    public GameObject asteroid;
    public GameObject enemyShip;
    public GameObject planet;

	void Start () {

        leveldata = new LevelScript[levelCount];
        for (int i = 0; i < levelCount; ++i)
        {
            leveldata[i] = new LevelScript();
        }

        leveldata[0].asteroids = 20;
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

        int asteroids = leveldata[currentLevel].asteroids;
        int enemyShips = leveldata[currentLevel].enemyShips;

        for (int i = 0; i < asteroids; ++i)
        {
            float randX = Random.Range(-levelWidth / 2, levelWidth / 2);
            float randY = Random.Range(0, levelHeight);

            Instantiate(asteroid, new Vector3(randX, randY, 0), transform.rotation);
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

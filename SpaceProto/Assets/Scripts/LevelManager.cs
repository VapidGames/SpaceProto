using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public const int levelCount = 5;

    public LevelScript[] leveldata;

    public int currentLevel;

    public PersistentData persistentData;
	void Start () {

        leveldata = new LevelScript[levelCount];

        leveldata[0].asteroids = 20;
        leveldata[0].enemyShips = 0;
        leveldata[0].levelLength = 100;

        persistentData = GameObject.Find("PersistentData").GetComponent<PersistentData>();
        currentLevel = persistentData.currentLevelID - 1;
	}

	void Update () {
	
	}

    public void BuildLevel()
    {
        //get the data from levelData[currentLevel]

    }

    public void DestroyLevel()
    {
        //remove all asteroids, enemy ships etc
        //leave the current planet but destoy previous planet (move previous planet?)
        //move current planet and ship back to (0, 0, 0)
    }
}

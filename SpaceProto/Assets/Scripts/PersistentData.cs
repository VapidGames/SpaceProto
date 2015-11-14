using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {

    public int[] levelUnlockedStatus;

    //0 is menu
    public int currentLevelID = 0;

	void Start () {

        levelUnlockedStatus = new int[5];

        DontDestroyOnLoad(this.gameObject);

	}

	void Update () {
	
	}

    public void Save()
    {
        PlayerPrefs.SetInt("level1", levelUnlockedStatus[0]);
        PlayerPrefs.SetInt("level2", levelUnlockedStatus[1]);
        PlayerPrefs.SetInt("level3", levelUnlockedStatus[2]);
        PlayerPrefs.SetInt("level4", levelUnlockedStatus[3]);
        PlayerPrefs.SetInt("level5", levelUnlockedStatus[4]);
    }

    public void Load()
    {
        levelUnlockedStatus[0] = PlayerPrefs.GetInt("level1");
        levelUnlockedStatus[1] = PlayerPrefs.GetInt("level2");
        levelUnlockedStatus[2] = PlayerPrefs.GetInt("level3");
        levelUnlockedStatus[3] = PlayerPrefs.GetInt("level4");
        levelUnlockedStatus[4] = PlayerPrefs.GetInt("level5");
    }                                 
}                                     
                                      
                                      
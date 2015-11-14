using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {

    public int[] levels;

	void Start () {

        levels = new int[5];

        DontDestroyOnLoad(this.gameObject);

	}

	void Update () {
	
	}

    public void Save()
    {
        PlayerPrefs.SetInt("level1", levels[0]);
        PlayerPrefs.SetInt("level2", levels[1]);
        PlayerPrefs.SetInt("level3", levels[2]);
        PlayerPrefs.SetInt("level4", levels[3]);
        PlayerPrefs.SetInt("level5", levels[4]);
    }

    public void Load()
    {
        levels[0] = PlayerPrefs.GetInt("level1");
        levels[1] = PlayerPrefs.GetInt("level2");
        levels[2] = PlayerPrefs.GetInt("level3");
        levels[3] = PlayerPrefs.GetInt("level4");
        levels[4] = PlayerPrefs.GetInt("level5");
    }                                 
}                                     
                                      
                                      
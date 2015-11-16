using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {

    public int[] levelUnlockedStatus;

    //0 is menu
    public int currentLevelID = 0;

	public AudioClip menuMusic;
	public AudioClip gameMusic;

	private AudioSource source;

    void Awake()
    {
        levelUnlockedStatus = new int[10];
        Load();

        DontDestroyOnLoad(this.gameObject);
    }

	void Start()
	{
		source = transform.GetComponent<AudioSource> ();

		source.clip = menuMusic;
		source.Play ();
	}

    public void Save()
    {
        PlayerPrefs.SetInt("level1", levelUnlockedStatus[0]);
        PlayerPrefs.SetInt("level2", levelUnlockedStatus[1]);
        PlayerPrefs.SetInt("level3", levelUnlockedStatus[2]);
        PlayerPrefs.SetInt("level4", levelUnlockedStatus[3]);
        PlayerPrefs.SetInt("level5", levelUnlockedStatus[4]);
		PlayerPrefs.SetInt("level6", levelUnlockedStatus[5]);
		PlayerPrefs.SetInt("level7", levelUnlockedStatus[6]);
		PlayerPrefs.SetInt("level8", levelUnlockedStatus[7]);
		PlayerPrefs.SetInt("level9", levelUnlockedStatus[8]);
		PlayerPrefs.SetInt("level10", levelUnlockedStatus[9]);
    }

    public void Load()
    {
        levelUnlockedStatus[0] = 1;
        levelUnlockedStatus[1] = PlayerPrefs.GetInt("level2");
        levelUnlockedStatus[2] = PlayerPrefs.GetInt("level3");
        levelUnlockedStatus[3] = PlayerPrefs.GetInt("level4");
        levelUnlockedStatus[4] = PlayerPrefs.GetInt("level5");
		levelUnlockedStatus[5] = PlayerPrefs.GetInt("level6");
		levelUnlockedStatus[6] = PlayerPrefs.GetInt("level7");
		levelUnlockedStatus[7] = PlayerPrefs.GetInt("level8");
		levelUnlockedStatus[8] = PlayerPrefs.GetInt("level9");
		levelUnlockedStatus[9] = PlayerPrefs.GetInt("level10");
    }

	public void SwitchTrack(int track)
	{
		if (track == 0) {
            source.Stop();
			source.clip = menuMusic;
			source.Play ();
		} else {
            source.Stop();
			source.clip = gameMusic;
			source.Play ();
		}
	}
}                                     
                                      
                                      
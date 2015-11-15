using UnityEngine;
using System.Collections;

public class MenuLevel : MonoBehaviour {

    public string levelName;

    public bool unlocked;

    private Renderer renderer;

    public Material lockedMat;
    public Material unlockedMat;

    public int levelID;

    public PersistentData persistentData;

    void Awake()
    {
        renderer = transform.GetComponent<Renderer>();
    }

	void Start () 
    {
        if (GameObject.Find("PersistentData") != null)
            persistentData = GameObject.Find("PersistentData").GetComponent<PersistentData>();

        if (persistentData.levelUnlockedStatus[levelID - 1] == 0)
        {
            unlocked = false;
            renderer.material = lockedMat;
        }
        else
        {
            unlocked = true;
            renderer.material = unlockedMat;
        }
	}

    public void Hit()
    {
        if (unlocked)
        {
            persistentData.currentLevelID = levelID;
			persistentData.SwitchTrack(1);
            Application.LoadLevel(levelName);
        }
    }
}

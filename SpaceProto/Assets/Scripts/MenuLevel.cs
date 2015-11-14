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

        if (unlocked)
        {
            renderer.material = unlockedMat;
        }
        else
        {
            renderer.material = lockedMat;
        }
    }

	void Start () 
    {
        if (GameObject.Find("PersistentData") != null)
            persistentData = GameObject.Find("PersistentData").GetComponent<PersistentData>();

        if (persistentData.levelUnlockedStatus[levelID - 1] == 0)
            unlocked = false;
        else
            unlocked = true;
	}

    public void Hit()
    {
        if (unlocked)
        {
            persistentData.currentLevelID = levelID;
            Application.LoadLevel(levelName);
        }
    }
}

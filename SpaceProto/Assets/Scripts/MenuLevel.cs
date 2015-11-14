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

	void Start () {

        renderer = transform.GetComponent<Renderer>();

        if (unlocked)
        {
            renderer.material = unlockedMat;
        }
        else
        {
            renderer.material = lockedMat;
        }

        if (GameObject.Find("PersistentData") != null)
            persistentData = GameObject.Find("PersistentData").GetComponent<PersistentData>();

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

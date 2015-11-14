using UnityEngine;
using System.Collections;

public class MenuConnection : MonoBehaviour {

    private LineRenderer renderer;

    public GameObject planet1;
    public GameObject planet2;

    public Material locked;
    public Material unlocked;

    PersistentData persistentData;

    public int levelID;

	void Start () {
        renderer = transform.GetComponent<LineRenderer>();

        persistentData = GameObject.Find("PersistentData").GetComponent<PersistentData>();

        if (persistentData.levelUnlockedStatus[levelID - 1] == 0)
        {
            renderer.material = locked;
        }
        else
        {
            renderer.material = unlocked;
        }
	}
	
	// Update is called once per frame
	void Update () {

        renderer.SetPosition(0, planet1.transform.position);
        renderer.SetPosition(1, planet2.transform.position);

	}
}

using UnityEngine;
using System.Collections;

public class MenuLevel : MonoBehaviour {

    public string levelName;

    public bool unlocked;

    private Renderer renderer;

    public Material lockedMat;
    public Material unlockedMat;

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

	}

    public void Hit()
    {
        if (unlocked)
        {
            Application.LoadLevel(levelName);
        }
    }
}

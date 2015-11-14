using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

    CannonScript[] turrets;

    public bool onTheRight;

	// Use this for initialization
	void Start () {
        turrets = GetComponentsInChildren<CannonScript>();
	}

    void Awake()
    {
        SetTurrets();
    }

    void SetTurrets()
    {
        foreach (CannonScript script in turrets)
        {
            script.onTheRight = onTheRight;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

    CannonAnchorScript[] turrets;

    public bool onTheRight;

    public float cannonRotationSpeed;

	// Use this for initialization
	void Start () {
        turrets = GetComponentsInChildren<CannonAnchorScript>();
        SetTurrets();
	}

    void SetTurrets()
    {
        foreach (CannonAnchorScript script in turrets)
        {
            script.SetCannonOnRight(onTheRight);
            script.SetCannonRotationSpeed(cannonRotationSpeed);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

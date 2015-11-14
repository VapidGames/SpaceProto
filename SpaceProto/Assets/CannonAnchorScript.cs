using UnityEngine;
using System.Collections;

public class CannonAnchorScript : MonoBehaviour {

    private CannonScript script;

	// Use this for initialization
	void Start () {
        script = GetComponentInChildren<CannonScript>();
	}
	
    public void SetCannonOnRight(bool input)
    {
        script.onTheRight = input;
    }

    public void SetCannonRotationSpeed(float input)
    {
        script.rotationSpeed = input;
    }


}

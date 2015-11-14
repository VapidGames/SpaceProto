using UnityEngine;
using System.Collections;

public class MenuConnection : MonoBehaviour {

    private LineRenderer renderer;

    public GameObject planet1;
    public GameObject planet2;

	void Start () {
        renderer = transform.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        renderer.SetPosition(0, planet1.transform.position);
        renderer.SetPosition(1, planet2.transform.position);

	}
}

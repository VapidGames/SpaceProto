using UnityEngine;
using System.Collections;

public class NebulaScript : MonoBehaviour {

    Renderer nebulaRenderer;

    public GameObject plane;

	// Use this for initialization
	void Start () {
        nebulaRenderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        nebulaRenderer.material.mainTextureOffset = new Vector2(plane.transform.position.y * 0.001f - 0.25f, 0);
	}
}

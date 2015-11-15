using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    public float velocity;

    private ParticleSystem[] particleSystems;

	// Use this for initialization
	void Start () {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
	}

	// Update is called once per frame
	void Update () {
        transform.position += transform.up * Time.deltaTime * velocity;

        if (transform.position.x < -18 || transform.position.x > 18)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            particleSystems[0].Play();
            particleSystems[1].Play();
            GameObject.Find("LevelManager").GetComponent<LevelManager>().ResetLevel();
        }

        if (other.gameObject.tag == "BigShip")
        {
            particleSystems[0].Play();
            particleSystems[1].Play();
        }
    }
}

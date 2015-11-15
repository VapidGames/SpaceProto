using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

    public void SetVelocity(Vector2 initialVelocity)
    {
        GetComponent<Rigidbody2D>().velocity = initialVelocity;
    }

    void Update()
    {
        if (transform.position.x > 19)
        {
            transform.position = new Vector2(-18.5f, transform.position.y);
        }

        if (transform.position.x < -19)
        {
            transform.position = new Vector2(18.5f, transform.position.y);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerControlScript>().Explode();
            GameObject.Find("LevelManager").GetComponent<LevelManager>().ResetLevel();
        }
    }


}

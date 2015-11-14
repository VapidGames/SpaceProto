using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // kill player
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	void OnBecameInvisible()
    {
		Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "boss")
        {
            Debug.Log("Hit Boss: " + collision.transform.name);
            Destroy(this.gameObject);
        }
    }
}

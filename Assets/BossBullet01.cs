using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet01 : MonoBehaviour
{
	void OnBecameInvisible()
    {
		Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player: " + collision.transform.name);
            Destroy(this.gameObject);
        }
    }

}

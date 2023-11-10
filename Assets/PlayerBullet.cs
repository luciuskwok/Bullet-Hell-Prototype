using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	void OnBecameInvisible() {
		Destroy(gameObject);
	}


	void OnTriggerEnter2D(Collider2D collider) {
		// PlayerBullet instances should have "Is Trigger" checked, so they will only get OnTrigger events, and not OnCollision
		if (collider.gameObject.tag == "Boss") {
			//Debug.Log("Player bullet trigger");
			Destroy(this.gameObject);
		}
	}
}

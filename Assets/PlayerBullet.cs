using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	void OnBecameInvisible() {
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "boss") {
			Debug.Log("Player bullet collision: boss");
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log("Player bullet trigger");
	}
}

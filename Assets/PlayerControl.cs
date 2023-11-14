using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	public GameObject bullet;

	private float playerSpeed = 10f;
	private float gunSpeed = 8.0f;
	private float gunHeat = 0.0f;

	private float xMax = 3.0f;
	private float xMin = -3.0f;
	private float yMax = 4.0f;
	private float yMin = -4.0f;

	private float hitCountdown = 0.0f;
	private Color originalColor;


	void Start() {
		originalColor = GetComponent<SpriteRenderer>().color;
	}

	// Update is called once per frame
	void Update()
	{
		// Player movement
		Vector3 pos = transform.position;
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		pos += move * playerSpeed * Time.deltaTime;
		pos.x = minMax(pos.x, xMin, xMax);
		pos.y = minMax(pos.y, yMin, yMax);
		transform.position = pos;

		// Player shooting
		if (Input.GetKey(KeyCode.Space)) {
			FireBullet();
		}

		// Countdown for color change if hit
		if (hitCountdown > 0.0f) {
			hitCountdown -= Time.deltaTime;
			if(hitCountdown <= 0.0f) {
				GetComponent<SpriteRenderer>().color = originalColor;
				hitCountdown = 0.0f;
			}
		}
	}

	// Player fire bullets
	void FireBullet() {
		if (gunHeat <= 0) {
			gunHeat = 1.0f/gunSpeed;
			GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);
			Rigidbody2D body = bulletClone.GetComponent<Rigidbody2D>();
			body.velocity = new Vector3(0.0f, 10.0f, 0.0f);
			//Debug.Log("Player fired bullet");
		} else {
			gunHeat -= Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag != "PlayerBullet") {
			Debug.Log("Player has hit enemy or enemy bullet.");
			
			// Visual indication that player was hit
			GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			hitCountdown = 1.0f/4.0f;
		}
	}

	// ## Utilities
	// Apply minimum and maximum limits
	float minMax(float x, float min, float max)
	{
		x = x > min ? x : min;
		return x < max ? x : max;
	}
}

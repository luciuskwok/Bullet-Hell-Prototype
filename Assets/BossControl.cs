using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
	public GameObject bullet;

	private float gunSpeed = 2.0f;
	private float gunHeat = 0.0f;

	private int currentPattern = 0;
	private int patternCountdown = 0;
	private float gunAngle = 0.0f;
	private float bulletSpeed = 4.0f;

	private int hitPoints = 8 * 60; // 8 shots/second for 60 seconds

	void Start() {
		InitializeFiringPattern();
	}

	void Update()
	{
		if (gunHeat <= 0) {
			gunHeat = 1.0f/gunSpeed;
			FireBullets();
		} else {
			gunHeat -= Time.deltaTime;
		}
	}

	void FireBullets() {
		if (currentPattern == 0) {
			FireBulletWithAngleSpeed(gunAngle, bulletSpeed);
			FireBulletWithAngleSpeed(gunAngle + 90.0f, bulletSpeed);
			FireBulletWithAngleSpeed(gunAngle + 180.0f, bulletSpeed);
			FireBulletWithAngleSpeed(gunAngle + 270.0f, bulletSpeed);

			gunAngle += 10.0f;
			gunAngle = gunAngle < 180.0f? gunAngle : gunAngle-360.0f;
		} else if (currentPattern == 1) {
			for (float i = 0.0f; i<360.0f; i+=45.0f) {
				FireBulletWithAngleSpeed(gunAngle + i, bulletSpeed);
			}

			gunAngle += 20.0f;
			gunAngle = gunAngle < 180.0f? gunAngle : gunAngle-360.0f;
		} else { // currentPattern >= 2
			for (float i = 0.0f; i<360.0f; i+=20.0f) {
				FireBulletWithAngleSpeed(gunAngle + i, bulletSpeed);
			}
			gunAngle += 7.5f;
		}

		// Advance countdown to next pattern
		patternCountdown--;
		if (patternCountdown <= 0) {
			// Advance to next pattern
			currentPattern++;
			currentPattern = currentPattern<3? currentPattern : 0;

			// Reset gun settings
			InitializeFiringPattern();
		}
	}

	void InitializeFiringPattern() {
		// Settings for specific patterns
		if (currentPattern == 0) {
			patternCountdown = 16;
			gunAngle = 10.0f;
			gunSpeed = 2.0f;
			gunHeat = 3.0f; // delay before this pattern starts
		} else if (currentPattern == 1) {
			patternCountdown = 16;
			gunAngle = 0.0f;
			gunSpeed = 4.0f;
			gunHeat = 2.0f; // delay before this pattern starts
		} else {  // currentPattern >= 2
			patternCountdown = 4;
			gunAngle = 0.0f;
			gunSpeed = 0.5f;
			gunHeat = 2.0f; // delay before this pattern starts
		}

	}

	void FireBulletWithAngleSpeed(float angleDeg, float speed) {
		GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
		Rigidbody2D body = clone.GetComponent<Rigidbody2D>();
		body.velocity = VectorWithAngleSpeed(angleDeg, speed);
	}

	Vector3 VectorWithAngleSpeed(float angleDeg, float speed) {
		float x = speed * Mathf.Cos(angleDeg*Mathf.PI/180.0f);
		float y = speed * Mathf.Sin(angleDeg*Mathf.PI/180.0f);
		return new Vector3(x, y, 0.0f);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log("Boss collision");
	}

	void OnTriggerEnter2D(Collider2D collider) {
		string tag = collider.gameObject.tag;
		if (tag == "Player") {
			Debug.Log("Boss was hit by player");
			DecreaseHitPoints(1);
		} else if (tag == "PlayerBullet") {
			Debug.Log("Boss was hit by player bullet");
			DecreaseHitPoints(1);
		}
	}

	void DecreaseHitPoints(int x) {
		hitPoints -= x;
		if (x <= 0) {
			Debug.Log("Boss was defeated");
			Destroy(this.gameObject);
        }
    }

}

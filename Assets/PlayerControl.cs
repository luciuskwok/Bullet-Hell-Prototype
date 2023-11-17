using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
	public GameObject bullet;
	public Image livesImage;

	public int livesLeft = 3;

	private float playerSpeed = 10f;
	private float gunSpeed = 8.0f;
	private float gunHeat = 0.0f;

	private float xMax = 8.5f;
	private float xMin = -8.5f;
	private float yMax = 8.5f;
	private float yMin = -8.5f;
	private Vector3 initialPosition;

	private float hitCountdown = 0.0f;
	private Color originalColor;
	private Color clearColor;

	private float invincibilityCountdown = 0.0f;
	private float hiddenCountdown = 0.0f;


	void Start() {
		originalColor = GetComponent<SpriteRenderer>().color;
		clearColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		initialPosition = transform.position;
		UpdatePlayerLivesImage();
	}

	// Update is called once per frame
	void Update()
	{
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();

		if (livesLeft < 0) return;

		if (hiddenCountdown > 0.0f) {
			// Player hidden after dying
			hiddenCountdown -= Time.deltaTime;
			if (hiddenCountdown <= 0.0f) {
				hiddenCountdown = 0.0f;

				// Deduct a life and decide if it's game over
				livesLeft--;
				if (livesLeft >= 0) {
					renderer.color = originalColor;
					UpdatePlayerLivesImage();

					// Start invincibility
					invincibilityCountdown = 3.0f; // 3 seconds of invincibility
				} else {
					LoadGameOverScene();
				}
			}
		} else {
			// Color change if hit
			if (hitCountdown > 0.0f) {
				hitCountdown -= Time.deltaTime;
				if (hitCountdown <= 0.0f) {
					hitCountdown = 0.0f;
					
					// Hide player for 3 seconds and reset position
					renderer.color = clearColor;
					hiddenCountdown = 3.0f;
					transform.position = initialPosition;
				}
			} else {
				// Player movement
				Vector3 pos = transform.position;
				Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
				pos += move * playerSpeed * Time.deltaTime;
				pos.x = MinMax(pos.x, xMin, xMax);
				pos.y = MinMax(pos.y, yMin, yMax);
				transform.position = pos;

				// Player shooting
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) {
					FireBullet();
				}
			}

			// Invincibility effect
			if (invincibilityCountdown > 0.0f) {
				Color color = renderer.color;
				color.a = 0.25f + (invincibilityCountdown * 8.0f % 1.0f) * 0.75f;
				renderer.color = color;
				invincibilityCountdown -= Time.deltaTime;
				if (invincibilityCountdown <= 0.0f) {
					renderer.color = originalColor;
					invincibilityCountdown = 0.0f;
				}
			}
		}
	}

	// Player fire bullets
	void FireBullet() {
		if (gunHeat <= 0 && CanShoot()) {
			gunHeat = 1.0f/gunSpeed;
			GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);
			Rigidbody2D body = bulletClone.GetComponent<Rigidbody2D>();
			body.velocity = new Vector3(0.0f, 16.0f, 0.0f);
			//Debug.Log("Player fired bullet");
		} else {
			gunHeat -= Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag != "PlayerBullet") {
			Debug.Log("Player has hit enemy or enemy bullet.");
			
			// Visual indication that player was hit
			if (CanBeHit()) {
				GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
				hitCountdown = 1.0f/4.0f;
			}
		}
	}

	bool CanShoot() {
		return hitCountdown <= 0.0 && hiddenCountdown <= 0.0;
	}

	bool CanBeHit() {
		return hitCountdown <= 0.0 && invincibilityCountdown <= 0.0 && hiddenCountdown <= 0.0;
	}

	void UpdatePlayerLivesImage() {
		livesImage.rectTransform.sizeDelta = new Vector2(52.0f * livesLeft, 36.0f);
	}


	public void LoadGameOverScene() {
		SceneManager.LoadScene("GameOverScene");
	}

	// ## Utilities
	// Apply minimum and maximum limits
	float MinMax(float x, float min, float max)
	{
		x = x > min ? x : min;
		return x < max ? x : max;
	}
}

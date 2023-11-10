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

	// Start is called before the first frame update
	void Start()
	{
		
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
	}

	// Player fire bullets
	void FireBullet() {
		if (gunHeat <= 0)
		{
			gunHeat = 1.0f/gunSpeed;
			Instantiate(bullet, transform.position, Quaternion.identity);
		} else
        {
			gunHeat -= Time.deltaTime;
        }
	}


	// Apply minimum and maximum limits
	float minMax(float x, float min, float max)
    {
		x = x > min ? x : min;
		return x < max ? x : max;
    }
}

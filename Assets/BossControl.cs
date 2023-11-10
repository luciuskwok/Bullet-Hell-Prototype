using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
	public GameObject bullet;

	private float gunSpeed = 4.0f;
	private float gunHeat = 0.0f;


	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
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
		GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
		Rigidbody2D body = clone.GetComponent<Rigidbody2D>();
		body.velocity = new Vector3(0.0f, -4.0f, 0.0f);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	float speed = 10f;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	void Update()
	{
		transform.position += new Vector3(0, speed * Time.deltaTime, 0);
	}

	void OnBecameInvisible()
    {
		Destroy(gameObject);
    }
}

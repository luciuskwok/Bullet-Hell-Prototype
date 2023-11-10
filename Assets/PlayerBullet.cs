using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	void OnBecameInvisible()
    {
		Destroy(gameObject);
    }
}

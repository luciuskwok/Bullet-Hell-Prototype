using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet01 : MonoBehaviour
{

	void OnBecameInvisible()
    {
		Destroy(gameObject);
    }

}

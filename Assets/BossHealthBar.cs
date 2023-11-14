using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
	public Image healthBarImage;

	public void UpdateHealthBar(float hp, float total) {
		healthBarImage.fillAmount = Mathf.Clamp(hp / total, 0, 1.0f);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSceneControl : MonoBehaviour
{
	public Graphic pressKeyGraphic;

	private float keyDisableTimer;

	void Start() {
		// Hide the Press Space prompt until timer is done
		keyDisableTimer = 3.0f;
		SetGraphicAlpha(pressKeyGraphic, 0.0f);
	}

	void Update() {
		if (keyDisableTimer > 0.0f) {
			keyDisableTimer -= Time.deltaTime;
			if (keyDisableTimer <= 0.0f) {
				SetGraphicAlpha(pressKeyGraphic, 1.0f);
			}
		} else if (Input.GetKeyUp(KeyCode.Space)) {
			LoadTitleScene();
		}
	}

	void SetGraphicAlpha(Graphic gr, float a) {
		Color color = gr.color;
		color.a = a;
		gr.color = color;
	}

	public void LoadTitleScene() {
		SceneManager.LoadScene("TitleScene");
	}
}

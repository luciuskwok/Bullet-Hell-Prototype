using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneControl : MonoBehaviour
{
	void Update() {
		if (Input.GetKeyUp(KeyCode.Space)) {
			LoadTitleScene();
		}
	}

	public void LoadTitleScene() {
		SceneManager.LoadScene("TitleScene");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneControl : MonoBehaviour
{
	void Update() {
		if (Input.GetKeyUp(KeyCode.Space)) {
			LoadGameplayScene();
		}
	}

	public void LoadGameplayScene() {
		//Debug.Log("New game");
		SceneManager.LoadScene("GameplayScene");
	}
}

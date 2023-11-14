using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenControl : MonoBehaviour
{
	public void LoadGameplayScene() {
		//Debug.Log("New game");
		SceneManager.LoadScene("GameplayScene");
	}
}

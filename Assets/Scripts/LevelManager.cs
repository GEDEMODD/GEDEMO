using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadScene(string name) {
		SceneManager.LoadScene(name);
	}

	public void QuitGame(){
		Application.Quit();
	}
}

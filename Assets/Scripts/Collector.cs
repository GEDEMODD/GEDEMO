using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Collector : MonoBehaviour {

	private int count;
	public Text countText;
	public Text winText;

	void Start(){
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Pick Up")){
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}

	void SetCountText(){
		countText.text = "Score: " + count.ToString ();
		if (count >= 1) {
			winText.text = "You've found all the cubes!";
		}
	}
}

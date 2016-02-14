using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Collector : MonoBehaviour {

	private int count;
	public int howManyCubes;
	public Text countText;
	public Text winText;
	public Text fps;

	void Start(){
		
		count = 0;
		SetCountText ();
		winText.text = "";
		fps.text = "";
	}

	void Update(){
		fps.text = (1/Time.deltaTime).ToString () + " FPS";
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Pick Up")){
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}

	void SetCountText(){
		countText.text = "Found " + count.ToString () + " of " + howManyCubes.ToString() + " items total" ;
		if (count >= howManyCubes) {
			winText.text = "You've found all the cubes!";
		}
	}
}

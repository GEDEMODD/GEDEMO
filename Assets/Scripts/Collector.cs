using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Collector : MonoBehaviour {

	private int count;
	public int howManyCubes;
	public Text countText;
	public Text winText;
	public Text fps;
	public Text timePassedText;
	private float timePassed;
	private AudioSource collectSound;

	void Start(){
		collectSound = GetComponent<AudioSource> ();
		count = 0;
		SetCountText ();
		winText.text = "";
		fps.text = "";
		timePassed = Time.time;
	}

	void Update(){
		fps.text = (1/Time.deltaTime).ToString () + " FPS";
		timePassedText.text = Mathf.Floor(Time.time - timePassed).ToString() + " seconds";
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Pick Up")){
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
			collectSound.Play ();
			collectSound.Play (44100);
		}
	}

	void SetCountText(){
		countText.text = "Found " + count.ToString () + " of " + howManyCubes.ToString() + " items total" ;
		if (count >= howManyCubes) {
			winText.text = "You've found all the cubes in " + timePassedText.text;
		}
	}
}

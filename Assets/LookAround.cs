using UnityEngine;
using System.Collections;

public class LookAround : MonoBehaviour {
	
	public float lookSensitivity;
	public float xRotation;
	public float yRotation;
	public float currentXRotation;
	public float currentYRotation;
	public float xRotationV;
	public float yRotationV;
	public float lookSmoothDamp;

	void Start(){
		lookSensitivity = 5.0f;
		xRotation = 0.0f;
		yRotation = 0.0f;
		currentXRotation = 0.0f;
		currentYRotation = 0.0f;
		xRotationV = 0.0f;
		yRotationV = 0.0f;
		lookSmoothDamp = 0.1f;
	}

	// Update is called once per frame
	void Update() {
		
		xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
		yRotation += Input.GetAxis("Mouse X") * lookSensitivity;


		currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothDamp);
		currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothDamp);
		

		transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
	}
}

using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	float moveSpeedMultiplier = 1;
	float stationaryTurnSpeed = 180;
	float movingTurnSpeed = 360;

	bool onGround;

	Animator animator;

	Vector3 moveInput;
	float turnAmount;
	float forwardAmount;
	Vector3 velocity;

	float jumpPower = 10;

	IComparer rayHitComparer;

	// Use this for initialization
	void Start () {
		SetupAnimator ();
	}

	public void Move(Vector3 move){
		if (move.magnitude > 1) {
			move.Normalize ();
		}

		this.moveInput = move;

		velocity = GetComponent<Rigidbody>().velocity;

		ConvertMoveInput ();
		ApplyExtraTurnRotation ();
		GroundCheck ();
		UpdateAnimator ();
	}

	void SetupAnimator(){
		animator = GetComponent<Animator> ();
		foreach (Animator childAnimator in GetComponentsInChildren<Animator>()) {
			if (childAnimator != animator) {
				animator.avatar = childAnimator.avatar;
				Destroy (childAnimator);
				break;
			}
		}
	}

	void OnAnimatorMove(){
		if (onGround && Time.deltaTime > 0) {
			Vector3 v = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;
			v.y = GetComponent<Rigidbody>().velocity.y;
			GetComponent<Rigidbody>().velocity = v;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate(){
	}

	void GroundCheck(){
		Ray ray = new Ray (transform.position + Vector3.up * 0.1f, -Vector3.up);
		RaycastHit[] hits = Physics.RaycastAll (ray, 0.5f);
		rayHitComparer = new RayHitComparer ();
		System.Array.Sort (hits, rayHitComparer);

		if (velocity.y < jumpPower * .5f) {
			//onGround = false;
			GetComponent<Rigidbody>().useGravity = true;
			foreach (var hit in hits) {
				if (!hit.collider.isTrigger) {
					if (velocity.y <= 0) {
						GetComponent<Rigidbody>().position = Vector3.MoveTowards (GetComponent<Rigidbody>().position, hit.point, Time.deltaTime * 5);
					}
	
					onGround = true;
					GetComponent<Rigidbody>().useGravity = false;

					//
					break;
				}
			}
		}
	}

	void ConvertMoveInput(){
		Vector3 localMove = transform.InverseTransformDirection (moveInput);
		turnAmount = Mathf.Atan2 (localMove.x, localMove.z);
		forwardAmount = localMove.z;
	}

	void UpdateAnimator(){
		animator.applyRootMotion = true;

		animator.SetFloat ("Forward", forwardAmount, 0.1f, Time.deltaTime);
		animator.SetFloat ("Turn", turnAmount, 0.1f, Time.deltaTime);
	}

	void ApplyExtraTurnRotation(){
		float turnSpeed = Mathf.Lerp (stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
		transform.Rotate (0, turnAmount * turnSpeed * Time.deltaTime, 0);

	}

	class RayHitComparer : IComparer
	{
		public int Compare(object x, object y){
			return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
		}
	}
}

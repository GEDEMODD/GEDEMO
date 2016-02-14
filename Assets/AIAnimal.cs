using UnityEngine;
using System.Collections;

public class AIAnimal : MonoBehaviour {

	public Transform[] Waypoints;
	public int NextDest = 0;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (agent.remainingDistance < 0.5f) {
			agent.SetDestination (Waypoints [NextDest].position);
			NextDest = (NextDest + 1) % Waypoints.Length;
		}
	}
}

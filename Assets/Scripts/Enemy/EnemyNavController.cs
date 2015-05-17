using UnityEngine;
using System.Collections;

public class EnemyNavController : MonoBehaviour {

	NavMeshAgent navAgent;
	Transform playerTarget;
	Transform myTransform;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		navAgent = GetComponent<NavMeshAgent> ();
		myTransform = GetComponent<Transform> ();
		playerTarget = GameObject.Find ("TroopPlayer").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Vector3.Distance (myTransform.position, playerTarget.position) > 2f) {
			anim.SetBool ("isMoving", true);
			navAgent.SetDestination (playerTarget.position);
		} else {
			anim.SetBool ("isMoving", false);
			navAgent.ResetPath();
		}
	}
}

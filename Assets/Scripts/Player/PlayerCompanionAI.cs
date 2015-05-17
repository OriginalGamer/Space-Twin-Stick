using UnityEngine;
using System.Collections;

public class PlayerCompanionAI : MonoBehaviour {

	NavMeshAgent navAgent;
	Transform playerTarget;
	Transform myTransform;
	Rigidbody myRigidBody;
	Animator anim;

	public GameObject activeTarget = null;

	public enum EnumState{
		none,
		findPlayer,
		findTarget,
		attackTarget,
	} 
	public EnumState targetEnum;

	// Use this for initialization
	void Start () {
		targetEnum = EnumState.none;
		navAgent = GetComponent<NavMeshAgent> ();
		myTransform = GetComponent<Transform> ();
		myRigidBody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		playerTarget = GameObject.Find ("TroopPlayer").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		SetTargetEnum ();
		AnimationController ();
	}

	void AnimationController(){
		if (targetEnum == EnumState.findTarget && activeTarget != null) {
			float distance = Vector3.Distance (myTransform.position, activeTarget.transform.position);
			if (distance < 1.5f ) {
				anim.SetBool ("isMoving", false);
			} else {
				anim.SetBool ("isMoving", true);
			}
		}
		if (targetEnum == EnumState.findPlayer) {
			anim.SetBool ("isMoving", true);
		}
	}

	void SetTargetEnum(){
		float distFromPlayer = Vector3.Distance (myTransform.position, playerTarget.position);
		if (distFromPlayer > 5f) {
			targetEnum = EnumState.findPlayer;
			navAgent.SetDestination (playerTarget.position);
			navAgent.Resume();
		}
		if (distFromPlayer < 5f) {
			targetEnum = EnumState.findTarget;
			if (activeTarget == null){
				navAgent.Stop();
				FindTarget();
				anim.SetBool ("isMoving", false);
			} else {
				navAgent.Resume();
				navAgent.SetDestination (activeTarget.transform.position);
				float distFromTarget = Vector3.Distance (myTransform.position, activeTarget.transform.position);
				if (distFromTarget < 1){
					anim.SetTrigger ("hit");
					Destroy (activeTarget, 0.5f);
				} else {
					anim.SetBool ("isMoving", true);
				}
			}
		}
	}

	void FindTarget(){
		RaycastHit hit;
		if (Physics.SphereCast (myTransform.position, 0.5f, transform.forward, out hit)){
			if (hit.collider.tag == "Enemy"){
				activeTarget = hit.collider.gameObject;
			}
		}  
	}
}

using UnityEngine;
using System.Collections;

public class PlayerCompanionAI : MonoBehaviour {

	public int moveSpeed;
	public GameObject swordFX;

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
		navAgent.speed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		SetTargetEnum ();
		AnimationController ();
		CheckAlive ();
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
		if (distFromPlayer > 10f) {
			targetEnum = EnumState.findPlayer;
			navAgent.SetDestination (playerTarget.position);
			navAgent.speed = moveSpeed;
			activeTarget = null;
		}
		if (distFromPlayer < 10f) {
			targetEnum = EnumState.findTarget;
			if (activeTarget == null){
				if (distFromPlayer < 5f){
					navAgent.SetDestination (myTransform.position);
					anim.SetBool ("isMoving", false);
				}
			} else {
				navAgent.speed = moveSpeed * 2;
				myRigidBody.transform.LookAt (activeTarget.transform);
				navAgent.SetDestination (activeTarget.transform.position);
				float distFromTarget = Vector3.Distance (myTransform.position, activeTarget.transform.position);
				if (distFromTarget < 1.5f){
					anim.SetTrigger ("hit");
					Instantiate (swordFX, myTransform.position, Quaternion.identity);
					activeTarget.SendMessage ("GetHit", 50);
				} else {
					anim.SetBool ("isMoving", true);
				}
			}
		}
	}

	void CheckAlive(){
		if (activeTarget != null){
			EnemyNavController hit = activeTarget.GetComponent<EnemyNavController>();
			if (!hit.isAlive){
				activeTarget = null;
			}
		}
	}

	void OnTriggerStay(Collider col){
		if (col.tag == "Enemy"){
			if (activeTarget == null){
				EnemyNavController hit = col.GetComponent<EnemyNavController>();
				if (hit.isAlive)
					activeTarget = col.gameObject;
			}
		}
	}
}

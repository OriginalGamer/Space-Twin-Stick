using UnityEngine;
using System.Collections;

public class PlayerCompanionAI : MonoBehaviour {

	public int moveSpeed;

	public int lifeSpan = 5; //Seconds
	float lifeCount = 0; //Start
	public GameObject healthBarFill;
	float fillScale;

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
		fillScale = healthBarFill.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		SetTargetEnum ();
		AnimationController ();
		CheckAlive ();
		LifespanCheck ();
	}

	void LifespanCheck(){
		lifeCount += Time.deltaTime;
		if (lifeCount >= lifeSpan) {
			Destroy (gameObject);
		}
		float xScale = Mathf.Clamp (fillScale / lifeSpan * (lifeSpan - lifeCount), 0, 1);
		Vector3 scale = new Vector3 (xScale, 1, 1);
		healthBarFill.transform.localScale = Vector3.Slerp (healthBarFill.transform.localScale, scale, 5 * Time.deltaTime);
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

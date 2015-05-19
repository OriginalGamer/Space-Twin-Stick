using UnityEngine;
using System.Collections;

public class EnemyNavController : MonoBehaviour {

	PlayerStats pStats;

	public int health = 100;
	public int attackDamage = 10;
	float attackTimer = 10;

	public int detectionRange;
	bool targetSighted = false;

	public bool isAlive = true;

	NavMeshAgent navAgent;
	Transform playerTarget;
	Transform myTransform;
	Rigidbody myRigidBody;
	Renderer myRenderer;

	Animator anim;

	public Material deathMaterial;

	public GameObject healthBarFill;
	float fillScale;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		navAgent = GetComponent<NavMeshAgent> ();
		myTransform = GetComponent<Transform> ();
		myRigidBody = GetComponent<Rigidbody> ();
		myRenderer = GetComponentInChildren<Renderer> ();
		playerTarget = GameObject.Find ("TroopPlayer").transform;
		pStats = (PlayerStats)GameObject.Find ("TroopPlayer").GetComponent<PlayerStats> ();

		fillScale = healthBarFill.transform.localScale.x;
	}


	// Update is called once per frame
	void FixedUpdate () {
		if (isAlive && pStats.isAlive) RoutMovement ();
		if (!isAlive)
			myRenderer.material.color = Color.Lerp (myRenderer.material.color, Color.clear, 0.4f * Time.deltaTime);
		HealthUI ();
	}

	void RoutMovement(){
		/*
		if (Vector3.Distance (myTransform.position, playerTarget.position) > 2f) {
			anim.SetBool ("isMoving", true);
			navAgent.SetDestination (playerTarget.position);
		} else {
			anim.SetBool ("isMoving", false);
			attackTimer += Time.deltaTime;
			if (attackTimer >= 2)
			{
				attackTimer = 0;
				anim.SetTrigger ("doAttack");
				playerTarget.SendMessage ("TakeHit", attackDamage);
			}
			navAgent.ResetPath();
		} */
		float navDistance = Vector3.Distance (myTransform.position, playerTarget.position);
		if (navDistance < detectionRange) {
			if(!targetSighted){
			RaycastHit hit;
				Vector3 rayTarget = new Vector3 (playerTarget.position.x, playerTarget.position.y + 1, playerTarget.position.z);
				Vector3 rayOrigin = new Vector3 (myTransform.position.x, myTransform.position.y + 1, myTransform.position.z);
				if (Physics.Raycast (rayOrigin, (rayTarget - rayOrigin), out hit, detectionRange)){
					if (hit.collider.tag == "Player"){
						targetSighted = true;
					}
				}
			}
			if (targetSighted){
				if (navDistance <= 2f){
					anim.SetBool ("isMoving", false);
					attackTimer += Time.deltaTime;
					if (attackTimer >= 2)
					{
						attackTimer = 0;
						anim.SetTrigger ("doAttack");
						playerTarget.SendMessage ("TakeHit", attackDamage);
					}
					navAgent.ResetPath();
				} else {
					anim.SetBool ("isMoving", true);
					navAgent.SetDestination (playerTarget.position);
				}
			}
		}
	}

	void GetHit(int damage){
		health -= damage;
		if (health <= 0) {
			DoDeath();
		} else {
			navAgent.ResetPath();
			anim.SetTrigger ("hit");
		}
	}

	void HealthUI(){
		if (isAlive) {
			float xScale = Mathf.Clamp (fillScale / 100 * health, 0, 1);
			Vector3 scale = new Vector3 (xScale, 1, 1);
			healthBarFill.transform.localScale = Vector3.Slerp (healthBarFill.transform.localScale, scale, 5 * Time.deltaTime);
		}
	}

	void DoDeath(){
		anim.SetTrigger ("died");
		anim.SetBool ("isDead", true);
		isAlive = false;
		Destroy (gameObject, 15);
		navAgent.enabled = false;
		myRenderer.material = deathMaterial;
		myRenderer.material.color = Color.red;
		gameObject.GetComponent<Collider> ().enabled = false;
		gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		healthBarFill.GetComponentInParent<FaceCamera> ().gameObject.SetActive (false);
	}
}

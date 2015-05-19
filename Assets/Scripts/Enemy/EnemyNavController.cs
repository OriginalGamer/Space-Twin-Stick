﻿using UnityEngine;
using System.Collections;

public class EnemyNavController : MonoBehaviour {

	PlayerStats pStats;

	public int health = 100;
	public int attackDamage = 10;
	float attackTimer = 10;

	public bool isAlive = true;

	NavMeshAgent navAgent;
	Transform playerTarget;
	Transform myTransform;
	Renderer myRenderer;

	Animator anim;

	public Material deathMaterial;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		navAgent = GetComponent<NavMeshAgent> ();
		myTransform = GetComponent<Transform> ();
		myRenderer = GetComponentInChildren<Renderer> ();
		playerTarget = GameObject.Find ("TroopPlayer").transform;
		pStats = (PlayerStats)GameObject.Find ("TroopPlayer").GetComponent<PlayerStats> ();
	}


	// Update is called once per frame
	void FixedUpdate () {
		if (isAlive && pStats.isAlive) RoutMovement ();
		if (!isAlive)
			myRenderer.material.color = Color.Lerp (myRenderer.material.color, Color.clear, 0.4f * Time.deltaTime);
	}

	void RoutMovement(){
		if (Vector3.Distance (myTransform.position, playerTarget.position) > 2f) {
			anim.SetBool ("isMoving", true);
			navAgent.SetDestination (playerTarget.position);
		} else {
			anim.SetBool ("isMoving", false);
			attackTimer += Time.deltaTime;
			if (attackTimer >= 2){
				attackTimer = 0;
				anim.SetTrigger ("doAttack");
				playerTarget.SendMessage ("TakeHit", attackDamage);
			}

			navAgent.ResetPath();
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
	}
}

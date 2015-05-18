﻿using UnityEngine;
using System.Collections;

public class PlayerDroneAI : MonoBehaviour {

	public int moveSpeed;
	
	Transform playerTarget;
	Transform myTransform;
	NavMeshAgent navAgent;
	Rigidbody myRigidBody;

	public GameObject activeTarget = null;
	public GameObject droneBullet;
	public Transform shootTip;
	
	public enum EnumState{
		none,
		findPlayer,
		findTarget,
		attackTarget,
	} 
	public EnumState targetEnum;

	float shootTimer = 2;
	float scoutRotTimer = 5;
	Vector3 rotDir = Vector3.up;

	// Use this for initialization
	void Start () {
		playerTarget = GameObject.Find ("TroopPlayer").transform;
		navAgent = GetComponent<NavMeshAgent> ();
		myTransform = GetComponent<Transform> ();
		myRigidBody = GetComponent<Rigidbody>();

		targetEnum = EnumState.none;
		navAgent.speed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		SetTarget ();
	}

	void SetTarget(){
		float distFromPlayer = Vector3.Distance (myTransform.position, playerTarget.position);
		if (distFromPlayer > 10f) {
			targetEnum = EnumState.findPlayer;
			navAgent.Resume ();
			navAgent.SetDestination (playerTarget.position);
			navAgent.speed = moveSpeed;
			activeTarget = null;
		}
		if (distFromPlayer < 10f) {
			targetEnum = EnumState.findTarget;
			if (activeTarget == null){
				ScoutTarget();
				if (distFromPlayer < 5f){
					navAgent.Stop ();
				}

			} else {
				Vector3 lookDir = new Vector3(activeTarget.transform.position.x, myTransform.position.y, activeTarget.transform.position.z);
				myRigidBody.transform.LookAt (lookDir);
				float rof = 2;
				shootTimer += Time.deltaTime;
				if (shootTimer >= rof){
					GameObject bullet = (GameObject)Instantiate (droneBullet, shootTip.position, Quaternion.identity);
					bullet.SendMessage ("GetTarget", activeTarget);
					activeTarget = null;
					shootTimer = 0;
				}
			}
		}
	}

	void ScoutTarget(){
		float dirSwitch = 3;
		scoutRotTimer += Time.deltaTime;
		if (scoutRotTimer >= dirSwitch) {
			int ran = Random.Range (1, 5);
			if (ran > 2){
				rotDir = -rotDir;
			} 
			scoutRotTimer = 0;
		}
		myTransform.Rotate (rotDir * 100 * Time.deltaTime);
		RaycastHit hit;
		if (myRigidBody.SweepTest (myRigidBody.transform.forward, out hit, 20)){
			if (hit.collider.tag == "Enemy"){
				activeTarget = hit.collider.gameObject;
			}
		} 
	}
}
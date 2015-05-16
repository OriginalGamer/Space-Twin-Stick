﻿using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	public int shootSpeed;
	Rigidbody myRigidBody;
	Transform myTransform;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
		myTransform = GetComponent<Transform> ();
	}

	void Update () {
		//myRigidBody.transform.Translate(myRigidBody.transform.forward * 1 * Time.deltaTime);
		myTransform.Translate (Vector3.forward * 30 * Time.deltaTime);
	}
}

﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour {

    PlayerStats Stats;

    public GameObject healthBarFill;
    float fillScale;

	// Use this for initialization
	void Start () {
	    Stats = GetComponent<PlayerStats>();
		fillScale = healthBarFill.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		MonitorHealthUI ();
		if (Input.GetKeyDown (KeyCode.Space)){
			Stats.playerHeath -= 10;
		}
	}

    void MonitorHealthUI() {
		float xScale = Mathf.Clamp (fillScale / 100 * Stats.playerHeath, 0, 1);
		Vector3 scale = new Vector3 (xScale, 1, 1);
		healthBarFill.transform.localScale = Vector3.Slerp (healthBarFill.transform.localScale, scale, 5 * Time.deltaTime);
    }

	void TakeHit(int damage){
		if (Stats.playerHeath <= 0) {
			Stats.myAnim.SetTrigger ("death");
			Stats.isAlive = false;
		} else {
			Stats.playerHeath -= damage;
			Stats.myAnim.SetTrigger ("getHit");
		}
	}
}
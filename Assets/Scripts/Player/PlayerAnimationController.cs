using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

	[HideInInspector] public PlayerStats Stats;
	[HideInInspector] public PlayerMovementController PlayerContoller;
	[HideInInspector] public PlayerGunController GunController;

	Rigidbody myRigidBody;
	Animator anim;

	string shootStance;
	bool animatingShot = false;

	void Start () {
		anim = GetComponent<Animator> ();
		myRigidBody = GetComponentInParent<Rigidbody> ();
	}

	void Update(){
		ShootCheck ();
	}

	void FixedUpdate () {
		WalkCheck ();
	}

	void WalkCheck(){
		if (myRigidBody.velocity.magnitude < 1f) {
			anim.SetBool ("isRunning", false);
		} else if (myRigidBody.velocity.magnitude > 0) {
			anim.SetBool ("isRunning", true);
		} 
	}

	void EndRoll(){
		PlayerContoller.canRoll = true;
		animatingShot = false;
	}

	void ShootCheck(){
		shootStance = PlayerContoller.shootEnum.ToString ();
		if (!animatingShot) {
			if (shootStance == "standShoot") {
				animatingShot = true;
				anim.SetTrigger ("standShoot");
			}
			if (shootStance == "standRapid"){
				animatingShot = true;
				anim.SetTrigger ("standRapid");
			}
			if (shootStance == "runShoot"){
				animatingShot = true;
				anim.SetTrigger ("runShoot");
			}
			if (shootStance == "runRapid"){
				animatingShot = true;
				anim.SetTrigger ("runRapid");
			}
			if (shootStance == "none"){
				animatingShot = false;
				anim.SetTrigger ("isNoneShoot");
			}
		}
	}

	void ShootBullet(){
		PlayerContoller.canRoll = true;
		GunController.ShotFired ();
	}

	void EndShoot(){
		animatingShot = false;
	}
}

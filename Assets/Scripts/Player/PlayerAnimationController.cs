using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

	PlayerStats Stats;

	string shootStance;
	bool animatingShot = false;

	[HideInInspector] public AudioSource walkingFX; //Audio Played When Walking

	void Start () {
		Stats = GetComponentInParent<PlayerStats> ();
	}

	void Update(){
		ShootCheck ();
	}

	void FixedUpdate () {
		MovementCheck ();
	}

	void MovementCheck(){
		/* Checks if the player is moving
		 * Sets the bool accordingly */
		if (Stats.myRigidBody.velocity.magnitude < 1f) {
			Stats.myAnim.SetBool ("isRunning", false);
		}
		else if (Stats.myRigidBody.velocity.magnitude > 0) {
			Stats.myAnim.SetBool ("isRunning", true);
			if (!walkingFX.isPlaying) walkingFX.Play ();
		} 
	}

	void EndRoll(){ //Called from Roll animation event.
		Stats.MovementContoller.canRoll = true;
		animatingShot = false;
	}

	#region Shooting
	void ShootCheck(){
		/* Reads the shooting stance enum from player movement controller,
		 * Sends a trigger when stance is present.
		 * AnimatingShot is set to true, stopping the trigger from being sent again.
		 * When EndShot() is called, animatingShot is reset. */
		shootStance = Stats.MovementContoller.shootEnum.ToString ();
		if (!animatingShot) {
			if (shootStance == "standShoot") {
				animatingShot = true;
				Stats.myAnim.SetTrigger ("standShoot");
			}
			if (shootStance == "standRapid"){
				animatingShot = true;
				Stats.myAnim.SetTrigger ("standRapid");
			}
			if (shootStance == "runShoot"){
				animatingShot = true;
				Stats.myAnim.SetTrigger ("runShoot");
			}
			if (shootStance == "runRapid"){
				animatingShot = true;
				Stats.myAnim.SetTrigger ("runRapid");
			}
			if (shootStance == "none"){
				animatingShot = false;
				Stats.myAnim.SetTrigger ("isNoneShoot");
			}
		}
	}

	void ShootBullet(){ //Called from animation event when bullet needs to fire.
		Stats.MovementContoller.canRoll = true;
		Stats.GunController.ShotFired ();
	}

	void EndShoot(){ //Called from animation event when shoot anim has finished.
		animatingShot = false;
	}
	#endregion
}

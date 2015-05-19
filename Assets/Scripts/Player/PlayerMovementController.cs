using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {

	Rigidbody myRigidBody; //Player Rigidbody
	Transform myTransform; //Player Transform
	PlayerStats Stats;	   //Player Stats

	Vector3 leftAxis, rightAxis; 					//Left/Right Joystick Axis
	[HideInInspector] public bool canRoll = true;   //Controlls eather can Roll, Shoot, Run
	[HideInInspector] Vector3 rollPoint; 			//Roll Direction

	// Stances for which way a gun is being shot
	public enum EnumState{ 
		none, 			//No Button Pressed
		standShoot, 	//R Tigger, No Movement
		runShoot		//R Trigger, Movement
	} [HideInInspector] public EnumState shootEnum;

	public AudioSource walkingFX; //Audio Played When Walking
	public GameObject laserLight; //Laser from Gun
	bool hasLockOn = false;
	Transform lockOnTarget;

	// Use this for initialization
	void Start () {
		Stats = GetComponent<PlayerStats> ();
		myRigidBody = GetComponent<Rigidbody> ();
		myTransform = GetComponent<Transform> ();

		shootEnum = EnumState.none;
		walkingFX = GetComponentInChildren<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetAxis ();
		if (Stats.isAlive) DashRollController ();
		if (canRoll && Stats.isAlive) {
			ShootController ();
			AimAssist ();
		}
	}

	void FixedUpdate(){
		if (canRoll && Stats.isAlive)MovementController ();
		if (Stats.isAlive && !hasLockOn) RotationController ();
	}

	// Gets the input for both joystick axis
	void GetAxis(){
		leftAxis = new Vector3 (Input.GetAxis ("Left_Horizontal"), 0, Input.GetAxis ("Left_Vertical"));
		rightAxis = new Vector3 (Input.GetAxis ("Right_Horizontal"), 0, Input.GetAxis ("Right_Vertical"));
	}

	void MovementController(){
		/* Checks if the player is moving the joystick,
		 * -Adds large amount of force on first line
		 * -Clamps to a max speed on second line */
		if (leftAxis.x != 0 || leftAxis.z != 0) {
			myRigidBody.AddForce (leftAxis.normalized * 50, ForceMode.Force);
			myRigidBody.velocity = Vector3.ClampMagnitude (myRigidBody.velocity, Stats.playerSpeed);
		}
	}

	void RotationController(){
		/* Checks if the right joystick is active.
		 * If it is then, get look direction form the joystick */
		if (rightAxis != Vector3.zero) {
			Quaternion dir = Quaternion.LookRotation (rightAxis.normalized);
			myTransform.rotation = Quaternion.RotateTowards (transform.rotation, dir, Stats.playerTurnSpeed);
		}
		/* Else, if the left stick is being used.
		 * Get look diredtion from velocity, If direction is !null then change rotation */
		else if (leftAxis != Vector3.zero) {
			Vector3 velDir = new Vector3 (myRigidBody.velocity.x, 0, myRigidBody.velocity.z);
			if (velDir != Vector3.zero){
				Quaternion dir = Quaternion.LookRotation (velDir.normalized);
				myTransform.rotation = Quaternion.RotateTowards (myTransform.rotation, dir, Stats.playerTurnSpeed);
			}
		}
	}

	void ShootController(){
		if (myRigidBody.velocity.magnitude > 1f) {
			if (Input.GetAxis ("Right_Trigger") < 0) {       //Running Rappid Fire
				shootEnum = EnumState.runShoot;
			} else {
				shootEnum = EnumState.none;					 //No Shooting
			}
		}
		else if (myRigidBody.velocity.magnitude < 1f) {
			if (Input.GetAxis ("Right_Trigger") < 0) {		 //Standing Rappid Fire
				shootEnum = EnumState.standShoot;
			}  else {
				shootEnum = EnumState.none;					 //No Shooting
			}
		}
	}

	void AimAssist(){
		if (Input.GetButton ("Left_Button")) {
			if (Stats.playerEnergy > 1) {
				Stats.playerEnergy -= 10 * Time.deltaTime;
				laserLight.SetActive (true);
				if (Input.GetAxis ("Right_Trigger") < 0) {
					RaycastHit hit;
					if (Physics.Raycast (laserLight.transform.position, myTransform.forward, out hit)){
						if (hit.collider.tag == "Enemy"){
							hasLockOn = true;
							lockOnTarget = hit.transform;
						} 
					}
				}
			} else {
				laserLight.SetActive (false);
				hasLockOn = false;
			}
		} else{
			laserLight.SetActive (false);
			hasLockOn = false;
		}
		if (hasLockOn){
			if(lockOnTarget.GetComponent<EnemyNavController>().isAlive ){
				myTransform.LookAt (lockOnTarget.transform.position);
			} else{
				hasLockOn = false;
			}
		}
	}

	void DashRollController(){
		/* Checks if can roll when roll button pressed.
		 * Sends a trigger for animation, then creates a roll point */
		if (Input.GetButtonDown ("Right_Button") && canRoll && shootEnum == EnumState.none){
			myRigidBody.velocity = Vector3.zero;
			Stats.myAnim.SetTrigger ("doRoll");
			rollPoint = myRigidBody.position + myRigidBody.transform.forward * Stats.rollDistance;
			canRoll = false;
		}
		/* Lerps player to possition until the animation event changes canroll to false */
		if (!canRoll) myRigidBody.position = Vector3.Lerp (myRigidBody.position, rollPoint, Stats.rollSpeed * Time.deltaTime);
	}
}

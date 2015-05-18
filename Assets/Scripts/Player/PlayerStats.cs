using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int playerHeath;
	public int playerSpeed;
	public int playerTurnSpeed;
	public float rollDistance;
	public float rollSpeed;
	
	[HideInInspector] public Rigidbody myRigidBody;
	[HideInInspector] public Transform myTransform;
	[HideInInspector] public Animator myAnim;

	[HideInInspector] public PlayerAnimationController AnimationController;
	[HideInInspector] public PlayerMovementController MovementContoller;
	[HideInInspector] public PlayerGunController GunController;

	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
		myTransform = GetComponent<Transform> ();
		myAnim = GetComponentInChildren<Animator> ();

		AnimationController = GetComponentInChildren<PlayerAnimationController> ();
		MovementContoller = GetComponent<PlayerMovementController> ();
		GunController = GetComponent<PlayerGunController> ();
	}
}

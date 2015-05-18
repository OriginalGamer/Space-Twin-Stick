using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	public GameObject bulletFX;
	public int shootSpeed;
	public int bulletDamage = 50;
	Rigidbody myRigidBody;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
		Destroy (gameObject, 5);
		Instantiate (bulletFX, myRigidBody.position, Quaternion.identity);
	}

	void FixedUpdate () {
		myRigidBody.transform.Translate (Vector3.forward * 30 * Time.deltaTime);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Enemy") {
			col.SendMessage ("GetHit", bulletDamage);
			Destroy (gameObject);
		}
	}
}

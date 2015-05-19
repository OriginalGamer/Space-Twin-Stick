using UnityEngine;
using System.Collections;

public class DroneBullet : MonoBehaviour {

	public GameObject explosionFX;
	GameObject shootTarget = null;
	Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shootTarget != null) {
			myTransform.LookAt (shootTarget.transform);
			myTransform.position = Vector3.MoveTowards (myTransform.position, shootTarget.transform.position, 10 * Time.deltaTime);
		}
	}

	void GetTarget(GameObject target){
		shootTarget = target;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Enemy") {
			Destroy (col.gameObject);
		}
		Destroy (this.gameObject);
		Instantiate (explosionFX, myTransform.position, Quaternion.identity);
	}
}

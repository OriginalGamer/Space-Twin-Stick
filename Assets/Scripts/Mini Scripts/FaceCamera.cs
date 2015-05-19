using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

	Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 lookPoint = new Vector3 (myTransform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
		myTransform.LookAt (lookPoint);
		myTransform.eulerAngles = new Vector3 ((int)myTransform.rotation.x, (int)myTransform.rotation.y, (int)myTransform.rotation.z);
	}
}

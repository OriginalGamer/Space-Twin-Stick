using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

	public Rigidbody followTarget; // Follow Target
	public Vector3 followOffset;   // Offset dimention from traget
	public bool acceleratedFollow; // Y/N To follow target by speed
	public int followSpeed;        // Follow Speed
	Transform myTransform;		   // Transform of this game object
	
	void Start () {
		myTransform = GetComponent<Transform> ();
	}

	void FixedUpdate () {
		if (!acceleratedFollow)
			myTransform.position = followTarget.position + followOffset;
		if (acceleratedFollow)
			myTransform.position = Vector3.Lerp (myTransform.position, followTarget.position + followOffset, followSpeed * Time.deltaTime);
	}
}

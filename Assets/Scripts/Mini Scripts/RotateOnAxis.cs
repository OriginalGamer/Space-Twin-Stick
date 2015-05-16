using UnityEngine;
using System.Collections;

public class RotateOnAxis : MonoBehaviour {

	public string axisName;
	public GameObject targetOBJ;
	public int turnSpeed;
	
	void Update () {
		float input = Input.GetAxis (axisName);
		float speed = input * turnSpeed * Time.deltaTime;
		targetOBJ.transform.Rotate (Vector3.down * speed); 
	}
}

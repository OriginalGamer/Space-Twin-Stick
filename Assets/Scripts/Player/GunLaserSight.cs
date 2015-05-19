using UnityEngine;
using System.Collections;

public class GunLaserSight : MonoBehaviour {

	LineRenderer myLine;
	public Transform parentTransform;
	public GameObject pointLight;

	// Use this for initialization
	void Start () {
		myLine = GetComponent<LineRenderer> ();
	}

	void FixedUpdate () {
		RaycastHit hit;
		if (Physics.Raycast (parentTransform.position, parentTransform.forward, out hit)){
			float lineDistance = Vector3.Distance (transform.position, hit.point);
			Vector3 cord = new Vector3(0, 0, lineDistance);
			myLine.SetPosition (1, cord);
			pointLight.transform.position = hit.point;
		}
	}
}

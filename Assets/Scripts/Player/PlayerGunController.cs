using UnityEngine;
using System.Collections;

public class PlayerGunController : MonoBehaviour {

	public Transform shootTip;
	public GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShotFired(){
		Instantiate (bullet, shootTip.transform.position, transform.rotation);
	}
}

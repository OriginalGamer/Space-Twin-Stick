using UnityEngine;
using System.Collections;

public class PlayerGunController : MonoBehaviour {

	public Transform shootTip;
	public GameObject basicBullet;
	public GameObject flash;

	enum EnumState{
		normal,
		somethingElse,
	} EnumState gunMode;
	
	void Start () {
		gunMode = EnumState.normal;
	}

	public void ShotFired(){
		if (gunMode == EnumState.normal) {
			Instantiate (basicBullet, shootTip.transform.position, transform.rotation);
			flash.SetActive (false);
			flash.SetActive (true);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		//spawn the player.
		Instantiate(player);
		//reference the CameraFollow script and set the target to the instantiated player.
		GameObject.Find("Main Camera").GetComponent<CameraFollow>().target = GameObject.Find("PlayerCat(Clone)").transform;
	}
	
	// Update is called once per frame
	void Update () {

	}

}

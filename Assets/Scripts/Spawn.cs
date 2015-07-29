using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject player;
	public CameraFollow camFollow;

	// Use this for initialization
	void Start () {
		camFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		//spawn the player.
		Instantiate(player);
		//reference the CameraFollow script and set the target to the instantiated player.
		camFollow.target = player.transform;
	}
	
	// Update is called once per frame
	void Update () {

	}

}

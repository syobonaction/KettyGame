using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
	public bool isGrounded = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag=="Ground"){
			isGrounded = true;
		}
	}
}

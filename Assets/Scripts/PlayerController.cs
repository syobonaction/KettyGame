using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	enum PlayerState{Spawning, Idle, Movement, Dead};
	
	PlayerState currentState;
	float lastStateChange = 0.0f;
	
	float walkSpeed = 3.0f;
	float jump = 200.0f;
	
	public GameObject player;
	
	// Use this for initialization
	void Start () {
		SetCurrentState(PlayerState.Spawning);
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState) {
			case PlayerState.Spawning:
				PlayerSpawning();
				break;
			case PlayerState.Idle:
				PlayerIdle();
				break;
			case PlayerState.Movement:
				PlayerMovement();
				break;
			case PlayerState.Dead:
				PlayerDead();
				break;
			default:
				return;
		}
	}
	
	void SetCurrentState(PlayerState state) {
		currentState = state;
		lastStateChange = Time.time;
	}
	
	float GetStateElapsed() {
		return Time.time - lastStateChange;
	}
	
	void PlayerSpawning(){
		print ("I am spawning!");
		if(GetStateElapsed() >= 1f){
			player = Instantiate(player);
			SetCurrentState(PlayerState.Idle);
		}
	}
	
	void PlayerIdle(){
		print ("I am idle!");
		if(Input.GetKeyDown(KeyCode.Space)&& player.GetComponent<PlayerCollision>().isGrounded){
			Jump();
		}
		if(Input.anyKey){
			SetCurrentState(PlayerState.Movement);
		}
	}
	
	void PlayerMovement(){
		print ("I am recieving input!");
		if(Input.GetKey(KeyCode.D)){
			player.transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.A)) {
			player.transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
		} else {
			SetCurrentState(PlayerState.Idle);
		}
		if(Input.GetKeyDown(KeyCode.Space)&& player.GetComponent<PlayerCollision>().isGrounded){
			Jump();
		}
	}
	
	void Jump(){
		print ("I am jumping!");
		player.GetComponent<PlayerCollision>().isGrounded = false;
		player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jump));
	}
	
	void PlayerDead(){
		print ("I am dead!");
		Destroy(player.gameObject);
		SetCurrentState(PlayerState.Spawning);
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		SetCurrentState(PlayerState.Dead);
	}
}

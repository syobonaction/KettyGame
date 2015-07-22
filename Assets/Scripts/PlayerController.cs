using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	enum PlayerState{Spawning, Idle, Walking, Running, Jumping, Dead};
	
	PlayerState currentState;
	float lastStateChange = 0.0f;
	
	bool isGrounded = false;
	float walkSpeed = 3.0f;
	
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
			case PlayerState.Walking:
				PlayerWalking();
				break;
			case PlayerState.Running:
				break;
			case PlayerState.Jumping:
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
			SetCurrentState(PlayerState.Idle);
		}
	}
	
	void PlayerIdle(){
		print ("I am idle!");
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)){
			SetCurrentState(PlayerState.Walking);
		}
	}
	
	void PlayerWalking(){
		print ("I am walking!");
		if(Input.GetKey(KeyCode.D)){
			transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.A)) {
			transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
		} else {
			SetCurrentState(PlayerState.Idle);
		}
	}
	
	void PlayerDead(){
		print ("I am dead!");
		Destroy(this.gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		SetCurrentState(PlayerState.Dead);
	}
}

﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	enum PlayerState{Spawning, Idle, Movement, Dead, Jumping, Running, Falling};
	
	PlayerState currentState;
	PlayerState previousState;
	float lastStateChange = 0.0f;
	
	float walkSpeed = 3.0f;
	float currentSpeed = 3.0f;
	float runSpeed = 5.0f;
	float acceleration = .05f;
	float DEFJUMP = 300.0f;
	float jump = 300.0f;
	
	public GameObject player;
	public AudioClip jumpSound;
	public PlayerCollision playerCollision;
	
	private AudioSource playerAudio;
	
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
			case PlayerState.Jumping:
				PlayerJumping();
				break;
			case PlayerState.Running:
				PlayerRunning();
				break;
			case PlayerState.Falling:
				PlayerFalling();
				break;
			case PlayerState.Dead:
				PlayerDead();
				break;
			default:
				return;
		}
	}
	
	void SetCurrentState(PlayerState state) {
		previousState = currentState;
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
			playerAudio = player.GetComponent<AudioSource>();
			playerCollision = player.GetComponent<PlayerCollision>();
			SetCurrentState(PlayerState.Idle);
		}
	}
	
	void PlayerIdle(){
		print ("I am idle!");
		if(Input.anyKey){
			SetCurrentState(PlayerState.Movement);
		}
	}
	
	void PlayerMovement(){
		print ("I am recieving input!");
		if(Input.GetKey(KeyCode.LeftShift) && playerCollision.isGrounded){
			SetCurrentState(PlayerState.Running);
		}
		if(Input.GetKey(KeyCode.D)){
			player.transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.A)) {
			player.transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
		} else {
			SetCurrentState(PlayerState.Idle);
		}
		if(Input.GetKey(KeyCode.Space)&& playerCollision.isGrounded){
			SetCurrentState(PlayerState.Jumping);
		}
	}
	
	void PlayerJumping(){
		print ("I am jumping!");
		if(previousState==PlayerState.Running){
			jump += 100;
		}
		playerCollision.isGrounded = false;
		playerAudio.PlayOneShot(jumpSound);
		player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jump));
		jump = DEFJUMP;
		SetCurrentState(PlayerState.Falling);
	}
	
	void PlayerFalling(){
		if(Input.GetKey(KeyCode.D)){
			player.transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.A)) {
			player.transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
		}
		if(playerCollision.isGrounded){
			SetCurrentState(PlayerState.Idle);
		}
	}
	
	void PlayerRunning(){
		if(Input.GetKey(KeyCode.LeftShift) && playerCollision.isGrounded){
			print (currentSpeed);
			if(Input.GetKey(KeyCode.D)){
				Accelerate();
				player.transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
				if(Input.GetKey(KeyCode.A)){
					currentSpeed = walkSpeed;
				}
			} else if(Input.GetKey(KeyCode.A)) {
				Accelerate();
				player.transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
				if(Input.GetKey(KeyCode.D)){
					currentSpeed = walkSpeed;
				}
			}
			if(Input.GetKey(KeyCode.Space)){
				SetCurrentState(PlayerState.Jumping);
			}
		} else {
			currentSpeed = walkSpeed;
			SetCurrentState(PlayerState.Movement);
		}
	}
	
	void Accelerate(){
		if(currentSpeed <= runSpeed){
			currentSpeed += acceleration;
		}
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

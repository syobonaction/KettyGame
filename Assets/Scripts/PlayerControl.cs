using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	//-----------JON VARIABLES------------------//
	//variables for jumping
	public float jumpPower = 250.0f;
	public Animator anim;
	public float minJumpDelay = 0.5f;
	public Transform groundCheck;
	private float jumpTime = 0.0f;
	public bool grounded = false;
	private bool jumped = false;
	private bool canJump = true;

	//variables for movement
	public float speed = 1.0f;
	public string axisName = "Horizontal";



	//------------------------CHARLIES VARS -----------------------------//


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
	
	public AudioClip jumpSound;
	private AudioSource playerAudio; 


	// Use this for initialization
	void Start () {
		//get a reference to the animator attached to the player.
		anim = gameObject.GetComponent<Animator>();

		//CHARLIE THING
		SetCurrentState(PlayerState.Spawning);
	
	}
	
	// Update is called once per frame
	void Update () {

		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		transform.position += transform.right*Input.GetAxis(axisName)*speed*Time.deltaTime;

		//------------CHARLIE SWITHCES-------------//
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

	
	//------------------PUT THE ACTUAL SWITCHES HERE--------------------------//


	//functions for looking left or right

	void lookLeft(){
		Vector2 newScale = transform.localScale;
		newScale.x = -1.0f;
		transform.localScale = newScale;  
	}
	
	void lookRight(){
		Vector2 newScale = transform.localScale;
		newScale.x = 1.0f;
		transform.localScale = newScale; 
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
		//print ("I am spawning!");
		if(GetStateElapsed() >= 1f){
			playerAudio = GetComponent<AudioSource>();
			SetCurrentState(PlayerState.Idle);
		}
	}
	
	void PlayerIdle(){
		//print ("I am idle!");
		anim.SetTrigger ("Land");
		anim.SetFloat("Speed",0);
		if(Input.GetKeyDown(KeyCode.Space)&& grounded){
			SetCurrentState(PlayerState.Jumping);
		} else if(Input.anyKey){
			SetCurrentState(PlayerState.Movement);
		}
		
	}
	
	void PlayerMovement(){
		//print ("I am recieving input!");
		
		//TRIGGER FOR THE SPEED VALUE FOR TRANSITIONS
		
		
		if(Input.GetKey(KeyCode.LeftShift) && grounded){
			SetCurrentState(PlayerState.Running);
		}
		
		if(Input.GetAxis (axisName) < 0)
		{
			//turn to the left is the A button is pressed and facing right.
			anim.SetFloat ("Speed", currentSpeed);
			Vector3 newScale = transform.localScale;
			newScale.x = -1.0f;
			transform.localScale = newScale;
			
		}
		else if (Input.GetAxis (axisName) > 0)
		{
			//turn to the right if the D button is pressed and facing left.
			anim.SetFloat ("Speed", currentSpeed);
			Vector3 newScale = transform.localScale;
			newScale.x = 1.0f;
			transform.localScale = newScale;

		}

		 else {
			SetCurrentState(PlayerState.Idle);
		}
		if(Input.GetKeyDown(KeyCode.Space)&& grounded){
			SetCurrentState(PlayerState.Jumping);
		}
	}
	
	void PlayerJumping(){
		//print ("I am jumping!");

		//set the trigger for landing for the animations.
		anim.SetTrigger("Jump");

		if(previousState==PlayerState.Running){
			jump += 100;
		}

		grounded = false;
		playerAudio.PlayOneShot(jumpSound);
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jump));
		jump = DEFJUMP;
		SetCurrentState(PlayerState.Falling);

	}
	
	void PlayerFalling(){

		if(Input.GetKey(KeyCode.D)){
			lookRight();
			}
		 else if(Input.GetKey(KeyCode.A)) {
			lookLeft();
			}

		if(grounded){
			SetCurrentState(PlayerState.Idle);
		}
	}


	void PlayerRunning(){
		if(Input.GetKey(KeyCode.LeftShift) && grounded){
			print (currentSpeed);

			if(Input.GetKey(KeyCode.D)){
				lookRight();
				Accelerate();
				transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
				if(Input.GetKey(KeyCode.A)){
					currentSpeed = walkSpeed;
				}
			} 
			if(Input.GetKey(KeyCode.A)) {
				lookLeft();
				Accelerate();
				transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
				if(Input.GetKey(KeyCode.D)){
					currentSpeed = walkSpeed;
				}
			}
			if(Input.GetKeyDown(KeyCode.Space)){
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
		//print ("I am dead!");
		SetCurrentState(PlayerState.Spawning);
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		SetCurrentState(PlayerState.Dead);
	}



}
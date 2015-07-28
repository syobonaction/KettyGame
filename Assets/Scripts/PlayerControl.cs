using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		//get a reference to the animator attached to the player.
		anim = gameObject.GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
		//FOR JUMPING

		//use a raycast to determine if the player is touching the ground.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		jumpTime -= Time.deltaTime;

		if (jumpTime < 0) 
		{
			jumpTime = 0;
		}

		//check to see if the jump button is pressed and that the player CAN jump. If so, jump.
		if(Input.GetKeyDown(KeyCode.Space) && canJump == true)
		{
			canJump = false;
			jumped = true;
			grounded = false;
			//set the Jump trigger for animations
			anim.SetTrigger("Jump");
			GetComponent<Rigidbody2D>().AddForce(transform.up*jumpPower);
			jumpTime = minJumpDelay;
		}

		if (grounded && jumpTime <=0 && jumped)
		{
			canJump = true;
			jumped = false;
			//set the Land trigger for animations
			anim.SetTrigger("Land");
		}



		//FOR MOVEMENT
		//set the Speed trigger for animations.
		anim.SetFloat("Speed", Mathf.Abs (Input.GetAxis(axisName)));

		if(Input.GetAxis (axisName) < 0)
		{
			//turn to the left is the A button is pressed and facing right.
			Vector3 newScale = transform.localScale;
			newScale.x = -1.0f;
			transform.localScale = newScale;

		}
		else if (Input.GetAxis (axisName) > 0)
		{
			//turn to the right if the D button is pressed and facing left.
			Vector3 newScale = transform.localScale;
			newScale.x = 1.0f;
			transform.localScale = newScale;

		}

		//move the character
		transform.position += transform.right*Input.GetAxis(axisName)*speed*Time.deltaTime;
	
	}

}

using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	Rigidbody2D rigidbody;
	public bool isGrounded;
	float walkSpeed;
	float runSpeed;
	float walkJump;
	float runJump;
	
	// Use this for initialization
	void Start () {
		rigidbody= GetComponent<Rigidbody2D>();
		isGrounded = false;
		walkSpeed = 4.0f;
		runSpeed = 7.0f;
		walkJump = 300.0f;
		runJump = walkJump * 1.2f;
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
	}
	
	void Movement(){
		if(isGrounded){
			if(Input.GetKey(KeyCode.LeftShift)){
				if(Input.GetKey(KeyCode.D)){
					transform.Translate(Vector2.right * runSpeed * Time.deltaTime);
				}
				if(Input.GetKey(KeyCode.A)){
					transform.Translate(Vector2.left * runSpeed * Time.deltaTime);
				}
				if(Input.GetKey(KeyCode.Space)&& isGrounded) {
					rigidbody.AddForce(new Vector2(0,runJump));
					isGrounded = false;
				}
			} else {
				if(Input.GetKey(KeyCode.D)){
					transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
				}
				if(Input.GetKey(KeyCode.A)){
					transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
				}
				if(Input.GetKey(KeyCode.Space)){
					rigidbody.AddForce(new Vector2(0,walkJump));
					isGrounded = false;
				}
			}
		} else {
			if(Input.GetKey(KeyCode.D)){
				transform.Translate(Vector2.right * walkSpeed/2 * Time.deltaTime);
			}
			if(Input.GetKey(KeyCode.A)){
				transform.Translate(Vector2.left * walkSpeed/2 * Time.deltaTime);
			}
		}
		
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag=="Ground"){
			isGrounded = true;
		}
	}
}

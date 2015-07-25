using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	enum MovementState{Up,Down};
	
	MovementState currentState;
	float lastStateChange = 0.0f;
	
	float startPos;
	float currentPos;
	float destPos;
	float speed = 1f;
	float distance = 3f;
	
	// Use this for initialization
	void Start () {
		startPos = transform.position.y;
		currentPos = startPos;
		SetCurrentState(MovementState.Up);
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState){
			case MovementState.Up:
				MoveUp();
				break;
			case MovementState.Down:
				MoveDown();
				break;
			default:
				return;
		}
	}
	
	void MoveUp(){
		destPos = startPos + distance;
		if(currentPos < destPos){
			transform.Translate(Vector2.up * speed * Time.deltaTime);
			currentPos = transform.position.y;
		} else {
			distance -= (distance * 2);
			SetCurrentState(MovementState.Down);
		}
	}
	
	void MoveDown(){
		destPos = startPos + distance;
		if(currentPos > destPos){
			transform.Translate(Vector2.down * speed * Time.deltaTime);
			currentPos = transform.position.y;
		} else {
			distance -= (distance * 2);
			SetCurrentState(MovementState.Up);
		}
		
	}
	
	void SetCurrentState(MovementState state) {
		currentState = state;
		lastStateChange = Time.time;
	}
	
	float GetStateElapsed() {
		return Time.time - lastStateChange;
	}
}

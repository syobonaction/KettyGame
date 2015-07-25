using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	Vector2 playerPos;
	public Camera camera;
	
	// Use this for initialization
	void Start () {
		playerPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

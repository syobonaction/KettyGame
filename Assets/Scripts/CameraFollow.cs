using UnityEngine;
using System.Collections;

public class CameraFollow: MonoBehaviour {
	
	public float dampTime = 0.15f;
	private Vector3 velocity;
	public Transform target;
	
	public Camera cam;

	//called once when the script is run.
	void Start () {
		velocity = Vector3.zero;
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = cam.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
		
	}
}
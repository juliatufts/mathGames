using UnityEngine;
using System.Collections;

public class VertexController : MonoBehaviour {
	
	public Ray ray;
	public RaycastHit hit;
	public bool canDrag;
	public bool hasVertex;
	public float foregroundPosZ;
	private float click_timer;
	public float hold_delay;

	// Use this for initialization
	void Start () {
		canDrag = false;
		hasVertex = false;
	}

	void OnMouseDown(){
		click_timer = Time.time;
		foregroundPosZ = Mathf.Abs(Camera.main.transform.position.z) + transform.position.z - 0.5f;
	}
	
	//While mouse click is down, update vertex position
	void OnMouseDrag(){

		if((Time.time - click_timer) > hold_delay){
			Debug.Log("clickhold");
		}

		//Get mouse position
		var mousePos = Input.mousePosition;
		mousePos.z = foregroundPosZ;
		//If the mouse click hold is over a vertex, enable click and drag
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit)){
			if(hit.collider == collider && !hasVertex){
				canDrag = true;
				hasVertex = true;
			}
		}

		if(canDrag){
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
			transform.position = mouseWorldPos;
		}

	}

	//When mouse click release, set position
	void OnMouseUp() {
		var mousePos = Input.mousePosition;
		mousePos.z = foregroundPosZ + 0.5f;

		if(canDrag){
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
			transform.position = mouseWorldPos;
			canDrag = false;
			hasVertex = false;
		}
	}
}

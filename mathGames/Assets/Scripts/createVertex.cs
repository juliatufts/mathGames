using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateVertex : MonoBehaviour {

	public Ray ray;
	public RaycastHit hit;
	public GameObject vertex;
	private List<GameObject> vertices;
	public bool canDrag = false;

	public bool one_click = false;
	public float foregroundPosZ;
	public bool timer_running;
	private float timer_for_double_click;
	public float delay;			//How long in seconds to allow for a double click

	void Start(){
		vertices = new List<GameObject>();
	}

	void OnMouseDown(){
		//Get Mouse position, z position forward
		var mousePos = Input.mousePosition;
		foregroundPosZ = Mathf.Abs(Camera.main.transform.position.z) + vertex.transform.position.z - 0.5f;
		mousePos.z = foregroundPosZ;
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

		//First click, no previous clicks
		if(!one_click){
			one_click = true;
			timer_for_double_click = Time.time; //Save the current time
			//Single click action
		} else {
			one_click = false; //Found a double click, now reset
			//If cursor is on graph paper, create a new vertex
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit)){
				if(hit.collider == collider){
					var newVertex = GameObject.Instantiate(vertex, mouseWorldPos, vertex.transform.rotation) as GameObject;
					newVertex.tag = "Vertex";
					//newVertex.transform.parent = transform;
					vertices.Add(newVertex);
				}
				canDrag = true;
			}
			
		}

	}
	
	// Update is called once per frame
	void Update () {

		if(one_click){
			//If the time now is delay seconds more than when the first click started, reset
			if((Time.time - timer_for_double_click) > delay){
				one_click = false;
			}
		}
		
	}
	
	//While mouse click is down, update new vertex position
	void OnMouseDrag () {
		var mousePos = Input.mousePosition;
		mousePos.z = foregroundPosZ;
		
		if(canDrag){
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
			vertices[vertices.Count - 1].transform.position = mouseWorldPos;
		}
		
	}
	
	//When mouse click release, set position
	void OnMouseUp() {
		if(canDrag){
			//Set mouse position, adjust z value
			var mousePos = Input.mousePosition;
			mousePos.z = foregroundPosZ + 0.5f;

			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
			vertices[vertices.Count - 1].transform.position = mouseWorldPos;
		}
		
		canDrag = false;
	}

}

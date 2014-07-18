using UnityEngine;
using System.Collections;

public class VertexController : MonoBehaviour {
	
	public Ray ray;
	public RaycastHit hit;
	public bool canDrag;
	public bool hasVertex;
	public float foregroundPosZ;

	// Use this for initialization
	void Start () {
		canDrag = false;
		hasVertex = false;
	}

	void OnMouseDown(){
		var mousePos = Input.mousePosition;
		mousePos.z = Mathf.Abs(Camera.main.transform.position.z) + transform.position.z;
		foregroundPosZ = Mathf.Abs(Camera.main.transform.position.z) + transform.position.z - 0.5f;
		
		//If the mouse is over a vertex, enable click and drag
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Input.GetKey(KeyCode.Mouse0)){
			if(Physics.Raycast(ray, out hit)){
				if(hit.collider == collider && !hasVertex){
					canDrag = true;
					hasVertex = true;
				}
			}
		} else {
			canDrag = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		var mousePos = Input.mousePosition;
		mousePos.z = foregroundPosZ;

		if(canDrag){
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
			transform.position = mouseWorldPos;
		}

	}

	void OnMouseUp() {
		var mousePos = Input.mousePosition;
		mousePos.z = foregroundPosZ + 0.5f;

		if(canDrag){
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
			transform.position = mouseWorldPos;
		}

		canDrag = false;
		hasVertex = false;
	}
}

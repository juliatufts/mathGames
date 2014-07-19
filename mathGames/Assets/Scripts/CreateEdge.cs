using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateEdge : MonoBehaviour {
	
	public Ray ray;
	public RaycastHit hit;
	public Vector3 initialPoint;
	public Vector3 midPoint;
	public Vector3 endPoint;

	public GameObject Edge;
	private List<GameObject> EdgeList;

	public GameObject v0;
	public GameObject v1;

	private float click_timer;
	public float hold_delay;
	public bool isCurrentlyDrawing = false;	//Are we currently drawing an edge?
	public float epsilon;					//Rounding error to differentiate between time < hold_delay and time > hold_delay
	public bool isHoldingVertex = false;	//Are we currently holding a vertex with the cursor?

	public Color color;
	public float width;
	public int numberOfPoints = 50;
	
	void Start () {
		//Initialize line renderer component
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		//Initialize List
		EdgeList = new List<GameObject>();
	}
	
	void OnMouseDown(){
		//Get initial mouse position
		var mousePos = Input.mousePosition;
		mousePos.z = Mathf.Abs(Camera.main.transform.position.z) + transform.position.z;
		initialPoint = Camera.main.ScreenToWorldPoint(mousePos);

		click_timer = Time.time;
	}
	
	
	//While mouse click is down, draw line and update end point position
	void OnMouseDrag(){

		//If it's before hold_delay OR after hold_delay + epsilon and the cursor is not holding a vertex, then draw edge
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit)){
			if((Time.time - click_timer) < hold_delay || (Time.time - click_timer) > (hold_delay + epsilon) && !isHoldingVertex){
				//Get mouse position
				var mousePos = Input.mousePosition;
				mousePos.z = Mathf.Abs(Camera.main.transform.position.z) + transform.position.z;
				endPoint = Camera.main.ScreenToWorldPoint(mousePos);
				
				//Calculate midpoint of initalPoint and endPoint
				midPoint = (endPoint - initialPoint)*0.5f + initialPoint;
				
				//Check parameters and components
				LineRenderer lineRenderer = GetComponent<LineRenderer>();
				if (null == lineRenderer || null == initialPoint || null == midPoint || null == endPoint){
					return; // no points specified
				} 
				//Update line renderer
				lineRenderer.SetColors(color, color);
				lineRenderer.SetWidth(width, width);
				if (numberOfPoints > 0){
					lineRenderer.SetVertexCount(numberOfPoints);
				}
				
				//Compute sample points
				float t;
				Vector3 position = new Vector3();
				for(int i = 0; i < numberOfPoints; i++){
					t = i / (numberOfPoints - 1.0f);
					position = (1.0f - t) * (1.0f - t) * initialPoint 
						+ 2.0f * (1.0f - t) * t * midPoint
							+ t * t * endPoint;
					lineRenderer.SetPosition(i, position);
				}
			}
			if(!isHoldingVertex && (Time.time - click_timer) > hold_delay && hit.collider == collider) {
				isHoldingVertex = true;
			}
		}
	}

	void OnMouseUp(){

		//If the mouse is on a vertex that is not the original vertex, create a new edge
		//Else remove the edge currently being drawn by setting linerenderer vertex count to 0
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit)){
			if(!(hit.collider == collider) && hit.collider.tag == "Vertex"){
				//Instantiate new edge
				var newEdge = GameObject.Instantiate(Edge, Edge.transform.position, Edge.transform.rotation) as GameObject;
				EdgeList.Add(newEdge);
				var newEdgeController = newEdge.GetComponent<EdgeController>();
				newEdgeController.vertex0 = this.gameObject;
				newEdgeController.vertex1 = hit.collider.gameObject;

				//Clear temp edge
				LineRenderer lineRenderer = GetComponent<LineRenderer>();
				lineRenderer.SetVertexCount(0);

			} else {
				LineRenderer lineRenderer = GetComponent<LineRenderer>();
				lineRenderer.SetVertexCount(0);
			}
			isHoldingVertex = false;
		}
	}
}
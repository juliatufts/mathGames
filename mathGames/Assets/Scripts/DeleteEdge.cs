using UnityEngine;
using System.Collections;

public class DeleteEdge : MonoBehaviour {

	public Ray ray;
	public RaycastHit hit;
	public Vector3 initialPoint;
	public Vector3 midPoint;
	public Vector3 endPoint;

	public bool canDelete;
	private CreateVertex createVertex;
	public bool canDrag;		//True if dragging a newly created vertex
	
	public Color color;
	public float width;
	public int numberOfPoints = 100;

	// Use this for initialization
	void Start () {
		//Initialize line renderer component and bools
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		canDelete = false;

		createVertex = GetComponent<CreateVertex>();
	}

	void OnMouseDown(){
		//Get initial mouse position
		var mousePos = Input.mousePosition;
		mousePos.z = Mathf.Abs(Camera.main.transform.position.z) + transform.position.z;
		initialPoint = Camera.main.ScreenToWorldPoint(mousePos);

		//If the cursor is on the background, can draw red slice through edges
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit)){
			canDelete = true;
		}
	}

	void OnMouseDrag(){

		canDrag = createVertex.canDrag;
		//If canDelete and not dragging newly created vertex, draw red line
		if(canDelete && !canDrag){
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
			//lineRenderer.SetColors(color, color);
			lineRenderer.SetWidth(width, width);
			if (numberOfPoints > 0){
				lineRenderer.SetVertexCount(numberOfPoints);
			}
			
			//Compute sample points
			float t;
			Vector3 position = new Vector3();
			for(int i = 0; i < numberOfPoints; i++){
				t = i / (numberOfPoints - 1.0f);
				position = (1 - t) * initialPoint + t * endPoint;
				lineRenderer.SetPosition(i, position);
			}

			//Check for edges the mouse cursor passes over
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit)){
				//Debug.Log(hit.collider.name);
				if(hit.collider.tag == "Edge"){
					//Debug.Log("hit an edge");
				}
			}
		}

	}
	
	void OnMouseUp(){

		//Clear the line
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(0);
	}
}

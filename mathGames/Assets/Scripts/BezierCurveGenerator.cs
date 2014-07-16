using UnityEngine;
using System.Collections;

public class BezierCurveGenerator : MonoBehaviour {

	public GameObject start;
	public GameObject middle;
	public GameObject end;
	
	public Color color;
	public float width;
	public int numberOfPoints = 20;
	
	void Start () {
		//Initialize line renderer component
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
	}

	void Update () {
		//Check parameters and components
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		if (null == lineRenderer || null == start || null == middle || null == end){
			return; // no points specified
		} 
		
		//Update line renderer
		lineRenderer.SetColors(color, color);
		lineRenderer.SetWidth(width, width);
		if (numberOfPoints > 0){
			lineRenderer.SetVertexCount(numberOfPoints);
		}
		
		//Set points of quadratic Bezier curve
		Vector3 p0 = new Vector3(start.transform.position.x, start.transform.position.y, start.transform.position.z);
		Vector3 p1 = new Vector3(middle.transform.position.x, middle.transform.position.y, middle.transform.position.z);
		Vector3 p2 = new Vector3(end.transform.position.x, end.transform.position.y, end.transform.position.z);

		//Compute sample points
		float t;
		Vector3 position = new Vector3();
		for(int i = 0; i < numberOfPoints; i++){
			t = i / (numberOfPoints - 1.0f);
			position = (1.0f - t) * (1.0f - t) * p0 
				     + 2.0f * (1.0f - t) * t * p1
					 + t * t * p2;
			lineRenderer.SetPosition(i, position);
		}
	}
}

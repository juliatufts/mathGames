using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BezierCurveGenerator_Discrete : MonoBehaviour {

	public GameObject BCurve;
	public GameObject start;
	public GameObject middle;
	public GameObject end;
	public Vector3 startingP0;		//Used to check when the BCurves start repeating
	public float epsilon;			//Used to account for error when checking starting P0 and current P0
	public float width;				//Width of the line	
	public Color color;				//Color of the line
	public int numberOfPoints = 20;	//Number of sample points in the line

	private LineRenderer lineRenderer;
	private List<GameObject> BCurveList;
	
	void Start () {
		BCurveList = new List<GameObject>();
		startingP0 = start.transform.position;
	}
	
	void Update () {
		//Instantiate new BCurve
		var newBCurve = GameObject.Instantiate(BCurve, BCurve.transform.position, BCurve.transform.rotation) as GameObject;
		newBCurve.transform.parent = transform;
		BCurveList.Add(newBCurve);

		//Initialize line renderer component
		LineRenderer lineRenderer = newBCurve.GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		lineRenderer.SetColors(color, color);
		lineRenderer.SetWidth(width, width);
		if (numberOfPoints > 0){
			lineRenderer.SetVertexCount(numberOfPoints);
		}
		
		//Set points of Bezier curve
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

		//Once the number of Bezier curves have spanned the ellipse, destroy all the curves and clear the list
		//We check for this by looking at the point P0
		if(Mathf.Abs(start.transform.position.x - startingP0.x) < epsilon && Mathf.Abs(start.transform.position.y - startingP0.y) < epsilon){
			for(int i = 0; i < BCurveList.Count; i++){
				Destroy(BCurveList[i]);
			}
			BCurveList.Clear();
		}
	}
}

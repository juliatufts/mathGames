using UnityEngine;
using System.Collections;

public class EdgeController : MonoBehaviour {

	public GameObject vertex0;
	public GameObject vertex1;
	private Transform vertex0_trans;
	private Transform vertex1_trans;

	public Vector3 initialPoint;
	public Vector3 midPoint;
	public Vector3 endPoint;

	public Color color;
	public float width;
	public int numberOfPoints = 50;

	// Use this for initialization
	void Start () {
		//Initialize line renderer component
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Set line points based on vertex positions
		vertex0_trans = vertex0.GetComponent<Transform>();
		vertex1_trans = vertex1.GetComponent<Transform>();

		initialPoint = new Vector3 (vertex0_trans.position.x, vertex0_trans.position.y, transform.position.z);
		endPoint = new Vector3 (vertex1_trans.position.x, vertex1_trans.position.y, transform.position.z);
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
		//Set final position
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
}

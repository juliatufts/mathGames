using UnityEngine;
using System.Collections;

public class EllipseCurve : MonoBehaviour {

	public Vector3 center;		//The center of the ellipse
	public float xStretch;		//The horizontal stretch of the ellipse
	public float yStretch;		//The vertical stretch of the ellipse

	public Color color;
	public float width;
	public int numberOfPoints = 20;
	
	void Start () {
		//Initialize line renderer component
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		
		//Update line renderer
		lineRenderer.SetColors(color, color);
		lineRenderer.SetWidth(width, width);
		if (numberOfPoints > 0){
			lineRenderer.SetVertexCount(numberOfPoints);
		}
		
		//Compute sample points
		Vector3 position = new Vector3();
		for(int i = 0; i < numberOfPoints; i++){
			position.x = xStretch * (Mathf.Sin(2*Mathf.PI*i / (numberOfPoints - 1)) + center.x);
			position.y = yStretch * (Mathf.Cos(2*Mathf.PI*i / (numberOfPoints - 1)) + center.y);
			position.z = transform.position.z;
			lineRenderer.SetPosition(i, position);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class EllipseMovement : MonoBehaviour {

	public Vector3 center;		//The center of the ellipse
	public float startingPos;	//The starting position in PI units
	public float xStretch;		//The horizontal stretch of the ellipse
	public float yStretch;		//The vertical stretch of the ellipse

	void Start () {
		Vector3 position = transform.position;
		position.x = xStretch * (Mathf.Sin(startingPos * Mathf.PI) + center.x);
		position.y = yStretch * (Mathf.Cos(startingPos * Mathf.PI) + center.y);
		transform.position = position;
	}
	
	void Update () {
		Vector3 position = transform.position;
		position.x = xStretch * (Mathf.Sin(startingPos * Mathf.PI + Time.time) + center.x);
		position.y = yStretch * (Mathf.Cos(startingPos * Mathf.PI + Time.time) + center.y);
		transform.position = position;
	}
}

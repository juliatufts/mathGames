using UnityEngine;
using System.Collections;

public class EllipseMovement : MonoBehaviour {

	public Vector3 center;		//The center of the ellipse
	public float startingPos;	//The starting position in PI units
	public float xStretch;		//The horizontal stretch of the ellipse
	public float yStretch;		//The vertical stretch of the ellipse
	public float speed;			//How fast the gameObject moves along the ellipse, this depends on the distance between P0 and P2
	public float maxSpeed;		//The upper bound on speed
	public GameObject ellipse;	//The ellipse gameObject from which the ellipse variables above are taken
	public GameObject P0;		//The point moving along the inner ellipse
	public GameObject P2;		//The point moving along the outer ellipse

	private Transform transform0;
	private Transform transform2;
	private EllipseCurve ellipseCurve;

	void Start () {
		//Get and set ellipse variables
		ellipseCurve = ellipse.GetComponent<EllipseCurve>();
		center = ellipseCurve.center;
		xStretch = ellipseCurve.xStretch;
		yStretch = ellipseCurve.yStretch;

		//Set the initial position
		Vector3 position = transform.position;
		position.x = xStretch * (Mathf.Sin(startingPos * Mathf.PI) + center.x);
		position.y = yStretch * (Mathf.Cos(startingPos * Mathf.PI) + center.y);
		transform.position = position;
	}
	
	void Update () {
		//Determine speed = distance between P0 and P1
		transform0 = P0.GetComponent<Transform>();
		transform2 = P2.GetComponent<Transform>();
		//speed = Vector3.Distance(transform0.position, transform2.position) / maxSpeed; //FIX THIS LATER
		//Update position
		Vector3 position = transform.position;
		position.x = xStretch * (Mathf.Sin(startingPos * Mathf.PI + speed * Time.time) + center.x);
		position.y = yStretch * (Mathf.Cos(startingPos * Mathf.PI + speed * Time.time) + center.y);
		transform.position = position;
	}
}

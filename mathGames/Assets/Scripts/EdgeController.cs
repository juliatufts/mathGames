﻿using UnityEngine;
using System.Collections;

public class EdgeController : MonoBehaviour {
	
	public GameObject vertex0;
	public GameObject vertex1;
	public BoxCollider[] boxColliders;
	public Vector3 initialPoint;
	public Vector3 midPoint;
	public Vector3 endPoint;
	public Color color;
	public float width;
	public int numberOfPoints;

	private Transform vertex0_trans;
	private Transform vertex1_trans;
	private VertexController vertexController0;
	private VertexController vertexController1;
	private bool hasVertex0;
	private bool hasVertex1;

	// Use this for initialization
	void Start () {
		//Initialize line renderer component
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
	}

	// Update is called once per frame
	void Update () {
		//Set line points based on current vertex positions, and whether a vertex is being held
		vertex0_trans = vertex0.GetComponent<Transform>();
		vertex1_trans = vertex1.GetComponent<Transform>();
		vertexController0 = vertex0.GetComponent<VertexController>();
		vertexController1 = vertex1.GetComponent<VertexController>();
		hasVertex0 = vertexController0.hasVertex;
		hasVertex1 = vertexController1.hasVertex;

		initialPoint = new Vector3 (vertex0_trans.position.x, vertex0_trans.position.y, transform.position.z);
		endPoint = new Vector3 (vertex1_trans.position.x, vertex1_trans.position.y, transform.position.z);
		midPoint = (endPoint - initialPoint)*0.5f + initialPoint;
		if(hasVertex0){
			initialPoint.z = transform.position.z - 0.5f;
			midPoint.z = transform.position.z + 0.5f;
		} else if(hasVertex1){
			endPoint.z = transform.position.z - 0.5f;
			midPoint.z = transform.position.z + 0.5f;
		}

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
		//Set position
		float t;
		Vector3 position = new Vector3();
		for(int i = 0; i < numberOfPoints; i++){
			t = i / (numberOfPoints - 1.0f);
			position = (1.0f - t) * (1.0f - t) * initialPoint 
				+ 2.0f * (1.0f - t) * t * midPoint
					+ t * t * endPoint;
			lineRenderer.SetPosition(i, position);;
		}

		//Get colliders
		if (Input.GetMouseButtonDown(0)){
			boxColliders = GetComponentsInChildren<BoxCollider>();
			Debug.Log(boxColliders.Length);
		}
		//UPDATE THE COLLIDERS
		if(Input.GetMouseButtonUp(0) && boxColliders.Length > 0){
			float x;
			//Adjust points to be exactly on vertices
			initialPoint = vertex0.transform.position;
			endPoint = vertex1.transform.position;
			midPoint = (endPoint - initialPoint)*0.5f + initialPoint;

			Vector3 colPosition = new Vector3();
			Vector3 lastColPosition = initialPoint;

			for(int j = 0; j < numberOfPoints; j++)
			{
				x = j / (numberOfPoints - 1.0f);
				colPosition = (1.0f - x) * (1.0f - x) * initialPoint 
					+ 2.0f * (1.0f - x) * x * midPoint
						+ x * x * endPoint;
				if(j > 0){
					BoxCollider currentCol = boxColliders[j-1];
					GameObject child = currentCol.gameObject;

					//child.transform.parent = this.transform;

					currentCol.center = (colPosition - lastColPosition)*0.5f + lastColPosition;
					Debug.Log(colPosition);
					Debug.Log(lastColPosition);
					Debug.Log("-------------");
					Debug.Log(currentCol.center);
					Debug.Log("==============");
					//adjust z value so that the collider lies behind the vertices
					Vector3 temp = currentCol.center;
					temp.z += 0.05f;
					currentCol.center = temp;
					currentCol.size = new Vector3(Vector3.Distance(vertex1.transform.position, vertex0.transform.position) / (numberOfPoints - 1.0f), width, 0.01f);
					//Rotate to fit to line
					Vector3 dir = vertex0.transform.position - vertex1.transform.position;
					float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
					//child.transform.RotateAround(currentCol.center, Vector3.forward, angle);
				}
				lastColPosition = colPosition;
			}

		}
	}
	
}

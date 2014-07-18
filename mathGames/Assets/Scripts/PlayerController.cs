using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject mainCamera;
	private Camera camera;
	private Transform camTransform;

	public Ray ray;
	public RaycastHit hit;
	public bool mouseOnVertex;

	// Use this for initialization
	void Start () {
		camera = mainCamera.GetComponent<Camera>();
		camTransform = mainCamera.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		var mousePos = Input.mousePosition;
		mousePos.z = Mathf.Abs(camTransform.position.z);

		if(Input.GetKey(KeyCode.Mouse0)){
			//Debug.Log(camera.ScreenToWorldPoint(mousePos));
		}

		//If the mouse is over a vertex enable click and drag
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit)){
			if(hit.collider.name == "Vertex"){
				mouseOnVertex = true;
			} else {
				mouseOnVertex = false;
			}
		}
	}
}
using UnityEngine;
using System.Collections;

public class DetectMouse : MonoBehaviour {

	public Ray ray;
	public RaycastHit hit;
	public GameObject particleHighlight;
	private ParticleSystem particles;
	private bool isEmitting;

	void Start(){
		particles = particleHighlight.GetComponent<ParticleSystem>();
	}
	
	void Update(){
		//If the mouse is over START, emit highlight particles
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit)){
			if(hit.collider.name == "ParticleHighlight"){
				particles.enableEmission = true;
			} else {
				particles.enableEmission = false;
			}
		}
	}
}

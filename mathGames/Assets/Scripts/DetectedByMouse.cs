using UnityEngine;
using System.Collections;

public class DetectedByMouse : MonoBehaviour {

	void OnMouseOver()
	{
		Debug.Log(gameObject.name);
	}
}

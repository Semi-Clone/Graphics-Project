using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worriorCameraController : MonoBehaviour {
	private Transform target;	
	private Vector3 offsetPosition;
	private Space offsetPositionSpace = Space.Self;
	private bool lookAt = true;

	void Start () {
		target = GameObject.FindGameObjectWithTag ("warrior").transform;
		offsetPosition = transform.position - target.position;
	}

	private void LateUpdate(){
		if(offsetPositionSpace == Space.Self) {
			transform.position = target.TransformPoint(offsetPosition);
		}else {
			transform.position = target.position + offsetPosition;
		}

		if (lookAt) {
			transform.LookAt(target);
		} else{
			transform.rotation = target.rotation;
		}
	}
}
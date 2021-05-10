using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour {

	public Transform spawn_point;
	void OnTriggerEnter2D (Collider2D col) {

		spawn_point.position = gameObject.transform.position;
				
	}
	
}

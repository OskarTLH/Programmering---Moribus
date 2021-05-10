using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spring_tile : MonoBehaviour {
	public int springForce;
	void OnTriggerEnter2D(Collider2D col) {
		col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,springForce));
	}
}

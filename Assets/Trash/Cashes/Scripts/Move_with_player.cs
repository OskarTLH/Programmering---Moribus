using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_with_player : MonoBehaviour {

	GameObject player;

	[SerializeField]
	Vector3 offset;

	[SerializeField]
	float smooth_speed;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, smooth_speed);
		
	}
}

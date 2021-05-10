using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_tile : MonoBehaviour {

	public Transform [] move_pos;
	public float speed;
	int count;

	bool holdup;

	void Start () {
		holdup = false;
	}

	void FixedUpdate () {
		if (!holdup) {
			float step = speed * Time.deltaTime; 
			 transform.position = Vector2.MoveTowards(transform.position, move_pos[count].position, step);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
    {
		if (col.gameObject.tag == "platform_post") {
			StartCoroutine("hold_patrole");
		}
        
    }

	IEnumerator hold_patrole () {
		holdup = true;		
		yield return new WaitForSeconds(2f);

		count ++;

		if (count >= move_pos.Length) {
			count = 0;
		} 
		holdup = false;
	}

}

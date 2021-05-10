using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour {
	public Transform portal_pos;
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "player")
		{
			col.gameObject.transform.position = portal_pos.position;
			StartCoroutine("stop_particles");
		}
	}

	IEnumerator stop_particles () {
        GetComponentInChildren<ParticleSystem>().Play();
        portal_pos.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds (0.5f);
        GetComponentInChildren<ParticleSystem>().Stop();
        portal_pos.GetComponentInChildren<ParticleSystem>().Stop();
    }
}

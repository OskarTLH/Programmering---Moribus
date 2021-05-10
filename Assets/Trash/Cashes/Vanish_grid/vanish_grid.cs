using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class vanish_grid : MonoBehaviour {

	public GameObject my_tiles;

	void OnTriggerEnter2D(Collider2D col) {
		my_tiles.SetActive(false);
	}

	 void OnTriggerExit2D(Collider2D other)
    {
        my_tiles.SetActive(true);
    }
}

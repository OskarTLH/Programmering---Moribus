using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class You_won : MonoBehaviour {


    GameObject interface_UI;

    [Header("Next Scene")]
    [SerializeField] string Scene;

    void Awake () {
        interface_UI = GameObject.FindGameObjectWithTag ("interface");
    }
    
	 void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player") { 
            interface_UI.SetActive(true);
            interface_UI.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            interface_UI.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Continue";
            interface_UI.GetComponentInChildren<Button>().onClick.AddListener(load_next_scene);

            interface_UI.GetComponentsInChildren<TMPro.TMP_Text>()[0].text = "You Reached Your Desination";
            interface_UI.GetComponentsInChildren<TMPro.TMP_Text>()[1].text = "However, Your journey Does Not Stop Here";
            }
   
    }

    void load_next_scene () {
        SceneManager.LoadScene(Scene);
    }
}

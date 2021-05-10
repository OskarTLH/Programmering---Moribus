using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour { 

    public float moveSpeed;
    public KeyCode swordToggle;
    public KeyCode attackKey;
    Animator anim;
    Rigidbody2D rb;
    GameObject sword;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sword = transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float moveSpeedMult = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
        {

            moveSpeedMult = 2.0f;

        } 
        // Vector3 movement = Vector3.zero;
        // movement = new Vector3(Input.GetAxisRaw("Horizontal"),movement.y,movement.z);
        // movement = new Vector3(movement.x, Input.GetAxisRaw("Vertical"), movement.z);
        anim.SetFloat("HorMove", Mathf.Abs(Movement.x) > Mathf.Abs(Movement.y) ? Movement.x : 0);
        anim.SetFloat("VerMove", Mathf.Abs(Movement.y) > Mathf.Abs(Movement.x) ? Movement.y : 0);
        anim.SetFloat("Magnitude", Movement.magnitude);
        rb.position += Movement * Time.deltaTime * moveSpeed * moveSpeedMult;

        if (Input.GetKeyDown(swordToggle))
        {
            sword.SetActive(!sword.active);
        }

        if (Input.GetKeyDown(attackKey) && sword.active)
        {
            anim.SetTrigger("Attack");
        }
        

        if (Movement.x == 0 && Movement.y == 0)
        {
            anim.SetBool("isIdle", true);
        }
        else
        {
            anim.SetBool("isIdle", false);
        }
    }

    

}

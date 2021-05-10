using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuardScript : MonoBehaviour
{

    [Header("Player Movement")]
    [SerializeField] float speed = 1.0f;
    [SerializeField] float Sprint = 1;

    float _Sprint = 0;
    [SerializeField] float jumpForce = 4.0f;
    private float inputX;
    private Animator animator;
    private Rigidbody2D body2d;
    private bool combatIdle = false;
    private bool isGrounded = true;


    /*          Custom variables            */
    bool is_alive;
    bool is_striking;

    int score;

    [Header("Player Settings")]
    [SerializeField] int lives;

    private int _lives;

    [SerializeField] GameObject StartingPosition;
    Vector2 hit_dir;

    float hit_dist;
    float center_offset;
    float x_offset;

    TMPro.TMP_Text score_UI;
    TMPro.TMP_Text lives_UI;

    GameObject interface_UI;

    GameObject[] points_ingame;



    ////////////


    // Use this for initialization
    void Start()
    {

        _Sprint = Sprint;

        transform.position = StartingPosition.transform.position;

        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();

        center_offset = 0.5f;
        x_offset = 0.5f;

        hit_dist = 1;

        _lives = lives;

        hit_dir = Vector2.right;

        is_alive = true;


        score_UI = GameObject.FindGameObjectWithTag("score").GetComponent<TMPro.TMP_Text>();
        lives_UI = GameObject.FindGameObjectWithTag("lives").GetComponent<TMPro.TMP_Text>();
        //interface_UI = GameObject.FindGameObjectWithTag ("interface");

        //points_ingame = GameObject.FindGameObjectsWithTag("point");

        //Debug.Log(interface_UI.GetComponentInChildren<Button> ().name);

//        interface_UI.GetComponentInChildren<Button>().onClick.AddListener(reset_game);
       // interface_UI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _Sprint = Sprint;
        }
        else
        {
            _Sprint = 0;
        }


        score_UI.text = "Points: " + score;
        lives_UI.text = "Lives: " + lives;






        if (lives == 0)
        {
            interface_UI.SetActive(true);
            animator.SetTrigger("Death");
            is_alive = false;
        }

        if (is_alive)
        {
            // -- Handle input and movement --
            inputX = Input.GetAxis("Horizontal");


            // Swap direction of sprite depending on walk direction
            if (inputX > 0)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                hit_dir = Vector2.right;
                x_offset = Mathf.Abs(x_offset);
            }
            else if (inputX < 0)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                hit_dir = Vector2.left;
                x_offset = x_offset * -1;
            }
            // Move
            body2d.velocity = new Vector2(inputX * (speed + _Sprint), body2d.velocity.y);

            // -- Handle Animations --
            isGrounded = IsGrounded();

            Vector2 center_vector = new Vector2(transform.position.x + x_offset, transform.position.y + center_offset);
            Debug.DrawRay(center_vector, hit_dir * hit_dist, Color.green);

            animator.SetBool("Grounded", isGrounded);

            //Death
            if (Input.GetKeyDown("k"))
            {
                GetComponent<BoxCollider2D>().size = new Vector2(0.05f, 0.25f);
                GetComponent<BoxCollider2D>().offset = new Vector2(0.737525f, 0.24f);
                animator.SetTrigger("Death");
            }
            //Hurt
            else if (Input.GetKeyDown("h"))
                animator.SetTrigger("Hurt");
            //Recover
            else if (Input.GetKeyDown("r"))
                animator.SetTrigger("Recover");
            //Change between idle and combat idle
            else if (Input.GetKeyDown("i"))
                combatIdle = !combatIdle;



            //Attack
            else if (Input.GetMouseButtonDown(0))
            {
                if (!is_striking)
                {
                    is_striking = true;

                    center_vector = new Vector2(transform.position.x + x_offset, transform.position.y + center_offset);
                    RaycastHit2D hit = Physics2D.Raycast(center_vector, hit_dir, hit_dist);

                    if (hit.collider != null)
                    {
                        Debug.Log(hit.collider.tag);
                        if (hit.collider.tag == "enemy")
                        {
                            Destroy(hit.collider);
                        }
                    }
                    StartCoroutine("striking");
                }
            }

            //Jump
            else if (Input.GetKeyDown("space") && isGrounded)
            {
                animator.SetTrigger("Jump");
                body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);

            }

            //Walk
            else if (Mathf.Abs(inputX) > Mathf.Epsilon && isGrounded)
                animator.SetInteger("AnimState", 2);
            //Combat idle
            else if (combatIdle)
                animator.SetInteger("AnimState", 1);
            //Idle
            else
                animator.SetInteger("AnimState", 0);
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy" || col.gameObject.tag == "obstacles" && is_alive)
        {
            is_alive = false;
            animator.SetTrigger("Death");
            lives--;
            StartCoroutine("spawn");
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "point")
        {
            col.gameObject.SetActive(false);
            score++;
        }

    }


    IEnumerator spawn()
    {
        if (lives != 0)
        {
            yield return new WaitForSeconds(2f);
            transform.position = StartingPosition.transform.position;
            yield return new WaitForSeconds(1f);
            animator.SetTrigger("Recover");
            yield return new WaitForSeconds(0.8f);
            is_alive = true;
        }
    }

    IEnumerator striking()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.8f);
        is_striking = false;
    }

    IEnumerator activate_reset()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Recover");
        yield return new WaitForSeconds(0.8f);
        is_alive = true;
    }


    public void reset_game()
    {
        interface_UI.SetActive(false);
        transform.position = StartingPosition.transform.position;
        lives = _lives;
        score = 0;
        StartCoroutine("activate_reset");

        foreach (GameObject obj in points_ingame)
        {
            obj.SetActive(true);
        }
    }


    bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector3.up * 0.5f, Color.green);
        return Physics2D.Raycast(transform.position, -Vector3.up, 0.5f);
    }

}

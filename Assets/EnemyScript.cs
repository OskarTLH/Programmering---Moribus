using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    GameObject player;
    public int distanceThreshold;
    Rigidbody2D rb;

    bool canAttack = true;

    public int life = 1;




// Start is called before the first frame update
void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && Vector2.Distance(transform.position,player.transform.position) <= distanceThreshold)
        {
            FollowPlayer();
        } else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void FollowPlayer()
    {

        if (Vector2.Distance(transform.position, player.transform.position) < distanceThreshold / 3)
        {
            rb.velocity = Vector2.zero;
            AttackPlayer();
        }
        else
        {
            //rb.AddForce(Vector3.Normalize(player.transform.position - transform.position) * 5);
            rb.velocity = Vector3.Normalize(player.transform.position - transform.position) * 5f;
        }
        
    }

    void AttackPlayer()
    {
        if (canAttack)
        {
            canAttack = false;
            player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * Time.deltaTime);
            Invoke("StopPush", 1f);
            Invoke("CanAttackAgain", 1f);
            player.GetComponent<Player>().DamagePlayer(4);
        }
    }

    void CanAttackAgain()
    {
        canAttack = true;
    }

    void StopPush()
    {
        player.GetComponent<Rigidbody2D>().Sleep();
    }

    void TakeDamage()
    {
        life--;
        if (life < 1)
        {
            GameObject.Find("Spawner").GetComponent<Spawner_enemies>().SpawnNewEnemy();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            TakeDamage();
        }
    }

}

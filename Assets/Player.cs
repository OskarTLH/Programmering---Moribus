using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 100;


    public void DamagePlayer(int damage)
    {
        Health -= damage;

        if (Health <= 0)
            GameMaster.KillPlayer(this);
        
    }
}

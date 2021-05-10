using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public void Start()
    {
    }

    public static void KillPlayer(Player player)
    {
        print(player);
        Destroy(player.gameObject);
    }

}
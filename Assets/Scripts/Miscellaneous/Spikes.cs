using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, IContactDamage
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void DealContactDamage(PlayerInstance player)
    {
        //Lose HP from PlayerData from PlayerInstance
        player.data.currentHP -= damage;
        player.transform.position = player.currentPCheckpoint;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero; //Have to do GetComponent?
        //If not sufficient, can reset angular velocity, too, and then call RigidBody2D.Sleep();
        //Teleport to most recent checkpoint (can store in player?)
        // player.GetComponent<Transform>().position = 
    }
}

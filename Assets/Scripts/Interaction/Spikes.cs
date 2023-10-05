using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, IContactDamage
{
    [SerializeField] int damage;
    
    public void DealContactDamage(PlayerInstance player)
    {
        //Lose HP from PlayerData from PlayerInstance
        player.combat.Damage(damage);
        player.transform.position = Checkpoint.currentCheckpoint;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero; //Have to do GetComponent?
        //If not sufficient, can reset angular velocity, too, and then call RigidBody2D.Sleep();
        //Teleport to most recent checkpoint (can store in player?)
        // player.GetComponent<Transform>().position = 
    }
}

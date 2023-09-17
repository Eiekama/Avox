using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, IContactDamage
{
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
        //Teleport to most recent checkpoint (can store in player?)
    }
}

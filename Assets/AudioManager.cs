using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Character Animation Sounds")]
    [SerializeField] public AK.Wwise.Event singleJump;
    [SerializeField] public AK.Wwise.Event doubleJump;
    

    public void playSingleJump()
    {
        singleJump.Post(gameObject);
    }

    public void playBuildingCollapse()
    {
        doubleJump.Post(gameObject);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Character Animation Sounds")]
    [SerializeField] public AK.Wwise.Event singleJump;
    [SerializeField] public AK.Wwise.Event doubleJump;
    

    void playSingleJump()
    {
        singleJump.Post(gameObject);
    }

    void playDoubleJump()
    {
        doubleJump.Post(gameObject);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Character Animation Sounds")]
    [SerializeField] public AK.Wwise.Event singleJump;
    [SerializeField] public AK.Wwise.Event doubleJump;
    [SerializeField] public AK.Wwise.Event grassFootStep;
    [SerializeField] public AK.Wwise.Event attack;


    void playSingleJump()
    {
        singleJump.Post(gameObject);
    }

    void playDoubleJump()
    {
        doubleJump.Post(gameObject);
    }

    void playGrassFootStep()
    {
        grassFootStep.Post(gameObject);
    }

    void playAttack()
    {
        attack.Post(gameObject);
    }




}

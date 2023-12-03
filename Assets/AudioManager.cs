using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Character Animation Sounds")]
    [SerializeField] public AK.Wwise.Event singleJump;
    [SerializeField] public AK.Wwise.Event doubleJump;
    [SerializeField] public AK.Wwise.Event grassFootStep;
    [SerializeField] public AK.Wwise.Event attack;
    [SerializeField] public AK.Wwise.Event playerDeath;

    [Header("UI Sounds")]
    [SerializeField] public AK.Wwise.Event click;
    [SerializeField] public AK.Wwise.Event buttonPress;


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

    void playPlayerDeath()
    {
        playerDeath.Post(gameObject);
    }

    public void playClick()
    {
        click.Post(gameObject);
    }

    public void playButtonPress()
    {
        buttonPress.Post(gameObject);
    }



}

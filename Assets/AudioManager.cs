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
    [SerializeField] public AK.Wwise.Event playerDamage;

    [Header("UI Sounds")]
    [SerializeField] public AK.Wwise.Event click;
    [SerializeField] public AK.Wwise.Event buttonPress;

    // Player Sounds
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

    void playPlayerDamage()
    {
        playerDamage.Post(gameObject);
    }

    //UI Sounds
    public void playClick()
    {
        click.Post(gameObject);
    }

    public void playButtonPress()
    {
        buttonPress.Post(gameObject);
    }

    // Music
    public void setMenuState()
    {
        AkSoundEngine.SetState("Music_State", "Menu_State");
    }

    public void setCalmState()
    {
        AkSoundEngine.SetState("Music_State", "Calm_State");
    }

    public void setBattleState()
    {
        AkSoundEngine.SetState("Music_State", "Battle_State");
    }

    public void setMusicNoneState()
    {
        AkSoundEngine.SetState("Music_State", "None");
    }
    // Ambience
    public void setForestState()
    {
        AkSoundEngine.SetState("Ambience_State", "Forest");
    }

    public void setCaveState()
    {
        AkSoundEngine.SetState("Ambience_State", "Cave");
    }


}

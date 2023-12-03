using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class InitialiseData : MonoBehaviour
{
    public static bool initialised = false;

    [SerializeField] PlayerData playerData;
    [SerializeField] CollectibleData collectibleData;

    private PlayerController _controller;

    private void Awake()
    {
        if (!initialised)
        {
            for (int i = 0; i < collectibleData.info.Length; i++)
            {
                collectibleData.info[i].collected = false;
            }

            playerData.hasWeapon = false;
            playerData.hasDash = false;
            playerData.maxHP = 3;
            playerData.currentHP = playerData.maxHP;
            playerData.maxMP = 0;
            playerData.currentMP = playerData.maxMP;
            playerData.isFacingRight = true;
        }
    }

    private void Start()
    {
        if (!initialised)
        {
            initialised = true;

            _controller = FindObjectOfType<PlayerController>();
            if (!_controller.inputActions.Dialogue.enabled)
                _controller.ToggleActionMap(_controller.inputActions.Player);
        }
        else
        {
            //hardcoded enabling of action map when respawning after death
            //will probably find time after this semester to write a respawn manager script instead
            _controller = FindObjectOfType<PlayerController>();
            Sequence.Create()
                .ChainDelay(1.0f)
                .ChainCallback(target: this, target => target._controller.ToggleActionMap(target._controller.inputActions.Player));
            //not the best but works for now
        }
    }
}

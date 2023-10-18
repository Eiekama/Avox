using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseData : MonoBehaviour
{
    private static bool initialised = false;

    [SerializeField] PlayerData playerData;
    [SerializeField] CollectibleData collectibleData;

    void Awake()
    {
        if (!initialised)
        {
            for (int i = 0; i < collectibleData.info.Length; i++)
            {
                collectibleData.info[i].collected = false;
            }

            playerData.currentHP = playerData.maxHP;

            initialised = true;
        }
    }
}

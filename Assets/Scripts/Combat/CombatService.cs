using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CombatService : ICombatService
{
    private readonly PlayerData _data = (PlayerData)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/PlayerData.asset", typeof(PlayerData));
}

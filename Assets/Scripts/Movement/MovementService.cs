// credit: https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerData.cs

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementService : IMovementService
{
    private readonly PlayerData _data = (PlayerData)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/PlayerData.asset", typeof(PlayerData));

    public Rigidbody2D RB { get; set; }
}

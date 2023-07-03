using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Status")]
    [Space(5)]

    public float MPRecoveryRate; //per second

    private int _maxHP;
    public int maxHP
    {
        get { return _maxHP; }
        set { _maxHP = Mathf.Max(0, value); }
    }

    private int _currentHP;
    public int currentHP
    {
        get { return _currentHP; }
        set { _currentHP = Mathf.Max(0, Mathf.Min(value, _maxHP)); }
    }

    private int _maxMP;
    public int maxMP
    {
        get { return _maxMP; }
        set { _maxMP = Mathf.Max(0, value); }
    }

    private int _currentMP;
    public int currentMP
    {
        get { return _currentMP; }
        set { _currentMP = Mathf.Max(0, Mathf.Min(value, _maxMP)); }
    }

    //[Space(20)]

    //[Header("Movement")]



    public static PlayerData Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
}
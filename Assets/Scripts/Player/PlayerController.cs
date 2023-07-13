using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInstance _player;

    private void Start()
    {
    }

    private void Update()
    {
        if (true) // replace later with key to press for interactions
        {
            if (_player.currentInteractable != null)
            {
                _player.currentInteractable.Interact(_player);
            }
        }
    }
}
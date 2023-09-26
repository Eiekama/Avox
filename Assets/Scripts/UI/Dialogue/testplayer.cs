using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testplayer : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    private const float speed = 10;
    private Rigidbody2D rb;
    public testinteract Interactable {get;set;}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {   
        if (dialogueUI.IsOpen) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        transform.position = transform.position + new Vector3 (horizontalInput, 0, 0)*speed*Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable?.Interact(this);
        }
    }
}

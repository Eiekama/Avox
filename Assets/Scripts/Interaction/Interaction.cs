using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class Interaction : IInteraction
{
    public void OpenInteractableIcon(AInteractable interactable)
    {
        if (interactable.interactUI != null)
            interactable.interactUI.ChangeAppearance(true);
    }
    public void CloseInteractableIcon(AInteractable interactable)
    {
        if (interactable.interactUI != null)
            interactable.interactUI.ChangeAppearance(false);
    }
}

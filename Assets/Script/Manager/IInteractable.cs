using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Interact(Player player = null);
}

public interface IPickable
{
    public void Interact(Player player = null);
}

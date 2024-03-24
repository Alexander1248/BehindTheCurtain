using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerInteract playerInteract);
    string tipButton { get; set; }
    string tipName { get; set; }
    string tipNameRUS { get; set; }

    MeshRenderer[] meshesOutline { get; set; }
}
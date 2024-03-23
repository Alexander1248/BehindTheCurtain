using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableBeginning : MonoBehaviour, IInteractable
{
   [SerializeField] private string _tipButton;
    [SerializeField] private string _tipName;

    public string tipButton { get; set; }
    public string tipName { get; set; }

    [SerializeField] private MeshRenderer[] _meshesOutline;
    public MeshRenderer[] meshesOutline { get { return _meshesOutline; } set { } }

    [SerializeField] private BeginningManager beginningManager;

    void Awake()
    {
        tipButton = _tipButton; tipName = _tipName;
    }

    public void Interact(PlayerInteract playerInteract)
    {
        beginningManager.startGame();
    }
}

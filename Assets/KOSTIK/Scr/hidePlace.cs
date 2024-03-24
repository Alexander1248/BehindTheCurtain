using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidePlace : MonoBehaviour, IInteractable
{
    [SerializeField] private string _tipButton;
    [SerializeField] private string _tipName;
    [SerializeField] private string _tipNameRUS;

    public string tipButton { get; set; }
    public string tipName { get; set; }
    public string tipNameRUS { get; set; }

    [SerializeField] private MeshRenderer[] _meshesOutline;
    public MeshRenderer[] meshesOutline { get { return _meshesOutline; } set { } }

    [SerializeField] private HidePart hidePart;
    [SerializeField] private int myID;

    void Awake()
    {
        tipButton = _tipButton; tipName = _tipName; tipNameRUS = _tipNameRUS;
    }

    public void Interact(PlayerInteract playerInteract)
    {
        hidePart.hidePlayer(myID);
    }
}

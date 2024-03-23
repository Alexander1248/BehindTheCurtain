using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private List<string> tagsInteract;
    [SerializeField] private List<string> tagsLook;
    [SerializeField] private float distInteract;

    [SerializeField] private Material outlineMat;

    private int idxInteract = -1;
    private IInteractable interactable;
    private GameObject interactableOBJ;
    private bool needToHideDescription;

    [SerializeField] private GameObject UITip;
    [SerializeField] private TMP_Text textTip_button;
    [SerializeField] private TMP_Text textTip_tip;

    [SerializeField] private TMP_Text textLook;

    void Start()
    {
        InvokeRepeating("raycasting", 0.1f, 0.1f);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && idxInteract != -1) InteractObj();
    }

    void raycasting()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            checkObject(hit.transform.gameObject);
        }
        else
        {
            idxInteract = -1;
            interactableOBJ = null;
            hideTip();
            if (needToHideDescription) hideDescription();
        }
    }

    void InteractObj()
    {
        if (Vector3.Distance(transform.position, interactableOBJ.transform.position) > distInteract) return;
        interactable.Interact(this);
    }

    void checkObject(GameObject obj)
    {
        idxInteract = tagsInteract.IndexOf(obj.tag);
        if (idxInteract == -1)
        {
            interactableOBJ = null;
            hideTip();
            return;
        }
        if (interactableOBJ != obj && Vector3.Distance(transform.position, obj.transform.position) <= distInteract)
        {
            hideTip();

            interactableOBJ = obj;
            interactable = obj.GetComponent<IInteractable>();
            showTip(interactable.tipButton, interactable.tipName);
        }
        else if (Vector3.Distance(transform.position, obj.transform.position) > distInteract)
        {
            hideTip();
            idxInteract = -1;
            interactableOBJ = null;
            interactable = null;
        }

        //if (idxInteract == 0) obj.GetComponent<door>().Interact();
    }


    private void ShowSubtitle(string obj)
    {
        needToHideDescription = false;
        showDescription(obj);
        CancelInvoke("hideDescription");
        Invoke("hideDescription", 2);
    }

    void showDescription(string description)
    {
        textLook.text = description;
    }

    void hideDescription()
    {
        needToHideDescription = false;
        textLook.text = "";
    }

    void showTip(string button, string tip)
    {
        UITip.SetActive(true);
        textTip_button.text = button;
        textTip_tip.text = tip;

        MeshRenderer[] meshes = interactable.meshesOutline;
        for(int i = 0; i < meshes.Length; i++)
        {
            Material[] mats = meshes[i].materials;
            Material[] newArray = new Material[mats.Length + 1];
            for (int j = 0; j < mats.Length; j++) newArray[j] = mats[j];
            newArray[newArray.Length - 1] = outlineMat;

            meshes[i].materials = newArray;
        }
    }

    void hideTip()
    {
        if (!UITip.activeSelf) return;
        UITip.SetActive(false);
        if (interactable == null) return;
        MeshRenderer[] meshes = interactable.meshesOutline;
        for (int i = 0; i < meshes.Length; i++)
        {
            Material[] mats = meshes[i].materials;
            Material[] newArray = new Material[mats.Length - 1];
            for (int j = 0; j < mats.Length - 1; j++) newArray[j] = mats[j];
            meshes[i].materials = newArray;
        }
    }
}

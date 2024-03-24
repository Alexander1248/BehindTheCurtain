using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class door : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator animator; 
    public Transform doorObj;
    private float t;
    private float startAngle;
    private float endAngle;
    private string state = "CloseDoor";

    [SerializeField] private bool locked = true;

    private Transform plr;

    [SerializeField] private string lockedMessage = "LOCKED";

    [SerializeField] private string _tipButton;
    [SerializeField] private string _tipName;
        [SerializeField] private string _tipNameRUS;

    public string tipButton { get; set; }
        public string tipNameRUS { get; set; }
    public string tipName { get; set; }

    [SerializeField] private MeshRenderer[] _meshesOutline;
    public MeshRenderer[] meshesOutline { get { return _meshesOutline; } set { } }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    void Awake()
    {
        tipButton = _tipButton; tipName = _tipName; tipNameRUS = _tipNameRUS;
    }
    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Interact(PlayerInteract playerInteract)
    {
        if (locked)
        {
            audioSource.clip = clips[0];
            audioSource.Play();
            if (!animator.enabled) animator.enabled = true;
            animator.CrossFade("LockedDoor", 0.1f, 0, 0);
            return;
        }
        animator.enabled = false;
        audioSource.clip = clips[1];
        audioSource.Play();

        if (state == "CloseDoor") state = "OpenDoor";
        else state = "CloseDoor";

        startAngle = doorObj.localEulerAngles.y;
        Vector3 direction = plr.position - doorObj.position;
        float dotProduct = Vector3.Dot(direction, doorObj.right);
        if (state == "OpenDoor" && dotProduct >= 0) endAngle = -90;
        else if (state == "OpenDoor" && dotProduct < 0) endAngle = 90;
        else endAngle = 0;
        t = 0.01f;
    }

    void Update()
    {
        if (t == 0) return;
        t += Time.deltaTime;

        doorObj.localEulerAngles = new Vector3(0, Mathf.LerpAngle(startAngle, endAngle, t), 0);

        if (t >= 1) t = 0;
    }

}

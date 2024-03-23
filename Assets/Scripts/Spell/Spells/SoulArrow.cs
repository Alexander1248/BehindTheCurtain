using UnityEngine;
using UnityEngine.UI;

public class SoulArrow : Spell
{
    public GameObject prefab;
    public AudioSource sound;
    public Image icon;

    private new void Awake()
    {
        base.Awake();
    }

    public override void Selected()
    {
        icon.enabled = true;
    }

    public override void Deselected()
    {
        icon.enabled = false;
    }

    public override void Cast(Vector3 position, Quaternion rotation)
    {
        var dir = rotation * Vector3.forward;
        Instantiate(prefab, position + dir * 2, rotation);
        sound.Play();
    }
}
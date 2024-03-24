
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    private Sprite mana;

    private int _max;
    private Image[] _images;
    
    public void Initialize(int max)
    {
        _max = max;
        _images = new Image[max];
        for (int i = 0; i < max; i++)
        {
            GameObject obj = new GameObject("M" + i);
            var t = obj.AddComponent<RectTransform>();
            t.SetParent(transform);
            t.localScale = new Vector3(0.4f, 0.4f);
            t.anchorMax = t.anchorMin = new Vector2(1, 0);
            t.anchoredPosition = new Vector2(-(25 + (i % 2) * 50), 25 + (i / 2) * 50);
            _images[i] = obj.AddComponent<Image>();
            _images[i].sprite = mana;
        }
    }

    public void Set(int value)
    {
        for (int i = 0; i < value; i++)
            _images[i].enabled = true;
        
        for (int i = value; i < _max; i++)
            _images[i].enabled = false;
    }
}

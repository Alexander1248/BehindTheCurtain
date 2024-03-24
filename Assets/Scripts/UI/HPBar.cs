using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    
    public void Set(float value, float max)
    {
        bar.fillAmount = Mathf.Clamp01(value / max);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    
    public void Set(float value, float max)
    {
        // TODO: Этот класс лишь обертка, от него можно избавится, вставив функционал в другой класс
        bar.fillAmount = Mathf.Min(1, value / max);
    }
}

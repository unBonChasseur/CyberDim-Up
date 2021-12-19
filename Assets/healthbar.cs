using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public Slider slide;

    public void SetMaxHp(int health) 
    {
        slide.maxValue = health;
        slide.value = health;
    }
    public void SetHp(int health)
    {
        slide.value = health;
    }

}

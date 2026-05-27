using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private Image fill;
    public TMP_Text healthText;

    void Awake()
    {
        slider = GetComponent<Slider>();
        fill = transform.Find("Fill Area/Fill").GetComponent<Image>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        UpdateText();
        UpdateColor();
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        UpdateText();
        UpdateColor();
    }

    void UpdateText()
    {
        healthText.text = slider.value + " / " + slider.maxValue;
    }

    void UpdateColor()
    {
        float percentage = slider.value / slider.maxValue;

        if (percentage > 0.5f)
            fill.color = Color.Lerp(Color.yellow, Color.green, (percentage - 0.5f) * 2f);
        else
            fill.color = Color.Lerp(Color.red, Color.yellow, percentage * 2f);
    }
}

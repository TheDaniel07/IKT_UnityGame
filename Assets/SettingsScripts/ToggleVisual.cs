using UnityEngine;
using UnityEngine.UI;

public class ToggleVisual : MonoBehaviour
{
    public Image background;
    public RectTransform knob;

    public Color colorOn = new Color(0.91f, 0.44f, 0.13f);
    public Color colorOff = new Color(0.55f, 0.55f, 0.55f);

    public float knobOnX = 12f;
    public float knobOffX = -12f;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(UpdateVisual);
        UpdateVisual(_toggle.isOn);
    }

    private void UpdateVisual(bool isOn)
    {
        background.color = isOn ? colorOn : colorOff;
        knob.anchoredPosition = new Vector2(isOn ? knobOnX : knobOffX, 0f);
    }
}
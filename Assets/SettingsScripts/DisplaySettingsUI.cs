using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DisplaySettingsUI : MonoBehaviour
{
    [Header("Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown windowModeDropdown;
    public TMP_Dropdown fpsCapDropdown;

    [Header("Toggle")]
    public Toggle vsyncToggle;

    [Header("FPS row (Szürke amikor a V-Sync be van kapcsolva)")]
    public GameObject fpsCapRow;

    [Header("Buttons")]
    public Button applyButton;
    public Button cancelButton;
    public Button resetDefaultsButton;

    private DisplaySettingsManager _mgr;

    private void Start()
    {
        _mgr = DisplaySettingsManager.Instance;

        PopulateDropdowns();
        RefreshUIFromSettings(_mgr.GetPending());
        RegisterCallbacks();
    }

    private void PopulateDropdowns()
    {
        var resOptions = new List<string>();
        foreach (var r in _mgr.AvailableResolutions)
            resOptions.Add($"{r.width} x {r.height}");
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resOptions);

        windowModeDropdown.ClearOptions();
        windowModeDropdown.AddOptions(new List<string>(DisplaySettingsManager.WindowModeLabels));

        fpsCapDropdown.ClearOptions();
        fpsCapDropdown.AddOptions(new List<string>(DisplaySettingsManager.FpsCapLabels));
    }

    private void RefreshUIFromSettings(DisplaySettings s)
    {
        resolutionDropdown.SetValueWithoutNotify(s.resolutionIndex);
        windowModeDropdown.SetValueWithoutNotify(s.windowMode);
        vsyncToggle.SetIsOnWithoutNotify(s.vsyncEnabled);
        fpsCapDropdown.SetValueWithoutNotify(s.fpsCapIndex);

        UpdateFpsRowInteractability(s.vsyncEnabled);
    }

    private void RegisterCallbacks()
    {
        resolutionDropdown.onValueChanged.AddListener(_ => StageCurrentUI());
        windowModeDropdown.onValueChanged.AddListener(_ => StageCurrentUI());
        fpsCapDropdown.onValueChanged.AddListener(_ => StageCurrentUI());

        vsyncToggle.onValueChanged.AddListener(vsyncOn =>
        {
            UpdateFpsRowInteractability(vsyncOn);
            StageCurrentUI();
        });

        applyButton.onClick.AddListener(OnApply);
        cancelButton.onClick.AddListener(OnCancel);
        resetDefaultsButton.onClick.AddListener(OnResetDefaults);
    }

    private void StageCurrentUI()
    {
        _mgr.Stage(new DisplaySettings
        {
            resolutionIndex = resolutionDropdown.value,
            windowMode = windowModeDropdown.value,
            vsyncEnabled = vsyncToggle.isOn,
            fpsCapIndex = fpsCapDropdown.value
        });
    }

    private void OnApply() => _mgr.Apply();
    private void OnCancel() { _mgr.Cancel(); RefreshUIFromSettings(_mgr.GetPending()); }
    private void OnResetDefaults() { _mgr.RevertToDefaults(); RefreshUIFromSettings(_mgr.GetPending()); }

    private void UpdateFpsRowInteractability(bool vsyncOn)
    {
        var cg = fpsCapRow.GetComponent<CanvasGroup>();
        if (cg == null) cg = fpsCapRow.AddComponent<CanvasGroup>();

        cg.alpha = vsyncOn ? 0.4f : 1f;
        cg.interactable = !vsyncOn;
        cg.blocksRaycasts = !vsyncOn;
    }
}
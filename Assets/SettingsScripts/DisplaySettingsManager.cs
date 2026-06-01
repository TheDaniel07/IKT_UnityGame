using UnityEngine;

public class DisplaySettingsManager : MonoBehaviour
{
    public static DisplaySettingsManager Instance { get; private set; }

    private const string PREFS_KEY = "DisplaySettings";

    public DisplaySettings Current { get; private set; } = new DisplaySettings();
    private DisplaySettings _pending = new DisplaySettings();

    public Resolution[] AvailableResolutions { get; private set; }

    public static readonly string[] WindowModeLabels =
        { "Fullscreen", "Windowed", "Borderless windowed" };

    public static readonly int[] FpsCapValues = { 30, 45, 60, 90, 120, 144, 240, -1 };
    public static readonly string[] FpsCapLabels = { "30", "45", "60", "90", "120", "144", "240", "Unlimited" };

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        AvailableResolutions = Screen.resolutions;
        Load();
    }

    public void Stage(DisplaySettings settings) => _pending = settings;
    public DisplaySettings GetPending() => Clone(_pending);

    public void Apply()
    {
        Current = Clone(_pending);
        Save();
        ApplyToScreen(Current);
    }

    public void Cancel() => _pending = Clone(Current);

    public void RevertToDefaults()
    {
        _pending = new DisplaySettings
        {
            resolutionIndex = GetCurrentResolutionIndex(),
            windowMode = 0,
            vsyncEnabled = true,
            fpsCapIndex = 2
        };
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(PREFS_KEY))
        {
            Current = JsonUtility.FromJson<DisplaySettings>(PlayerPrefs.GetString(PREFS_KEY));
            _pending = Clone(Current);
        }
        else
        {
            Current = new DisplaySettings
            {
                resolutionIndex = GetCurrentResolutionIndex(),
                windowMode = Screen.fullScreen ? 0 : 1,
                vsyncEnabled = QualitySettings.vSyncCount > 0,
                fpsCapIndex = 2
            };
            _pending = Clone(Current);
        }

        ApplyToScreen(Current);
    }

    private void Save()
    {
        PlayerPrefs.SetString(PREFS_KEY, JsonUtility.ToJson(Current));
        PlayerPrefs.Save();
    }

    private void ApplyToScreen(DisplaySettings s)
    {
        FullScreenMode mode = s.windowMode switch
        {
            0 => FullScreenMode.ExclusiveFullScreen,
            1 => FullScreenMode.Windowed,
            2 => FullScreenMode.FullScreenWindow,
            _ => FullScreenMode.ExclusiveFullScreen
        };

        int idx = Mathf.Clamp(s.resolutionIndex, 0, AvailableResolutions.Length - 1);
        Resolution r = AvailableResolutions[idx];
        Screen.SetResolution(r.width, r.height, mode);

        QualitySettings.vSyncCount = s.vsyncEnabled ? 1 : 0;
        Application.targetFrameRate = s.vsyncEnabled ? -1 : FpsCapValues[s.fpsCapIndex];
    }

    private int GetCurrentResolutionIndex()
    {
        for (int i = 0; i < AvailableResolutions.Length; i++)
            if (AvailableResolutions[i].width == Screen.currentResolution.width &&
                AvailableResolutions[i].height == Screen.currentResolution.height)
                return i;
        return AvailableResolutions.Length - 1;
    }

    private static DisplaySettings Clone(DisplaySettings s) =>
        JsonUtility.FromJson<DisplaySettings>(JsonUtility.ToJson(s));
}
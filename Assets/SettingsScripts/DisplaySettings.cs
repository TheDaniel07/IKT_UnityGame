[System.Serializable]
public class DisplaySettings
{
    public int resolutionIndex = -1;
    public int windowMode = 0;    // 0=Fullscreen, 1=Windowed, 2=Borderless
    public bool vsyncEnabled = true;
    public int fpsCapIndex = 2;
}
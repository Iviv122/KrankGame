using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings", order = 0)]
public class Settings : ScriptableObject
{
    public float MouseSensX;
    public float MouseSensY;
    public float Volume;
    public bool fullscreen;
    public int ResIndex;
    public event Action Updated;
    public float FOV;
    public int VSync;
    public int TargetFrameRate;
    public void Load()
    {
        MouseSensX = PlayerPrefs.GetFloat("MouseX", 0.1f);
        MouseSensY = PlayerPrefs.GetFloat("MouseY", 0.1f);

        FOV = PlayerPrefs.GetFloat("FOV", 90f);
        Volume = PlayerPrefs.GetFloat("Volume", 1);

        fullscreen = PlayerPrefs.GetInt("Fullscreen", 1) != 0;
        ResIndex = PlayerPrefs.GetInt("ResolutionIndex", GetDefaultResolutionIndex());

        VSync = PlayerPrefs.GetInt("VSync", 0);
        TargetFrameRate = PlayerPrefs.GetInt("FPS",60);
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("MouseX", MouseSensX);
        PlayerPrefs.SetFloat("MouseY", MouseSensY);
        PlayerPrefs.SetInt("Fullscreen", fullscreen ? 1 : 0);
        PlayerPrefs.SetInt("ResolutionIndex", ResIndex);
        PlayerPrefs.SetFloat("FOV", FOV);
        PlayerPrefs.SetFloat("Volume", Volume);
        PlayerPrefs.SetInt("VSync", VSync);
        PlayerPrefs.SetInt("FPS",TargetFrameRate);
        PlayerPrefs.Save();
        Updated?.Invoke();
    }
    //
    // ---- Util -----
    //
    private int GetDefaultResolutionIndex()
    {
        Resolution[] resolutions = Screen.resolutions;
        Resolution current = Screen.currentResolution;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == current.width && resolutions[i].height == current.height)
                return i;
        }

        return resolutions.Length - 1;
    }
    public void GlobalSettings()
    {
        if (fullscreen)
        {

            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        Resolution[] resolutions = Screen.resolutions;

        ResIndex = Mathf.Clamp(ResIndex, 0, resolutions.Length - 1);
        Resolution chosenRes = resolutions[ResIndex];

        Screen.SetResolution(chosenRes.width, chosenRes.height, fullscreen);

        QualitySettings.vSyncCount = VSync;
        Application.targetFrameRate = TargetFrameRate;
    }

}

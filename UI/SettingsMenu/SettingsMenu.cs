using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] UIDocument ui;
    [SerializeField] Settings settings;
    private void OnEnable()
    {

        var root = ui.rootVisualElement;

        Button exitButton = root.Q<Button>("exit");
        Toggle fullscreen = root.Q<Toggle>("fullscreen");
        Slider mouseSensXSlider = root.Q<Slider>("MouseX");
        Slider mouseSensYSlider = root.Q<Slider>("MouseY");
        Slider volumeSlider = root.Q<Slider>("volume");
        DropdownField ResolutionDropDown = root.Q<DropdownField>("resolution");
        IntegerField MaxFpsInput = root.Q<IntegerField>("fps");
        Toggle VSync = root.Q<Toggle>("vsync");
        Slider FovSlider = root.Q<Slider>("fov");
       
        settings.Load();

        fullscreen.value = settings.fullscreen;
        mouseSensXSlider.value = settings.MouseSensX;
        mouseSensYSlider.value = settings.MouseSensY;
        volumeSlider.value = settings.Volume;

        FovSlider.value = settings.FOV;
        MaxFpsInput.value = settings.TargetFrameRate;
        VSync.value = settings.VSync != 0;

        ResolutionDropDown.choices = GetResolutions();
        ResolutionDropDown.value = ResolutionDropDown.choices[settings.ResIndex];


        exitButton.RegisterCallback<MouseUpEvent>((env) => gameObject.SetActive(false));

        VSync.RegisterValueChangedCallback(evt =>
        {
            settings.VSync = evt.newValue ? 1 : 0;
            settings.Save();
        });

        MaxFpsInput.RegisterValueChangedCallback(evt =>
        {
            settings.TargetFrameRate = evt.newValue;
            settings.Save();
        });

        volumeSlider.RegisterValueChangedCallback(evt =>
        {
            settings.Volume = evt.newValue;
            settings.Save();
        });

        fullscreen.RegisterValueChangedCallback(evt =>
        {
            settings.fullscreen = evt.newValue;
            Screen.fullScreen = evt.newValue;
            settings.Save();
        });

        mouseSensXSlider.RegisterValueChangedCallback(evt =>
        {
            settings.MouseSensX = evt.newValue;
            settings.Save();
        });
        FovSlider.RegisterValueChangedCallback(evt =>
        {
            settings.FOV = evt.newValue;
            settings.Save();
        });
        mouseSensYSlider.RegisterValueChangedCallback(evt =>
        {
            settings.MouseSensY = evt.newValue;
            settings.Save();
        });
        ResolutionDropDown.RegisterValueChangedCallback(evt =>
        {
            settings.ResIndex = ResolutionDropDown.choices.IndexOf(evt.newValue);
            settings.Save();
        });
    }
    private List<string> GetResolutions()
    {
        Resolution[] AllResolutions = Screen.resolutions;
        List<string> resolutionStringList = new List<string>();
        string newRes;

        for (int i = 0; i < AllResolutions.Length; i++)
        {
            Resolution res = AllResolutions[i];
            newRes = res.width + "x" + res.height;
            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);

            }
        }
        return resolutionStringList;
    }
}
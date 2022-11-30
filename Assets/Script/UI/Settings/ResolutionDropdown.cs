using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionDropdown : MonoBehaviour
{
    Resolution[] resolutions;
    private void Awake()
    {
        bool isOpenForTheFirstTime = Convert.ToBoolean(PlayerPrefs.GetInt("GameOpenedForFirstTime", 0));
        int currentResolution = PlayerPrefs.GetInt("CurrentResolution");
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add($"{resolutions[i].width} X {resolutions[i].height}");
            if (isOpenForTheFirstTime && resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                isOpenForTheFirstTime = false;
                PlayerPrefs.SetInt("GameOpenedForFirstTime", Convert.ToInt32(isOpenForTheFirstTime));
                currentResolution = i;
                PlayerPrefs.SetInt("CurrentResolution", i);
            }
        }
        TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
        dropdown.value = currentResolution;
        dropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        FindObjectOfType<CanvasScaler>().referenceResolution = new Vector2(resolution.width, resolution.height);
        Screen.SetResolution(resolution.height, resolution.height, Screen.fullScreen);
    }
}

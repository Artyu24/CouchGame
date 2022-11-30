using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string exposedFielName;

    private void Awake()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat(exposedFielName + "Volume Value", 1);
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat(exposedFielName + "Volume Value", value);
        mixer.SetFloat(exposedFielName, value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider BGMSlider;
    public Slider EffectSlider;

    // Start is called before the first frame update
    void Start()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        EffectSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
    }

    public void SetBGMLevel(float sliderValue)
    {
        mixer.SetFloat("BGMParam", Mathf.Log10(sliderValue) *20);
        PlayerPrefs.SetFloat("BGMVolume", sliderValue);
    }

    public void SetEffectsLevel(float sliderValue)
    {
        mixer.SetFloat("EffectsParam", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("EffectsVolume", sliderValue);
    }
}

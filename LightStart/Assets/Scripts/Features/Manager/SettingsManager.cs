using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class SettingsDefaultValues
{
    public const float BGM_VOL = 0.5f;

    public const float VOICE_VOL = 1;

    public const float MAX_SENSITIVITY = 200;
    public const float MIN_SENSITIVITY = 1;
    public const float SENSITIVITY = 100;

    public const float MAX_FOV = 120;
    public const float MIN_FOV = 10;
    public const float AIM_FOV = 20;
    public const float FOV = 90;
    public const float AIM_COEF = AIM_FOV / FOV;
}

public class SettingsManager : MonoBehaviour
{
    private const int FPS = 60;

    [SerializeField]
    private SettingsSliders settingsSliders;

    [SerializeField]
    private AudioSource bgm;
    [SerializeField]
    private AudioSource voice;
    [SerializeField]
    private Hero hero;

    private void Awake()
    {
        SetQualitySettings();
        InitializeSliders();
        InitializeProperties();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CheckFloatValues(string key, float min, float max) 
    {
        return (PlayerPrefs.HasKey(key) &&
            PlayerPrefs.GetFloat(key) >= min &&
            PlayerPrefs.GetFloat(key) <= max);
    }

    void SetQualitySettings() 
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = FPS;
    }

    void InitializeSliders() 
    {
        if (settingsSliders.BgmVol != null)
        {
            settingsSliders.BgmVol.value =
                  (!CheckFloatValues("BGMVol", 0.0f, 1.0f)) ? SettingsDefaultValues.BGM_VOL : PlayerPrefs.GetFloat("BGMVol");

            settingsSliders.BgmVol.onValueChanged.AddListener(delegate { ChangeBGMVol(settingsSliders.BgmVol.value); });
        }

        if (settingsSliders.VoiceVol != null)
        {
            settingsSliders.VoiceVol.value =
                  (!CheckFloatValues("VoiceVol", 0.0f, 1.0f)) ? 
                  SettingsDefaultValues.VOICE_VOL : PlayerPrefs.GetFloat("VoiceVol");

            settingsSliders.VoiceVol.onValueChanged.AddListener(delegate { ChangeVoiceVol(settingsSliders.VoiceVol.value); });
        }

        if (settingsSliders.Sensitivity != null)
        {
            settingsSliders.Sensitivity.minValue = SettingsDefaultValues.MIN_SENSITIVITY;
            settingsSliders.Sensitivity.maxValue = SettingsDefaultValues.MAX_SENSITIVITY;

            settingsSliders.Sensitivity.value =
                  (!CheckFloatValues("Sencitivity", SettingsDefaultValues.MIN_SENSITIVITY, 
                  SettingsDefaultValues.MAX_SENSITIVITY)) ?
                  SettingsDefaultValues.SENSITIVITY : PlayerPrefs.GetFloat("Sencitivity");

            settingsSliders.Sensitivity.onValueChanged.AddListener(delegate {
                ChangeSensitivity(settingsSliders.Sensitivity.value);
            });
        }

        if (settingsSliders.FOV != null)
        {
            settingsSliders.FOV.maxValue = SettingsDefaultValues.MAX_FOV;
            settingsSliders.FOV.minValue = SettingsDefaultValues.MIN_FOV;

            settingsSliders.FOV.value =
                  (!CheckFloatValues("FOV", SettingsDefaultValues.MIN_FOV, SettingsDefaultValues.MAX_FOV)) ? 
                  SettingsDefaultValues.FOV : PlayerPrefs.GetFloat("FOV");

            settingsSliders.FOV.onValueChanged.AddListener(delegate {
                ChangeFOV(settingsSliders.FOV.value);
            });
        }
    }

    void InitializeProperties() 
    {
        if (bgm != null)
            bgm.volume =
                (!CheckFloatValues("BGMVol", 0.0f, 1.0f)) ? SettingsDefaultValues.BGM_VOL : PlayerPrefs.GetFloat("BGMVol");

        if (voice != null)
            voice.volume =
                (!CheckFloatValues("VoiceVol", 0.0f, 1.0f)) ? 
                SettingsDefaultValues.VOICE_VOL : PlayerPrefs.GetFloat("VoiceVol");

        if (hero != null)
        {
            hero.overview.sensitivity = 
                (!CheckFloatValues("Sencitivity", SettingsDefaultValues.MIN_SENSITIVITY, 
                SettingsDefaultValues.MAX_SENSITIVITY)) ?
                SettingsDefaultValues.SENSITIVITY : PlayerPrefs.GetFloat("Sencitivity");

            float fov = (!CheckFloatValues("FOV", SettingsDefaultValues.MIN_FOV, SettingsDefaultValues.MAX_FOV)) ?
                SettingsDefaultValues.FOV : PlayerPrefs.GetFloat("FOV");

            hero.overview.defaultFOV = fov;
            hero.overview.aimingFOV = fov * SettingsDefaultValues.AIM_COEF;
        }
    }

    public void ResetToDefault() 
    {
        ChangeBGMVol(SettingsDefaultValues.BGM_VOL);
        if (settingsSliders.BgmVol != null)
            settingsSliders.BgmVol.value = SettingsDefaultValues.BGM_VOL;

        ChangeVoiceVol(SettingsDefaultValues.VOICE_VOL);
        if (settingsSliders.VoiceVol != null)
            settingsSliders.VoiceVol.value = SettingsDefaultValues.VOICE_VOL;

        ChangeSensitivity(SettingsDefaultValues.SENSITIVITY);
        if (settingsSliders.Sensitivity != null)
            settingsSliders.Sensitivity.value = SettingsDefaultValues.SENSITIVITY;

        ChangeFOV(SettingsDefaultValues.FOV);
        if (settingsSliders.FOV != null)
            settingsSliders.FOV.value = SettingsDefaultValues.FOV;
    }

    public void ChangeBGMVol(System.Single val) 
    {
        if (bgm != null)
            bgm.volume = val;

        PlayerPrefs.SetFloat("BGMVol", val);
        PlayerPrefs.Save();
    }
    public void ChangeVoiceVol(System.Single val)
    {
        if (voice != null)
            voice.volume = val;

        PlayerPrefs.SetFloat("VoiceVol", val);
        PlayerPrefs.Save();
    }

    public void ChangeSensitivity(System.Single val) 
    {
        if (hero != null)
            hero.overview.sensitivity = val;

        PlayerPrefs.SetFloat("Sencitivity", val);
        PlayerPrefs.Save();
    }

    public void ChangeFOV(System.Single val)
    {
        if (hero != null)
        { 
            hero.overview.defaultFOV = val;
            hero.overview.aimingFOV = val * SettingsDefaultValues.AIM_COEF;
        }

        PlayerPrefs.SetFloat("FOV", val);
        PlayerPrefs.Save();
    }
}


[System.Serializable]
class SettingsSliders
{
    public Slider BgmVol;
    public Slider VoiceVol;
    public Slider Sensitivity;
    public Slider FOV;
}


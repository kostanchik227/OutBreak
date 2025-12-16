using UnityEngine;
using UnityEngine.UI;

public class SoundSlidersValue : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musikSlider;
    [SerializeField] private Slider effectsSlider;

    private void Start()
    {
        SettingsData data = SettingsLoader.Instance.GetSettingsData();
        SetVolume(data.globalVolume, data.effectsVolume, data.musikVolume);
    }
    public void SetVolume(float valueMaster, float effectsMaster, float musikMaster)
    {
        masterSlider.value = valueMaster;
        effectsSlider.value = effectsMaster;
        musikSlider.value = musikMaster;
    }
}

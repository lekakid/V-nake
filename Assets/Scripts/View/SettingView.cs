using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingView : CanvasView
{
    public Slider MasterSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;

    public new void Show() {
        base.Show();
        
        MasterSlider.Select();
    }

    public void SetValue(Dictionary<string, float> volumes) {
        MasterSlider.normalizedValue = volumes["Master"];
        BGMSlider.normalizedValue = volumes["BGM"];
        SFXSlider.normalizedValue = volumes["SFX"];
    }
}

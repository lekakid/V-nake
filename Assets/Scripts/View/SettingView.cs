using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingView : CanvasView
{
    public VolumeMixer MasterMixer;
    public VolumeMixer BGMMixer;
    public VolumeMixer SFXMixer;

    public void SetValue(int master, int bgm, int sfx) {
        MasterMixer.SetValue(master);
        BGMMixer.SetValue(bgm);
        SFXMixer.SetValue(sfx);
    }

    public void SetSelectColor(VolumeMixer.MixerType type) {
        MasterMixer.SetColor(false);
        BGMMixer.SetColor(false);
        SFXMixer.SetColor(false);

        switch(type) {
            case VolumeMixer.MixerType.MASTER:
                MasterMixer.SetColor(true);
                break;
            case VolumeMixer.MixerType.BGM:
                BGMMixer.SetColor(true);
                break;
            case VolumeMixer.MixerType.SFX:
                SFXMixer.SetColor(true);
                break;
        }
    }
}

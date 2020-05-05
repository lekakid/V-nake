using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingView : MonoView
{
    public Text[] TextVolume;
    public Color normal = Color.white;
    public Color selected = Color.yellow;

    int[] VolumeValue;
    int _select;

    void Start() {
        VolumeValue = new int[3];
        
        VolumeValue[0] = (int)(SoundManager.Instance.MasterVolume * 100f);
        VolumeValue[1] = (int)(SoundManager.Instance.BGMVolume * 100f);
        VolumeValue[2] = (int)(SoundManager.Instance.SFXVolume * 100f);

        TextVolume[0].color = selected;
    }

    void InitColor() {
        TextVolume[0].color = normal;
        TextVolume[1].color = normal;
        TextVolume[2].color = normal;
    }

    public void SelectVolumePrev() {
        _select = (_select == 0) ? 0 : _select - 1;
        InitColor();
        TextVolume[_select].color = selected;
    }

    public void SelectVolumeNext() {
        _select = (_select == 2) ? 2 : _select + 1;
        InitColor();
        TextVolume[_select].color = selected;
    }

    public void SelectVolume(int index) {
        _select = index;
        InitColor();
        TextVolume[index].color = selected;
    }

    public void UpVolume() {
        VolumeValue[_select] = (VolumeValue[_select] == 100) ? 100 : VolumeValue[_select] + 5;
        TextVolume[_select].text = string.Format("{0}", VolumeValue[_select]);
        ApplyVolume();
    }

    public void DownVolume() {
        VolumeValue[_select] = (VolumeValue[_select] == 0) ? 0 : VolumeValue[_select] - 5;
        TextVolume[_select].text = string.Format("{0}", VolumeValue[_select]);
        ApplyVolume();
    }

    public void UpVolume(int index) {
        SelectVolume(index);
        VolumeValue[index] = (VolumeValue[index] == 100) ? 100 : VolumeValue[index] + 5;
        TextVolume[index].text = string.Format("{0}", VolumeValue[index]);
        ApplyVolume();
    }

    public void DownVolume(int index) {
        SelectVolume(index);
        VolumeValue[index] = (VolumeValue[index] == 0) ? 0 : VolumeValue[index] - 5;
        TextVolume[index].text = string.Format("{0}", VolumeValue[index]);
        ApplyVolume();
    }

    void ApplyVolume() {
        SoundManager.Instance.MasterVolume = VolumeValue[0] / 100f;
        SoundManager.Instance.BGMVolume = VolumeValue[1] / 100f;
        SoundManager.Instance.SFXVolume = VolumeValue[2] / 100f;
    }
}

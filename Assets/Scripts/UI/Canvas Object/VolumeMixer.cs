using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMixer : MonoBehaviour
{
    public enum MixerType { MASTER, BGM, SFX }
    
    [SerializeField] Text TextValue = null;

    [Header("COLOR")]
    [SerializeField] Color NORMAL_COLOR = Color.white;
    [SerializeField] Color SELECTED_COLOR = Color.yellow;
    
    public void SetValue(int value) {
        TextValue.text = string.Format("{0}", value);
    }

    public void SetColor(bool isSelect) {
        if(isSelect)
            TextValue.color = SELECTED_COLOR;
        else
            TextValue.color = NORMAL_COLOR;
    }
}

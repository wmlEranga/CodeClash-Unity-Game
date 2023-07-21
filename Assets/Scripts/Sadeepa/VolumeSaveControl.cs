using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSaveControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;

    [SerializeField] private Text volumeTEXTUI = null;

    public void VolumeSlider(float volume)
    {
        volumeTEXTUI.text = volume.ToString("0.0");
    }
}

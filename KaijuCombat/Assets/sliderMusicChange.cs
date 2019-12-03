using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderMusicChange : MonoBehaviour
{
    public void setMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", GetComponent<UnityEngine.UI.Slider>().value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderChanges : MonoBehaviour
{
    public void setSoundVolume()
    {
        PlayerPrefs.SetFloat("soundVolume", GetComponent<UnityEngine.UI.Slider>().value);
    }
}

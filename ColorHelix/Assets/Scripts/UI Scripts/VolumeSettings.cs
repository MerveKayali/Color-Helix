using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class VolumeSettings : MonoBehaviour
{
    public Slider VolumeSlider;
    public AudioMixer mixer;
    public Button ShopButton;
    public void SetVolume()
    {

        mixer.SetFloat("Volume", VolumeSlider.value);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void ShopScene()
    {
        SceneManager.LoadScene(1);
        
    }
}

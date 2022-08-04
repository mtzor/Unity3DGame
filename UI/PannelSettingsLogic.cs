using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelSettingsLogic : MonoBehaviour
{
    public GameObject MuteImage;
    public GameObject EnableImage;
    public GameObject MuteThemeImage;
    public GameObject EnableThemeImage;
    public GameObject PanelSettings;


    bool settingsActive = false;
    bool soundActive = true;
    bool themeActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleSettings()
    {
        settingsActive = !settingsActive;
        PanelSettings.SetActive(settingsActive);
    }

    public void ToggleSound()
    {
        soundActive = !soundActive;
        MuteImage.SetActive(!soundActive);
        EnableImage.SetActive(soundActive);
        FindObjectOfType<AudioMasterLogic>().ToggleSound();
    }

    public void ToggleTheme()
    {
        themeActive = !themeActive;
        MuteThemeImage.SetActive(!themeActive);
        EnableThemeImage.SetActive(themeActive);
        FindObjectOfType<AudioMasterLogic>().ToggleTheme();
    }


}

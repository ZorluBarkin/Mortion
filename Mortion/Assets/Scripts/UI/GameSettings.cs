using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    // FPS Buttons
    // attach in editor
    //public Panel gameSettings;
    public RectTransform settingsPanel;

    public Button button240;
    public Button button120;
    public Button button90;
    public Button button60;
    public Button button30;

    // Audio and Volume
    public Slider masterVolumeSlider;
    private Text masterVolumeText;


    public Text currentFrameRate;

    private string currentTarget;

    private bool open;

    private void Start()
    {
        // set the settings window to close
        open = false;

        // Frame Preference Interface
        // currently every time you start the game fps cap is 60, 61 because of "< 61"
        Application.targetFrameRate = 61;

        //gameSettings.enabled = false;
        settingsPanel.localScale = Vector3.zero; // Panels cannot close like canvas can, so we set the scale to 0.

        button240.onClick.AddListener(Frame240);
        button120.onClick.AddListener(Frame120);
        button90.onClick.AddListener(Frame90);
        button60.onClick.AddListener(Frame60);
        button30.onClick.AddListener(Frame30);

        currentTarget = "60";

        FrameToString();

        // Audio Interface

        // resolution settings
        //Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && open == false)
        {
            
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f; // stop the gameplay

            //SetVolume(); currently disabled

            settingsPanel.localScale = Vector3.one;
            open = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && open == true)
        {
            settingsPanel.localScale = Vector3.zero;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f; // continue the gameplay
            open = false;
        }
    }

    // Button actions
    private void Frame240()
    {
        Application.targetFrameRate = 241;
        currentTarget = "240";
        FrameToString();
    }

    private void Frame120()
    {
        Application.targetFrameRate = 121;
        currentTarget = "120";
        FrameToString();
    }

    private void Frame90()
    {
        Application.targetFrameRate = 91;
        currentTarget = "90";
        FrameToString();
    }

    private void Frame60()
    {
        Application.targetFrameRate = 61;
        currentTarget = "60";
        FrameToString();
    }

    private void Frame30()
    {
        Application.targetFrameRate = 31;
        currentTarget = "30";
        FrameToString();
    }
    
    // to string method for the frame buttons
    private void FrameToString()
    {
        string concat = ("Current Target Frame Rate: " + currentTarget);
        currentFrameRate.text = concat;
    }

    // Master volume setting method
       // REDO THESE
    /*private void SetVolume()
    {
        PlayerPrefs.SetFloat("VolumeValue", masterVolumeValue);
        float masterVolumeValue = PlayerPrefs.GetFloat("VolumeValue");
        masterVolumeSlider.value = masterVolumeValue;
        AudioListener.volume = masterVolumeValue;

        

    }

    private void MasterVolumeToString()
    {



    }*/
}

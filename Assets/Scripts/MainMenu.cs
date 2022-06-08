using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text energyText;
    [SerializeField] Button playButton;

    [SerializeField] AndroidNotificationsHandler androidNotificationsHandler;
    [SerializeField] iOSNotificationsHandler iOSNotificationsHandler;

    [SerializeField] int maxEnergy;
    [SerializeField] int energyRechargeDuration;

    private int energy;

    private const string energyKey = "Energy";
    private const string energyReadyKey = "EnergyReady";

    void Start()
    {
        OnApplicationFocus(true);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) { return; }

        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoreSystem.highScoreKey, 0);

        highScoreText.text = $"High score: {highScore}";

        energy = PlayerPrefs.GetInt(energyKey, maxEnergy);

        if (energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(energyReadyKey, string.Empty);

            if (energyReadyString == string.Empty) { return; }

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady) 
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(energyKey, energy);
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }
        }

        energyText.text = $"Play ({energy})";
    }

    void EnergyRecharged()
    {
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(energyKey, energy);
        energyText.text = $"Play ({energy})";
    }

    public void Play()
    {
        if(energy < 1) { return; }
        energy--;
        PlayerPrefs.SetInt(energyKey, energy);        
        
        if(energy == 0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(energyReadyKey, energyReady.ToString());
#if UNITY_ANDROID
            androidNotificationsHandler.ScheduleNotification(energyReady);
#elif UNITY_IOS
            iOSNotificationsHandler.ScheduleNotification(energyRechargeDuration);
#endif

        }

        SceneManager.LoadScene(1);
    }
}

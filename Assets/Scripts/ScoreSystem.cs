using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float scoreMultiplier = 1;
    float score;
    public const string highScoreKey = "HighScore";

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(highScoreKey, 0);

        if(score > currentHighScore)
        {
            PlayerPrefs.SetInt(highScoreKey, Mathf.FloorToInt(score));
        }
    }
}

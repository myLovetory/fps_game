using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }

    //lưu
     string highScoreKey = "Điểm cao nhất";
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
 
    }

    public void Save_High_Score(int score)
    {
        PlayerPrefs.SetInt(highScoreKey, score);
    }   
    
    public int Load_High_Score()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            return PlayerPrefs.GetInt(highScoreKey);
        } else
        {
            return 0;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public TMP_Text highScoreUi;

    string newGameScreen = "SampleScene";

  
    public AudioSource main_Channel;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        

        int highScore = SaveLoadManager.Instance.Load_High_Score();
        highScoreUi.text = $"Điểm cao nhất: {highScore}";
    }

    // Update is called once per frame
    public void StartNewGame()
    {
        main_Channel.Stop();
        UnityEngine.SceneManagement.SceneManager.LoadScene(newGameScreen);   
    }

    public void Load_Huong_Dan()
    {
        main_Channel.Stop();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Direction");
    }    

    public void Clear_Player_pref()
    {
        PlayerPrefs.DeleteAll();
    }    
    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }
}

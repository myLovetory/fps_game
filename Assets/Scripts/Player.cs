using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;
    public UnityEngine.UI.Image healthBarImage;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI maxHealthText;

    public GameObject gameOverUI;
    public GameObject weaponPanelUI;
    public GameObject Wave_UI;
    //blood screen
    public GameObject bloody_Screen;
    public bool isDead;


    private void Start()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHP -= damageAmount;

        if (currentHP <= 0)
        {
            currentHP = 0;
            print("bomaychetroi");

            //chết
            PlayerDead();
            isDead = true;
            
        }
        else
        {
            print("bomaydangbidanh");
            StartCoroutine(Bloody_Screen_Effect());
            SoundManager.Instance.player.PlayOneShot(SoundManager.Instance.playerHurt);
        }

        UpdateHealthBar();
    }

    private void PlayerDead()
    {
        SoundManager.Instance.player.PlayOneShot(SoundManager.Instance.playerDeath);

        GetComponent<CharacterController>().enabled = false;
        GetComponent<InputManager>().enabled = false;
        weaponPanelUI.SetActive(false);
        Wave_UI.SetActive(false);
       
        //dying Anim
        GetComponentInChildren<Animator>().enabled = true;

        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(Show_Game_Over_UI());


    }

    private IEnumerator Show_Game_Over_UI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);

        int WaveSurvived = GlobalReference.Instance.WaveNumber;
        if(WaveSurvived - 1 > SaveLoadManager.Instance.Load_High_Score())
        {
            SaveLoadManager.Instance.Save_High_Score(WaveSurvived - 1);
        }

        StartCoroutine(Return_To_Main_Menu());

    }

    private IEnumerator Return_To_Main_Menu()
    {
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("Main menu");
    }

    private IEnumerator Bloody_Screen_Effect()
    {
        if(bloody_Screen.activeInHierarchy == false)
        {
            bloody_Screen.SetActive(true);
        }

        var image = bloody_Screen.GetComponentInChildren<UnityEngine.UI.Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }

        if (bloody_Screen.activeInHierarchy)
        {
            bloody_Screen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if (isDead == false)
            {
                float damage = other.gameObject.GetComponent<ZombieHand>().damage;
                TakeDamage(damage);
            }
        }
    }

    private void UpdateHealthBar()
{
        float healthPercentage = currentHP / maxHP;

        if (currentHP <= 0)
        {
            healthPercentage = 0f;
        }
        healthBarImage.fillAmount = healthPercentage;
        healthText.text = currentHP.ToString();
        
        maxHealthText.text = maxHP.ToString();
    }
}
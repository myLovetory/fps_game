using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;
public class SoundManager : MonoBehaviour
{
    //SINGLE TON 
    public static SoundManager Instance { get; set; }
    
    //pistol
    public AudioSource ShottingSoundPiston2;
    public AudioSource ReloadingPiston2;
    public AudioSource EmptyManagizePistol2;

    //M16
    public AudioSource ShottingSoundM16;
    public AudioSource ReloadingM16;
    public AudioSource EmptyManagizeM16;

    //AKM
    public AudioSource ShottingSoundAKM;
    public AudioSource ReloadingAKM;
    public AudioSource EmptyManagizeAKM;

    public AudioClip zombieWalking;
    public AudioClip zombieChase;
    public AudioClip zombieAttack;
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;

    public AudioSource zombieChanels;
    public AudioSource zombieChanels2;

    public AudioSource player;

    public AudioClip playerHurt;
    public AudioClip playerDeath;

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
    }
    public void PlayShootingSound(WeaponModel weapon)
    { 
           switch (weapon)
           {
                case WeaponModel.Pistol1911:
                    ShottingSoundPiston2.Play();
                    break;
                case WeaponModel.M16:
                    ShottingSoundM16.Play();
                    break;
                case WeaponModel.AKM:
                    ShottingSoundAKM.Play();
                    break;
           }
    }
    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol1911:
                ReloadingPiston2.Play();
                break;
            case WeaponModel.M16:
                ReloadingM16.Play();
                break;
            case WeaponModel.AKM:
                ReloadingAKM.Play();
                break;
        }
    }   
}

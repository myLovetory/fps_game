using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }

    [Header("Ammo")]
    public TextMeshProUGUI managize_Ammo_UI;
    public TextMeshProUGUI total_Ammo_UI;
    public Image ammo_Type_UI;

    [Header("Weapon")]
    public Image active_Weapon_UI;
    public Image unactive_Weapon_UI;

    [Header("Throwable")]
    public Image lethalUI;
    public TextMeshProUGUI lethal_Amount_UI;

    public Image TacticalUI;
    public TextMeshProUGUI TacticalAmount_UI;

    public Sprite emptySlot;

    public GameObject middleDot;


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

    //the fuck UI
    private void Update()
    {
        Weapon active_Weapon = Weapon_Manager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
        Weapon unactive_Weapon = GetUnActiveWeaponSLot().GetComponentInChildren<Weapon>();

        if(active_Weapon)
        {
            //magazine UI
            managize_Ammo_UI.text = $"{active_Weapon.bulletsLeft / active_Weapon.bulletsPerBurst}";
            total_Ammo_UI.text =$"{Weapon_Manager.Instance.checkAmmoLeftFor(active_Weapon.thisWeaponModel)}";

            Weapon.WeaponModel model = active_Weapon.thisWeaponModel;
            ammo_Type_UI.sprite = GetAmmoSprite(model);

            active_Weapon_UI.sprite = GetWeaponSprite(model);

            if(unactive_Weapon)
            {
                unactive_Weapon_UI.sprite = GetWeaponSprite(unactive_Weapon.thisWeaponModel);

            }
        }else
        {
            managize_Ammo_UI.text = "";
            total_Ammo_UI.text = "";

            ammo_Type_UI.sprite = emptySlot;

            active_Weapon_UI.sprite = emptySlot;
            unactive_Weapon_UI.sprite = emptySlot;
        }

    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol1911:
                return Resources.Load<GameObject>("Pistol1911_Weapon").GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.AKM:
                return Resources.Load<GameObject>("AKM_Weapon").GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.M16:
                return Resources.Load<GameObject>("M16_Weapon").GetComponent<SpriteRenderer>().sprite;
            default:
                return null;
        }
    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol1911:
                return Resources.Load<GameObject>("Pistol_Ammo").GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.AKM:
                return Resources.Load<GameObject>("Rifle7_Ammo").GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.M16:
                return Resources.Load<GameObject>("Rifle5_Ammo").GetComponent<SpriteRenderer>().sprite;
            default:
                return null;
        }
    }

    //tìm vũ khí chưa được kích hoạt và trả về nó
    private GameObject GetUnActiveWeaponSLot()
    {
        foreach (GameObject weaponSlot in Weapon_Manager.Instance.weaponSlots)
        {
            if (weaponSlot != Weapon_Manager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }
        return null;
    }

}


    

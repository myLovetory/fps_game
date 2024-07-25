using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Weapon : MonoBehaviour
{
    //active weapon
    public bool isActiveWeapon = false;
    public int weaponDamage;

    [Header("Shotting")]
    //shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    [Header("Burst")]
    //burst
    public int bulletsPerBurst = 3;
    public int BurstBulletsLeft;

    [Header("Spread")]
    //spread 
    public float spreadIntensity;
    public float hipSpreadInstensity;
    public float adsSpreadInstensity;

    [Header("Bullet")]
    //Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public float bulletVecocity = 30;
    //để ko còn lỗ đạn
    public float bulletPrefabLifeTime = 3f;

    //muzzle effect
    public GameObject muzzleEffect;

    internal Animator animator;

    [Header("Loading")]
    // Reloading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloaing;


    //lưu vị trí cầm súng
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    bool isADS;

    public enum WeaponModel
    {
        Pistol1911,
        M16,
        AKM,
        M24
    }

    public WeaponModel thisWeaponModel;


    public enum ShootingMode
    {
        Single,
        Burst,
        Auto,
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        BurstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;
        spreadIntensity = hipSpreadInstensity;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isActiveWeapon)
        {

            //
            foreach(Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Default");
            }    


            if(Input.GetMouseButtonDown(1))
            {
                EnterADS();
            }
            if(Input.GetMouseButtonUp(1))
            {
                ExitADS();
            }


            GetComponent<Outline>().enabled = false;
            
            //call emptysound
            if (bulletsLeft == 0 && isShooting)
            {
                SoundManager.Instance.PlayReloadSound(thisWeaponModel);
            }



            if (currentShootingMode == ShootingMode.Auto)
            {
                //giữ chuột trái
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
            {
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            //nhập code nạp đạn
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloaing == false && Weapon_Manager.Instance.checkAmmoLeftFor(thisWeaponModel) > 0)
            {
                Reload();
            }
            //automatic reloading
            if (readyToShoot && isShooting == false && isReloaing == false && bulletsLeft <= 0)
            {
                Reload();
            }
            //điều kiện để bắn
            if (readyToShoot && isShooting && bulletsLeft > 0)
            {
                BurstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }

        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }


    }

    private void EnterADS()
    {
        animator.SetTrigger("enterADS");
        isADS = true;
        HUDManager.Instance.middleDot.SetActive(false);
        spreadIntensity = adsSpreadInstensity;
    }

    private void ExitADS()
    {
        animator.SetTrigger("exitADS");
        isADS = false;
        HUDManager.Instance.middleDot.SetActive(true);
        spreadIntensity = hipSpreadInstensity;
    }
    private void FireWeapon()
    {

        //giảm đạn
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        if(isADS)
        {
            animator.SetTrigger("RECOIL_ADS");
        }else
        {
            animator.SetTrigger("RECOIL");
        }

        //sound

        SoundManager.Instance.PlayShootingSound(thisWeaponModel);
        readyToShoot = false;
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        //khởi tạo viên đạn
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position,Quaternion.identity);

        //set damge
        Bullet bul = bullet.GetComponent<Bullet>();
        bul.bulletDamage = weaponDamage;

        //hướng viên đạn vào hướng bắn
        bullet.transform.forward = shootingDirection;

        //bắn đạn
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized *bulletVecocity);

        //destroy 
        StartCoroutine(DestroyBulletAfterTime(bullet,bulletPrefabLifeTime));
        
        //check if we are done shooting
        if(allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        //Burst Mode
        if(currentShootingMode == ShootingMode.Burst && BurstBulletsLeft > 1)//cta đã bắn 1 lần trc khi check
        {
            BurstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }    
    }
    

    private void Reload()
    {
        //call reload sound
        SoundManager.Instance.PlayReloadSound(thisWeaponModel);

        animator.SetTrigger("RELOAD");
        readyToShoot = false;
        isReloaing = true;
        //:V
         
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        isReloaing = false;
        int ammoAvailable = Weapon_Manager.Instance.checkAmmoLeftFor(thisWeaponModel);

        if (ammoAvailable >= magazineSize)
        {
            bulletsLeft = magazineSize;
            Weapon_Manager.Instance.DecreaseTotalAmmo(magazineSize, thisWeaponModel);
        }
        else if (ammoAvailable > 0)
        {
            bulletsLeft = ammoAvailable;
            Weapon_Manager.Instance.DecreaseTotalAmmo(magazineSize, thisWeaponModel);
        }

        readyToShoot = true;

    } 

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()//bản chất là raycasting 
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit)) 
        {
            //hitting something
            targetPoint = hit.point;
        }
        else
        {
            //shooting at the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(0, y, z);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}

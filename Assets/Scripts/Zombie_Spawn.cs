using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zombie_Spawn : MonoBehaviour
{
    public int initialZombie_Per_Wave = 5;
    public int current_zombie_per_wave;

    public float spawn_Delay = 0.5f;

    //delay zombie
    public int current_Wave = 0;
    public float wave_Cool_down =  10.0f;

    //delay between wave
    public bool inCoolDown;
    public float cool_down_Counter = 0;

    public List<KhaBanh> Current_Zombie_Alive;

    public GameObject KhaBanhPrefab;

    //text mesh
    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI cool_Down_UI;
    public TextMeshProUGUI current_wave_ui;

    private void Start()
    {
        current_zombie_per_wave = initialZombie_Per_Wave;

        //Khởi tạo wave mới
        StartNextWave();
    }

    private  void StartNextWave()
    {
        Current_Zombie_Alive.Clear();

        current_Wave++;

        GlobalReference.Instance.WaveNumber = current_Wave;

        current_wave_ui.text = "Wave: " + current_Wave.ToString();

        StartCoroutine(SpawmWave());
    }

    private IEnumerator SpawmWave()
    {
        for (int i = 0; i < current_zombie_per_wave; i++)
        {
            Vector3 spawmOffset = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f));
            Vector3 SpawmPos = transform.position + spawmOffset;

            //instantiate zombie
            var zombie = Instantiate(KhaBanhPrefab, SpawmPos, Quaternion.identity);

            KhaBanh KhabanhScript = zombie.GetComponent<KhaBanh>();

            Current_Zombie_Alive.Add(KhabanhScript);

            yield return new WaitForSeconds(spawn_Delay);
        }
    }

    private void Update()
    {
        List<KhaBanh> khabanh_to_remove = new List<KhaBanh>();

        foreach (KhaBanh khabanh in Current_Zombie_Alive)
        {
            if (khabanh.isDead)
            {
                khabanh_to_remove.Add(khabanh);
            }
        }
        foreach (KhaBanh khaBanh in khabanh_to_remove)
        {
            Current_Zombie_Alive.Remove(khaBanh);
        }
        khabanh_to_remove.Clear();

        if(Current_Zombie_Alive.Count == 0 && inCoolDown == false)
        {
            StartCoroutine(Wave_Cool_down());
        }

        //chạy đếm ngược
        if(inCoolDown)
        {
            cool_down_Counter -= Time.deltaTime;

        }else
        {
            cool_down_Counter = wave_Cool_down;
        }

        cool_Down_UI.text = cool_down_Counter.ToString("F0");

    }

    private IEnumerator Wave_Cool_down()
    {
        inCoolDown = true;

        waveOverUI.gameObject.SetActive(true);


        yield return new WaitForSeconds(wave_Cool_down);

        inCoolDown = false;
        waveOverUI.gameObject.SetActive(false);

        current_zombie_per_wave = (int)(current_zombie_per_wave * 1.2);

        StartNextWave();
    }
}

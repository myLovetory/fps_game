using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReference : MonoBehaviour
{
    public static GlobalReference Instance { get; set; }

    public GameObject bulletImpactEffectPrefab;

    public GameObject blood_Spray_Effect;

    public int WaveNumber;
    private void Awake()
    { 
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}

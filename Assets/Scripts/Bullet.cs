using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;

    private void OnCollisionEnter(Collision objectWeHit)
    {
        if(objectWeHit.gameObject.CompareTag("Target"))
        {
            print("hit " + objectWeHit.gameObject.name + " !");

            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
        if(objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall");

            CreateBulletImpactEffect(objectWeHit);

            Destroy(gameObject);
        }
        if (objectWeHit.gameObject.CompareTag("KhaBanh"))
        {
            //check chua chet moi nhan damge
            if (objectWeHit.gameObject.GetComponent<KhaBanh>().isDead == false)
            {
                objectWeHit.gameObject.GetComponent<KhaBanh>().TakeDamage(bulletDamage);
                Creat_Blood_Spray_Effect(objectWeHit);
            }
        
            Destroy(gameObject);
        }
    }

    private void Creat_Blood_Spray_Effect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        //chuyển vào prefabs
        GameObject blood_Spray_Prefab = Instantiate(
            GlobalReference.Instance.blood_Spray_Effect,
            contact.point,
            Quaternion.LookRotation(contact.normal)

        );

        blood_Spray_Prefab.transform.SetParent(objectWeHit.gameObject.transform);
    }

    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        //chuyển vào prefabs
        GameObject hole = Instantiate(
            GlobalReference.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)

        ) ;

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }    
}

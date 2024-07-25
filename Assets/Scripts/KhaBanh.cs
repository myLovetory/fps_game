using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KhaBanh : MonoBehaviour
{
    [SerializeField] private int Hp = 1000;
    private Animator animator;

    private NavMeshAgent navAgent;
    private float destroyDelay = 8f;


    // Thời gian chờ trước khi xác zombie biến mất (3 giây)
    public bool isDead;
    void Start()
    {
        animator = GetComponent<Animator>(); 
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void TakeDamage(int damageAmount)
    {

        if (Hp <= 0) // If already dead, don't take further damage
            return;


        Hp -= damageAmount;

        if(Hp <=0)
        {
            //:>> chết cũng cần random đó
            int randomValue = Random.Range(0, 2);
            if(randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }else
            {
                animator.SetTrigger("DIE2");
            }
            isDead = true;

            //dead
            SoundManager.Instance.zombieChanels2.PlayOneShot(SoundManager.Instance.zombieDeath);
            //SoundManager.Instance.zombieChanels.enabled = false;    
            StartCoroutine(DestroyAfterDelay(destroyDelay));
        }
        else
        {
            animator.SetTrigger("DAMAGE");

            //hurt 
            SoundManager.Instance.zombieChanels2.PlayOneShot(SoundManager.Instance.zombieHurt);
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(transform.root.gameObject); // Hủy GameObject chứa script này
    }

    //mother fucker gizmos to debug
    private void OnDrawGizmos()
    {
        //attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 11f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 15f);
    }


}

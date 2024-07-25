using System.Collections;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Time before destruction
    public float time_For_Destruction;

    void Start()
    {
        StartCoroutine(DestroySelf(time_For_Destruction));
    }

    private IEnumerator DestroySelf(float time_For_Destruction)
    {
        yield return new WaitForSeconds(time_For_Destruction);

        Destroy(gameObject);
    }
}

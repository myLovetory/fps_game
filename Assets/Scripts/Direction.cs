using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    // Start is called before the first frame update
     public void Back_to_main_menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main menu");
    }

}

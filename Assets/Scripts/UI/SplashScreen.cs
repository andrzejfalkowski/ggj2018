using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
	private void Start ()
    {
        Invoke("Launch", 2);
	}

    public void Launch()
    {
        SceneManager.LoadScene("Test");
    }
}

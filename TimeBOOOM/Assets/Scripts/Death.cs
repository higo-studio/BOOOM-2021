using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    bool ready = false;

    void Update()
    {
        if (ready && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }

    public void ready2Restart()
    {
        ready = true;
    }
}

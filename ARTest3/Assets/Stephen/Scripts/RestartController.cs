using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour
{
    public float timePassed;
    // Update is called once per frame
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    void FixedUpdate()
    {
        if(!Input.anyKey)
        {
            timePassed = timePassed + 1;
        }
        else
        {
            timePassed = 0;
        }

        if (timePassed >= 1000)
        {
            timePassed = 0;
            SceneManager.LoadScene("TitleScreen");
        }
    }
}

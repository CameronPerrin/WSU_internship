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
            timePassed += Time.deltaTime;
        }
        else
        {
            timePassed = 0;
        }

        if (timePassed >= 300)// 300seconds = 5mins
        {
            timePassed = 0;
            SceneManager.LoadScene("TitleScreen");
        }
    }
}

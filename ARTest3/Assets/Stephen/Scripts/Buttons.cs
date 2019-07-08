using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public enum ButtonType { MainMenuButton, InGameButton };
    public ButtonType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(type == ButtonType.MainMenuButton)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (type == ButtonType.InGameButton)
        {

        }
    }

    private void OnMouseDown()
    {
        if(gameObject.name == "Start")
        {
            PuzzleManager.game_status.status = GameStatus.GameState.start_pressed;
            Destroy(gameObject);
        }
    }
}

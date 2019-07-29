using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickSpriteChange : MonoBehaviour
{
    private string spriteNames = "SpinningSpritesheet";
    private int spriteVersion = 0;
    private SpriteRenderer spriteR;
    private Sprite[] sprites;

    public int winningPosition;
    public int Successes = 0;
    [SerializeField] private int Position = 0;

    public AudioClip bell;
    public AudioClip victory;
    public AudioSource playerSource;
    private bool played = false;


    [SerializeField] private KeyCode Button1;
    [SerializeField] private KeyCode Button2;
    [SerializeField] private KeyCode Button3;
    [SerializeField] private KeyCode Button4;
    [SerializeField] private KeyCode Button5;
    [SerializeField] private KeyCode Button6;
    [SerializeField] private KeyCode Button7;
    [SerializeField] private KeyCode Button8;
    [SerializeField] private KeyCode Button9;
    [SerializeField] private KeyCode Button10;
    [SerializeField] private KeyCode Button11;
    [SerializeField] private KeyCode Button12;
    [SerializeField] private KeyCode Button13;
    [SerializeField] private KeyCode Button14;
    [SerializeField] private KeyCode Button15;
    [SerializeField] private KeyCode Button16;
    [SerializeField] private KeyCode check;

    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNames);
        winningPosition = Random.Range(0, 15);
    }

    private void Update()
    {

       if (Position == winningPosition && played == false)
        {
            StartCoroutine(PlaySound());
        }
        if (Input.GetKeyDown(check) && Position == winningPosition)
        {
            Successes += 1;
            winningPosition = Random.Range(0, 15);
            StartCoroutine(PlaySuccess());
        }

        if (Successes == 3)
        {
            //victory load other scene
        }


        if (Input.GetKeyDown(Button1))
        {
            spriteR.sprite = spriteR.sprite = sprites[0];
            Position = 0;
            played = false;
        }
        if (Input.GetKeyDown(Button2))
        {
            spriteR.sprite = spriteR.sprite = sprites[1];
            Position = 1;
            played = false;
        }
        if (Input.GetKeyDown(Button3))
        {
            spriteR.sprite = spriteR.sprite = sprites[2];
            Position = 2;
            played = false;
        }
        if (Input.GetKeyDown(Button4))
        {
            spriteR.sprite = spriteR.sprite = sprites[3];
            Position = 3;
            played = false;
        }
        if (Input.GetKeyDown(Button5))
        {
            spriteR.sprite = spriteR.sprite = sprites[4];
            Position = 4;
            played = false;
        }
        if (Input.GetKeyDown(Button6))
        {
            spriteR.sprite = spriteR.sprite = sprites[5];
            Position = 5;
            played = false;
        }
        if (Input.GetKeyDown(Button7))
        {
            spriteR.sprite = spriteR.sprite = sprites[6];
            Position = 6;
            played = false;
        }
        if (Input.GetKeyDown(Button8))
        {
            spriteR.sprite = spriteR.sprite = sprites[7];
            Position = 7;
            played = false;
        }
        if (Input.GetKeyDown(Button9))
        {
            spriteR.sprite = spriteR.sprite = sprites[8];
            Position = 8;
            played = false;
        }
        if (Input.GetKeyDown(Button10))
        {
            spriteR.sprite = spriteR.sprite = sprites[9];
            Position = 9;
            played = false;
        }
        if (Input.GetKeyDown(Button11))
        {
            spriteR.sprite = spriteR.sprite = sprites[10];
            Position = 10;
            played = false;
        }
        if (Input.GetKeyDown(Button12))
        {
            spriteR.sprite = spriteR.sprite = sprites[11];
            Position = 11;
            played = false;
        }
        if (Input.GetKeyDown(Button13))
        {
            spriteR.sprite = spriteR.sprite = sprites[12];
            Position = 12;
            played = false;
        }
        if (Input.GetKeyDown(Button14))
        {
            spriteR.sprite = spriteR.sprite = sprites[13];
            Position = 13;
            played = false;
        }
        if (Input.GetKeyDown(Button15))
        {
            spriteR.sprite = spriteR.sprite = sprites[14];
            Position = 14;
            played = false;
        }
        if (Input.GetKeyDown(Button16))
        {
            spriteR.sprite = spriteR.sprite = sprites[15];
            Position = 15;
            played = false;
        }

    }
    

    IEnumerator PlaySound()
    {

        playerSource.PlayOneShot(bell);
        played = true;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator PlaySuccess()
    {

        playerSource.PlayOneShot(victory);
        yield return new WaitForEndOfFrame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewJoystick : MonoBehaviour
{
    

    public int winningPosition;
    public int Successes = 0;
    [SerializeField] public float PositionAngle;

    public AudioClip bell;
    public AudioClip victory;
    public AudioSource playerSource;
    private bool played = false;

    public float x;
    public float y;
    [SerializeField] public GameObject joystick;
    [SerializeField] private Vector3 rotate;

    

    //these will target objects that have the rings of leds.
    [SerializeField] public GameObject success0;
    [SerializeField] public GameObject success1;
    [SerializeField] public GameObject success2;

    [SerializeField] public GameObject Camera;

    [SerializeField] public GameObject ThisGameWindow;

    //Variable for new game check
    public bool newGame;

    //Holder for item description text
    public GameObject descText;


    void Start()
    {
        GenWinning();
    }

    private void Awake()
    {
        //Camera.GetComponent<ScannerMove>().enabled = false;
    }

    private void Update()
    {
        Leds();
        //Check for new game to reset the game for each object
        if(newGame == true)
        {
            descText.SetActive(false);
            GenWinning();
            newGame = false;
        }

        //joystick input
        y = Input.GetAxisRaw("PS X");
        x = Input.GetAxisRaw("PS Y");

        //Gets the angle of the joystick.  Generates rotate
        CurrentPosition();

        //rotate image
        joystick.transform.eulerAngles = rotate; 

        //if the angle is within range of the winning angle plays notification sound
        if (Mathf.Abs(PositionAngle - winningPosition) < 3 && played == false)
        {
            StartCoroutine(PlaySound());
        }

        //check to see if on the winning angle
        if (Input.GetButtonDown("Submit") && PositionAngle == winningPosition)
        {
            Successes += 1;
            GenWinning();
            StartCoroutine(PlaySuccess());
        }

        //check if you have won 3 times
        if (Successes == 3)
        {

            playerSource.PlayOneShot(victory);
            Successes = 0;
            //Camera.GetComponent<ScannerMove>().enabled = false;
            ThisGameWindow.SetActive(false);
            //Reveals item information after winning game
            descText.SetActive(true);
            newGame = true;
        }
    }

    void CurrentPosition()
    {
        //Depending on how many wins there are this will change the snapping to deviding the 360 degrees
        switch (Successes)
        {
            case 0:
                if (x == 0 && y == 0)
                {
                    rotate = new Vector3(0, 0, 0);
                    PositionAngle = 1;
                }
                else if (y < 0)
                {
                    rotate = new Vector3(0, 0, (PositionAngle - 1) * 45);
                    PositionAngle = Mathf.Round((Mathf.Rad2Deg * Mathf.Atan(-x / -y) + 270) / 45f);
                }
                else
                {
                    rotate = new Vector3(0, 0, (PositionAngle - 1) * 45);
                    PositionAngle = Mathf.Round((Mathf.Rad2Deg * Mathf.Atan(-x / -y) + 90) / 45f);
                }
                
                break;



            case 1:
                if (x == 0 && y == 0)
                {
                    rotate = new Vector3(0, 0, 0);
                    PositionAngle = 1;
                }
                else if (y < 0)
                {
                    rotate = new Vector3(0, 0, (PositionAngle - 1) * 22.5f);
                    PositionAngle = Mathf.Round((Mathf.Rad2Deg * Mathf.Atan(-x / -y) + 270) / 22.5f);
                }
                else
                {
                    rotate = new Vector3(0, 0, (PositionAngle - 1) * 22.5f);
                    PositionAngle = Mathf.Round((Mathf.Rad2Deg * Mathf.Atan(-x / -y) + 90) / 22.5f);
                }

                break;

            case 2:
                if (x == 0 && y == 0)
                {
                    rotate = new Vector3(0, 0, 0);
                    PositionAngle = 1;
                }
                else if (y < 0)
                {
                    rotate = new Vector3(0, 0, (PositionAngle - 1) * 15f);
                    PositionAngle = Mathf.Round((Mathf.Rad2Deg * Mathf.Atan(-x / -y) + 270) / 15f);
                }
                else
                {
                    rotate = new Vector3(0, 0, (PositionAngle - 1) * 15f);
                    PositionAngle = Mathf.Round((Mathf.Rad2Deg * Mathf.Atan(-x / -y) + 90) / 15f);
                }

                break;

            default:
                break;
        }

    }

    void GenWinning ()
    {
        switch (Successes)
        {
            case 0:
                winningPosition = Random.Range(1, 8);
                break;

            case 1:
                winningPosition = Random.Range(1, 16);
                break;

            case 2:
                winningPosition = Random.Range(1, 24);
                break;

            default:
                break;
        }
    }

    void Leds()
    {
        switch (Successes)
        {
            case 0:
                success0.SetActive(true);
                success1.SetActive(false);
                success2.SetActive(false);
                break;

            case 1:
                success0.SetActive(false);
                success1.SetActive(true);
                success2.SetActive(false);
                break;

            case 2:
                success0.SetActive(false);
                success1.SetActive(false);
                success2.SetActive(true);
                break;

            default:
                break;
        }

    }

    IEnumerator PlaySound()
    {
        playerSource.volume =  1/(Mathf.Abs(PositionAngle - winningPosition));

        playerSource.PlayOneShot(bell);
        played = true;
        playerSource.volume = 1;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator PlaySuccess()
    {

        playerSource.PlayOneShot(victory);
        yield return new WaitForEndOfFrame();
    }
}



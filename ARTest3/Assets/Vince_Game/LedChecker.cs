using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LedChecker : MonoBehaviour
{

    [SerializeField] public GameObject[] leds;

    [SerializeField] public Sprite[] ledColors;

    [SerializeField] public GameObject joystick;

    public int LedPos;
    private int RandLedBreak;
    private Sprite abutton;




    // Start is called before the first frame update
    void Awake()
    {
        if (joystick.GetComponent<NewJoystick>().Successes == 1)
        {
            RandLedBreak = Random.Range(0, 15);
            leds[RandLedBreak].GetComponent<Image>().sprite = ledColors[3];
            Debug.Log("Led broken" + RandLedBreak);

        }

        if (joystick.GetComponent<NewJoystick>().Successes == 2)
        {
            int RandLedBreak = Random.Range(0, 11);
            leds[RandLedBreak].GetComponent<Image>().sprite = ledColors[3];
            Debug.Log("Led broken" + RandLedBreak);

            RandLedBreak = Random.Range(12, 23);
            leds[RandLedBreak].GetComponent<Image>().sprite = ledColors[3];
            Debug.Log("Led broken" + RandLedBreak);

        }

    }

    // Update is called once per frame
    void Update()
    {
        //Stephen: Added + 1 to fix orientation issues
        LedPos = Mathf.FloorToInt(joystick.GetComponent<NewJoystick>().PositionAngle) + 1;

        foreach (GameObject led in leds)
        {
            if (led.GetComponent<Image>().sprite == ledColors[1])
            {
                led.transform.localScale = new Vector3(0.013f, 0.013f, 0.013f);
            }
            else
            {
                led.transform.localScale = new Vector3(0.009301168f, 0.009301168f, 0.009301168f);
            }
        }

        //If the joystick is pointing at the winning position and the led is not broken change sprite to Green.
        if (joystick.GetComponent<NewJoystick>().PositionAngle == joystick.GetComponent<NewJoystick>().winningPosition && !leds[LedPos - 1].GetComponent<Image>().sprite != ledColors[3] )
        {
            leds[LedPos - 1].GetComponent<Image>().sprite = ledColors[1];
        }

        //if the joystick is not pointing at the winning position and the led is not broken change the sprite to red.
        if (joystick.GetComponent<NewJoystick>().PositionAngle != joystick.GetComponent<NewJoystick>().winningPosition)
        {
            leds[LedPos - 1].GetComponent<Image>().sprite = ledColors[2];
        }

        //if the joystick is not pointing at the led change its sprite to off.
        foreach (GameObject deactivate in leds)
        {
            if (leds[LedPos - 1] != deactivate && deactivate.GetComponent<Image>().sprite != ledColors[3])
            {
                deactivate.GetComponent<Image>().sprite = ledColors[0];
            }
            
        }
    }
}

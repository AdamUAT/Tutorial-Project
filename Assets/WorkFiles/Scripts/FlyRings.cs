using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyRings : MonoBehaviour
{
    [Tooltip("0: Nothing, 1: Start flying, 2: Stop flying")]
    public int isStart;

    [Tooltip("0: Not flown through, 1: Next ring to fly through, 2: Flown through")]
    public int state;

    [SerializeField]
    [Tooltip("The next ring the player should fly through")]
    private FlyRings nextRing;

    [SerializeField]
    [Tooltip("The time increased by flying through this ring, in seconds")]
    private int timerIncrease = 5;

    [SerializeField]
    [Tooltip("The time between when the indicator changes colors in seconds")]
    private float indicatorDelay = 2.5f;

    [SerializeField]
    [Tooltip("The color of the indicator when the ring has been flown through")]
    private Material lightOn;

    [SerializeField]
    [Tooltip("The color of the indicator when the ring has not been flown through")]
    private Material lightOff;

    [SerializeField]
    [Tooltip("A reference to the indicator mesh")]
    private MeshRenderer indicator;

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();
        if (player != null && state == 1) //Only triggers if this ring is supposed to be the next one to fly through.
        {
            switch(isStart)
            {
                case 0:
                    //Tells the next ring to start flashing its lights and start colliding.
                    nextRing.state = 1;
                    //Tells this ring to have a solid color and stop colliding.
                    state = 2;
                    indicator.material = lightOn;
                    //Increases the time on the timer
                    GameManager.instance.AddTime(timerIncrease);
                    break;
                case 1:
                    GameManager.instance.EnableTimer();
                    GameManager.instance.ShowAllRings();
                    GameManager.instance.AddTime(timerIncrease);
                    state = 2;
                    indicator.material = lightOn;
                    nextRing.state = 1;
                    //Starts the player flying
                    player.isFlying = true;
                    break;
                case 2:
                    GameManager.instance.DisableTimer();
                    GameManager.instance.HideAllRings(new int[] {2});
                    state = 2;
                    indicator.material = lightOn;
                    //Stops the player flying
                    player.isFlying = false;
                    break;
                default:
                    break;
            }
        }
    }

    public void LightOff()
    {
        indicator.material = lightOff;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 1)
        {
            switch(Mathf.Lerp(0, 1, Mathf.Round(Mathf.Sin(Time.time / indicatorDelay) * Mathf.Sin(Time.time / indicatorDelay))))
            {
                case 0:
                    indicator.material = lightOff;
                    break;
                case 1:
                    indicator.material = lightOn;
                    break;
                default:
                    Debug.LogError("Error with sin in material lerp for FlyRing lights");
                    break;
            }
        }
    }
}

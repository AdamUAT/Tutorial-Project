using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using StarterAssets;
using UnityEditor.Experimental.Rendering;

public class GameManager : MonoBehaviour
{

    #region Singleton
    //This is a singleton, it makes sure we always have a CollectableObjectMaster.
    private static GameManager GameManagerInstance = null;

    public static GameManager instance
    {
        get
        {
            if (GameManagerInstance == null)
            {
                GameManagerInstance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            if (GameManagerInstance == null)//If we didn't find a CollectableObjectMaster, make one
            {
                GameObject newObj = new GameObject("GameManager");
                GameManagerInstance = newObj.AddComponent(typeof(GameManager)) as GameManager;
                Debug.Log("Could not find CollectableObjectMaster, so I made one");
            }

            return GameManagerInstance;
        }
    }
    #endregion

    public GameObject player;

    private int collectablesCount;//How many collectables we have

    [SerializeField]
    private TextMeshProUGUI collectablesDisplay;
    [SerializeField]
    private TextMeshProUGUI timerDisplay;
    private float timer;
    private GameObject[] rings;

    public void AddCollectable()
    {
        collectablesCount++;
        collectablesDisplay.text = collectablesCount.ToString();
    }


    #region FlyRings

    /// <summary>
    /// Unenables every ring except the start.
    /// </summary>
    public void HideAllRings()
    {
        foreach (GameObject ring in rings)
        {
            ring.SetActive(false);
        }
    }

    /// <summary>
    /// Unenables every ring except the types specified.
    /// </summary>
    /// <param name="shouldIgnore">An array that contains the ring types it should ignore.</param>
    public void HideAllRings(int[] shouldIgnore)
    {
        foreach (GameObject ring in rings)
        {
            FlyRings flyRing = ring.GetComponent<FlyRings>();
            if (flyRing != null)
            {
                if (!shouldIgnore.Contains<int>(flyRing.isStart))
                {
                    ring.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Enables every ring except the start.
    /// </summary>
    public void ShowAllRings()
    {
        foreach (GameObject ring in rings)
        {
            ring.SetActive(true);
        }
    }

    /// <summary>
    /// Enables every ring except the types specified.
    /// </summary>
    /// <param name="shouldIgnore">An array that contains the ring types it should ignore.</param>
    public void ShowAllRings(int[] shouldIgnore)
    {
        foreach (GameObject ring in rings)
        {
            FlyRings flyRing = ring.GetComponent<FlyRings>();
            if (flyRing != null)
            {
                if (!shouldIgnore.Contains<int>(flyRing.isStart))
                {
                    ring.SetActive(true);
                }
            }
        }
    }

    public void EnableTimer()
    {
        timerDisplay.gameObject.SetActive(true);
    }
    public void DisableTimer()
    {
        timerDisplay.gameObject.SetActive(false);
        timerDisplay.text = "";
    }
    public void SetTimer(string time)
    {
        timerDisplay.text = time;
    }

    public void AddTime(int seconds)
    {
        timer += seconds;
    }

    /// <summary>
    /// Resets the fly course
    /// </summary>
    private void TimeOut()
    {
        //Hides all the rings except for the first ring and sets the first ring to blink.
        foreach(GameObject ring in rings)
        {
            FlyRings flyRing = ring.GetComponent<FlyRings>();
            if (flyRing != null)
            {
                if (flyRing.isStart != 1)
                {
                    ring.SetActive(false);
                    flyRing.state = 0;
                    flyRing.LightOff();
                }
                else
                {
                    ring.SetActive(true);
                    flyRing.state = 1;
                }
            }
        }

        timerDisplay.text = "";
        timerDisplay.gameObject.SetActive(false);
        player.GetComponent<FirstPersonController>().isFlying = false;
    }
    #endregion FlyRings

    // Start is called before the first frame update
    void Start()
    {
        collectablesCount = 0;

        //Caches all rings in the scene and then sets them to not active, except the first ring.
        rings = GameObject.FindGameObjectsWithTag("FlyRing");
        HideAllRings(new int[] {1});
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {                    
            //Truncates the decimal so the timer counts from 10-1.
            SetTimer(Mathf.CeilToInt(timer).ToString());
            timer -= Time.deltaTime;
        }
        else if(timer <= 0 && timerDisplay.gameObject.activeInHierarchy == true ) //The second check ensures it the timer runs out only happens once.
        {
            TimeOut();
        }
    }
}

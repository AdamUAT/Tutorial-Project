using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRings : MonoBehaviour
{
    [SerializeField]
    [Tooltip("0: Nothing, 1: Start flying, 2: Stop flying")]
    private int isStart;

    [SerializeField]
    private List<FlyRings> flyRings;

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();
        if (player != null)
        {
            if (isStart == 2)
            {
                player.isFlying = false;
                foreach (FlyRings ring in flyRings)
                {
                    ring.gameObject.SetActive(false);
                }
            }
            else if (isStart == 1)
            {
                player.isFlying = true;
                foreach(FlyRings ring in flyRings)
                {
                    ring.gameObject.SetActive(true);
                }
            }
            else
            {

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

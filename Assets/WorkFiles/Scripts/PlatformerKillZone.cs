using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerKillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();
        if (player != null && GameManager.instance.currentRespawnPoint == 2) //Only triggers if the current checkpoint is the parkour checkpoint.
        {
            GameManager.instance.RespawnPlayer();
        }

    }
}

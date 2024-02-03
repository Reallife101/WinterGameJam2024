using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHeartDisplay : MonoBehaviour
{
    [SerializeField] List<GameObject> hearts;

    int currentHeartCount;

    private void Start()
    {
        currentHeartCount = hearts.Count;
    }

    private void OnEnable()
    {
        playerHealth.PlayerHitEvent += HandlePlayerHitEvent;
        playerHealth.PlayerHealEvent += HandlePlayerHealEvent;
    }

    private void OnDisable()
    {
        playerHealth.PlayerHitEvent -= HandlePlayerHitEvent;
        playerHealth.PlayerHealEvent -= HandlePlayerHealEvent;
    }

    private void HandlePlayerHitEvent()
    {
        currentHeartCount -= 1;

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i>=currentHeartCount)
            {
                hearts[i].SetActive(false);
            }
        }
    }
    
    private void HandlePlayerHealEvent()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHeartCount)
            {
                hearts[i].SetActive(true);
            }
        }
    }


}

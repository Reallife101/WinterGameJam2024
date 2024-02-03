using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pointManager : MonoBehaviour
{
    public static pointManager PM_Instance;
    public int totalPoints;
    [SerializeField] TMP_Text score;

    private void Awake()
    {
        if(PM_Instance != null)
        {
            Destroy(gameObject);
        }
        PM_Instance = this;
    }

    private void Start()
    {
        totalPoints = 0;
    }

    public void GainPoint(int points)
    {
        int pointsGained = (int)(points * comboManager.CM_Instance.currentMultiplier);
        totalPoints += pointsGained;
        comboManager.CM_Instance.GainComboPoints(pointsGained);
        score.text = "Points: " + totalPoints;
    }
}

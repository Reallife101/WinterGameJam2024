using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    [SerializeField] private TextMeshProUGUI healthIndicator;
    [SerializeField] private string bossName;
    [SerializeField] private Boss boss;

    protected override void Start()
    {
        base.Start();
        setHealthMeter();
    }

    private void setHealthMeter()
    {
        healthIndicator.text = bossName + "\n" + getHealth() + " / " + getMaxHealth();
    }

    public override void TakeDamage(int i = 1)
    {
        base.TakeDamage(i);
        setHealthMeter();
        if (getHealth() < getMaxHealth() / 2 && boss.GetPhase() == 0)
        {
            boss.SetPhase2();
            healthIndicator.color = Color.red;
        }
    }
}

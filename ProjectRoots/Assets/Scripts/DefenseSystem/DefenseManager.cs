using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseManager : MonoBehaviour
{
    private int _baseDefenseMaxLevel = 1;
    private int _heavyDefenseMaxLevel = 1;
    private int _longhsotDefenseMaxLevel = 1;

    public void IncreaseDefensesDamageBy(float percentage)
    {

    }

    public void EnableBaseDefenseUpgrade()
    {
        _baseDefenseMaxLevel++;
    }
    public void EnableHeavyDefenseUpgrade()
    {
        _heavyDefenseMaxLevel++;
    }
    public void EnableLognshotDefenseUpgrade()
    {
        _longhsotDefenseMaxLevel++;
    }
}

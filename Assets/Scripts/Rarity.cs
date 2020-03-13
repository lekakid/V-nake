using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rarity
{
    public enum RarityEnum { COMMON, RARE, UNIQUE, LEGENDARY }

    public RarityEnum Grade;
    public float Rate;
}
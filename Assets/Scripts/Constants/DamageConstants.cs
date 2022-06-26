using UnityEngine;
using System.Collections;

public class DamageConstants
{
    public static readonly float Normal_Damage = 5f;
    public static readonly float Normal_Frecuency = 0.2f;
    public static readonly float Easy_Damage = 0.5f;
    public static readonly float Easy_Frecuency = 1f;
    public static float Damage = Normal_Damage;
    public static float Frecuency = Normal_Frecuency;
    public static bool IsInEasyMode() {
        return Damage == Easy_Damage;
    }
}

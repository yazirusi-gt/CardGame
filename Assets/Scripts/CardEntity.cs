using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードデータそのもの
/// </summary>
[CreateAssetMenu(fileName = "CardEntity", menuName = "Create CardEntity")]
public class CardEntity : ScriptableObject
{
    public new string name;
    public int hp;
    public int at;
    public int cost;
    public ABILITY ability;
    public Sprite icon;

    public enum ABILITY
    {
        None,
        INIT_ATTACKABLE,
        SHIELD,
    }
}

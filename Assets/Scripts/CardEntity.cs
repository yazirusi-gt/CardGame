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
    public SPELL spell;
    public Sprite icon;

    public enum ABILITY
    {
        None,
        INIT_ATTACKABLE,
        SHIELD,
    }

    public enum SPELL
    {
        NONE,
        DAMAGE_ENEMY_CARD,
        DAMAGE_ENEMY_CARDS,
        DAMAGE_ENEMY_HERO,
        HEAL_FRIEND_CARD,
        HEAL_FRIEND_CARDS,
        HEAL_FRIEND_HERO,
    }
}

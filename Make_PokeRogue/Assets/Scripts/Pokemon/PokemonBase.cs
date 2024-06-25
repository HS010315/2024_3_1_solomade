using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]
public class PokemonBase : ScriptableObject
{
    public string name;
    [SerializeField] PokemonType type;
    [SerializeField] Sprite sprite;  

    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableSkill> learnableSkills;

    public string Name
    {
        get { return name; }
    }

    public Sprite Sprite
    {
        get { return sprite; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableSkill> LearnableSkills
    {
        get { return learnableSkills; }
    }
}

[System.Serializable]
public class LearnableSkill
{
    [SerializeField] SkillBase skillBase;
    [SerializeField] int level;

    public SkillBase Base
    {
        get { return skillBase; }
    }

    public int Level
    {
        get { return level; }
    }
}
public enum PokemonType
{
    None,
    Triangle,
    Square,
    Circle
}
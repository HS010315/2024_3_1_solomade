using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public PokemonBase Base { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public List<Skill> Skills { get; set; }
    public int Experience { get; set; } 
    public int MaxExperience { get; set; }

    public Pokemon(PokemonBase pBase, int pLevel)
    {
        Initialize(pBase, pLevel);
    }

    public void Initialize(PokemonBase pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        HP = MaxHP;
        Experience = 0;
        MaxExperience = CalculateMaxExperience(Level);

        Skills = new List<Skill>();
        foreach (var skill in Base.LearnableSkills)
        {
            if (skill.Level <= Level)
                Skills.Add(new Skill(skill.Base));
            Debug.Log($"{Base.Name}은 {skill.Base.Name}을 독학으로 배웠다..");
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = Base.Sprite;
        }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    }
    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; }
    }
    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5; }
    }
    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
    }
    public int MaxHP
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; }
    }

    private int CalculateMaxExperience(int level)
    {
        return Mathf.FloorToInt(Mathf.Pow(level, 3));
    }

    public void GainExperience(int amount)
    {
        Experience += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (Experience >= MaxExperience)
        {
            Experience -= MaxExperience;
            Level++;
            MaxExperience = CalculateMaxExperience(Level);
            HP = MaxHP;
            Debug.Log($"{Base.Name} 의 레벨이 {Level}으로 올랐다!");
        }
    }
}
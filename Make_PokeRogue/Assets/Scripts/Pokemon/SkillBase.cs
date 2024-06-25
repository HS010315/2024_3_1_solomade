using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Pokemon/Create new Skill")]
public class SkillBase : ScriptableObject
{
    public string name;
    [SerializeField] PokemonType type;
    [SerializeField] int power;
    [SerializeField] int accuracy;

    public string Name
    {
        get { return name; }
    }
    public PokemonType Type
    {
        get { return type; }
    }
    public int Power
    {
        get { return power; }
    }
    public int Accuracy
    {
        get { return accuracy; }
    }
}

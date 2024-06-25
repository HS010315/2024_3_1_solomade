using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSystem : MonoBehaviour
{
    public PokemonBase[] enemyPokemonBases;
    public Transform playerPokemonPosition;
    public Transform enemyPokemonPosition;
    public GameObject player;

    private Pokemon playerPokemon;
    private Pokemon enemyPokemon;

    private bool isPlayerTurn;
    private int stage = 1;

    public PokemonBase playerPokemonBase; 
    public GameObject pokemonPrefab; 

    private void Start()
    {
        if (playerPokemonBase == null)
        {
            Debug.LogError("Player's Pokemon base is not assigned in the inspector!");
            return;
        }

        GameObject playerObj = Instantiate(pokemonPrefab, playerPokemonPosition);
        playerPokemon = playerObj.GetComponent<Pokemon>();
        playerPokemon.Initialize(playerPokemonBase, 5);

        StartBattle();
    }

    private void StartBattle()
    {
        SpawnEnemyPokemon();
        playerPokemon.HP = playerPokemon.MaxHP;
        Debug.Log($"{playerPokemon.name}�� ü���� ������ ȸ���Ǿ���.");
        isPlayerTurn = true;
    }

    private void SpawnEnemyPokemon()
    {
        int randomIndex = Random.Range(0, enemyPokemonBases.Length);
        PokemonBase enemyBase = enemyPokemonBases[randomIndex];

        int levelVariation = Random.Range(-3, 0);
        int enemyLevel = playerPokemon.Level + levelVariation;
        enemyLevel = Mathf.Max(enemyLevel, 1); 

        GameObject enemyObj = Instantiate(pokemonPrefab, enemyPokemonPosition);
        enemyPokemon = enemyObj.GetComponent<Pokemon>();
        enemyPokemon.Initialize(enemyBase, enemyLevel);

        // ��������Ʈ ����
        SpriteRenderer spriteRenderer = enemyObj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = enemyBase.Sprite;
        }

        Debug.Log($"�������� {stage}: �߻��� {enemyBase.Name} �� ��Ÿ����. {enemyLevel} �����̳�?");
    }

    private void Update()
    {
        if (isPlayerTurn)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && playerPokemon.Skills.Count > 0)
            {
                UseSkill(playerPokemon, enemyPokemon, playerPokemon.Skills[0]); 
                isPlayerTurn = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && playerPokemon.Skills.Count > 0)
            {
                UseSkill(playerPokemon, enemyPokemon, playerPokemon.Skills[1]); 
                isPlayerTurn = false;
            }
        }
        else
        {
            EnemyTurn();

            if (playerPokemon.HP == 0)
            {
                Debug.Log("��Ʋ���� �й��Ͽ���.");
                Destroy(player);
            }
            else if (enemyPokemon.HP == 0)
            {
                Debug.Log("��� ���ϸ��� ��������! ��Ʋ���� �¸��Ͽ���!");
                Destroy(enemyPokemon.gameObject);

                int expGained = enemyPokemon.Level * 10; 
                playerPokemon.GainExperience(expGained);

                stage++;
                Debug.Log($"����ġ ȹ��: {expGained}");
                Debug.Log($"������ ���� ���� ����ġ: {playerPokemon.Experience}/{playerPokemon.MaxExperience}");
                Debug.Log($"���� ����: {playerPokemon.Level}");
                StartBattle();
            }
            isPlayerTurn = true;
        }
    }

    private void UseSkill(Pokemon attacker, Pokemon defender, Skill skill)
    {
        int damage = Mathf.FloorToInt((2 * attacker.Level / 5 + 2) * skill.Base.Power * attacker.SpAttack / defender.Defense) / 50 + 2;
        defender.HP -= damage;

        if (defender.HP < 0) defender.HP = 0;

        Debug.Log($"{attacker.Base.Name} �� {skill.Base.Name} �� ����ߴ�! {defender.Base.Name}�� {damage} �� �������� �޾Ҵ�.");
        Debug.Log($"{defender.Base.Name}�� ü���� {defender.HP} ���Ҵ�!");
    }

    private void EnemyTurn()
    {
        if (enemyPokemon != null && enemyPokemon.Skills.Count > 0)
        {
            Skill randomSkill = enemyPokemon.Skills[Random.Range(0, enemyPokemon.Skills.Count)];
            UseSkill(enemyPokemon, playerPokemon, randomSkill);
        }
    }
}
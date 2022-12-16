using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lose
}


public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public Character playerPref;
    public Character enemyPref;

    public Transform playerBattlePosition;
    public Transform enemyBattlePosition;

    public int playerCount = 5;
    public int enemyCount = 5;

    public List<ActionBase> actionBases;

    public Button endTurnButton;
    public GameObject losePanel;
    public GameObject wonPanel;

    private List<Character> PlayerCharacters = new List<Character>();
    private List<Character> EnemyCharacters = new List<Character>();

    ActionBase activeAction;

    int playerIndex = 0;
    int enemyIndex = 0;

    private void Start()
    {
        state = BattleState.Start;
        SetupBattle();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            EndTurn();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void SetupBattle()
    {
        Character character;
        for (int i = 0; i < playerCount; i++)
        {
            character = Instantiate(playerPref, new Vector3((i * (playerBattlePosition.localScale.x / playerCount)) - playerBattlePosition.localScale.x / 2.5f, playerBattlePosition.position.y, playerBattlePosition.position.z), Quaternion.identity);
            character.OnDeath += (() => { EndBattle(BattleState.Lose); });
            PlayerCharacters.Add(character);
        }

        for (int i = 0; i < enemyCount; i++)
        {
            character = Instantiate(enemyPref, new Vector3(i * (enemyBattlePosition.localScale.x / enemyCount) - enemyBattlePosition.localScale.x / 2.5f, enemyBattlePosition.position.y, enemyBattlePosition.position.z), Quaternion.identity);
            character.OnDeath += (() => { EndBattle(BattleState.Won); }); ;
            EnemyCharacters.Add(character);
        }



        playerIndex = 0;
        enemyIndex = 0;


        PlayerTurn();
    }

    private void PlayerTurn()
    {
        endTurnButton.interactable = true;
        state = BattleState.PlayerTurn;
        int actionIndex = UnityEngine.Random.Range(0, actionBases.Count);
        Character player = PlayerCharacters[playerIndex];
        player.YourTurn();
        if (player.StillAlive)
        {
            ActionBase action = Instantiate(actionBases[actionIndex]);
            activeAction = action;
            activeAction.currentCharacter = player;
            activeAction.transform.position = new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z);


            playerIndex++;
        }
        else
        {
            EndBattle(BattleState.Lose);
        }
    }

    void EnemyTurn()
    {
        endTurnButton.interactable = false;
        state = BattleState.EnemyTurn;
        int actionIndex = UnityEngine.Random.Range(0, 2);
        Character enemy = EnemyCharacters[enemyIndex];
        enemy.YourTurn();
        if (enemy.StillAlive)
        {
            ActionBase action = Instantiate(actionBases[actionIndex]);
            activeAction = action;
            activeAction.currentCharacter = enemy;
            activeAction.transform.position = new Vector3(enemy.transform.position.x - 1, enemy.transform.position.y, enemy.transform.position.z);

            int playerRandomIndex = UnityEngine.Random.Range(0, playerCount);

            activeAction.Drag(PlayerCharacters[playerRandomIndex]);
            enemyIndex++;
            if (enemyIndex >= enemyCount)
            {
                activeAction.OnActionEnd += PlayerTurn;
                enemyIndex = 0;
            }
            else
            {
                activeAction.OnActionEnd += EnemyTurn;
            }
        }
        else
        {
            EndBattle(BattleState.Won);
        }
    }

    void EndBattle(BattleState battleState)
    {
        state = battleState;
        if (state == BattleState.Won)
        {
            wonPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
    }

    public void EndTurn()
    {
        if (state == BattleState.PlayerTurn)
        {
            if (activeAction != null)
            {
                activeAction.DestroyAction();
            }
            if (playerIndex >= playerCount)
            {
                EnemyTurn();
                playerIndex = 0;
            }
            else
            {
                PlayerTurn();
            }
        }
    }
}

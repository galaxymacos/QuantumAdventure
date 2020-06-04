using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnemyAnger : MonoBehaviour
{
    [SerializeField] private int maxValue;


    #region Property

    // Debug purpose
    public string targetPlayerName;

    public PlayerManager targetPlayer;

    public GameObject targetGameObject => targetPlayer == null ? null : targetPlayer.gameObject;
    public event Action<PlayerManager, PlayerManager> onTargetPlayerChanged;

    [SerializeField] private float angerDecreaseRate = 5f;

    public float targetPlayerAnger
    {
        get
        {
            if (playerAngerValues.ContainsKey(targetPlayer))
            {
                return playerAngerValues[targetPlayer];
            }

            return 0;
        }
    }

    public Dictionary<PlayerManager, float> playerAngerValues;

    #endregion

    #region Private Field

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        playerAngerValues = new Dictionary<PlayerManager, float>();
    }

    private void Update()
    {
        if (targetPlayer != null)
        {
            targetPlayerName = targetPlayer.gameObject.name;
        }

        // Decrease anger value gradually
        List<PlayerManager> deletePlayers = new List<PlayerManager>();

        List<PlayerManager> keys = new List<PlayerManager>();
        foreach (var key in playerAngerValues.Keys)
        {
            keys.Add(key);
        }

        foreach (PlayerManager player in keys)
        {
            playerAngerValues[player] -= angerDecreaseRate * Time.deltaTime;
            if (playerAngerValues[player] < 0)
            {
                deletePlayers.Add(player);
            }
        }

        foreach (PlayerManager player in deletePlayers)
        {
            playerAngerValues.Remove(player);
            if (targetPlayer == player)
            {
                targetPlayer = null;
            }

        }
    }

    #endregion

    #region Public Method

    public void IncreaseAngerTowards(PlayerManager playerManager, int anger)
    {
        print($"Increase anger {anger} towards {playerManager.gameObject}");

        if (!playerAngerValues.ContainsKey(playerManager))
        {
            playerAngerValues.Add(playerManager, anger);
        }

        if (targetPlayer == null)
        {
            targetPlayer = playerManager;
            onTargetPlayerChanged?.Invoke(null, targetPlayer);

            return;
        }

        PlayerManager currentTargetPlayer = targetPlayer;
        playerAngerValues[playerManager] += anger;
        float largestAnger = targetPlayerAnger;
        foreach (var player in playerAngerValues.Keys)
        {
            if (playerAngerValues[player] > largestAnger)
            {
                largestAnger = playerAngerValues[player];
                targetPlayer = player;
                onTargetPlayerChanged?.Invoke(currentTargetPlayer, player);
            }
        }
    }

    #endregion

    #region Private Method

    #endregion
}
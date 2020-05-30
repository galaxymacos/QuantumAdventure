using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutScenePlayer : MonoBehaviourPun
{
    public static CutScenePlayer instance;

    private List<GameObject> playersGameObject;
    
    public List<CutScene> cutScenes;

    public PlayableDirector mainDirector;

    private Dictionary<string, TimelineAsset> cutScenesByName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cutScenesByName = new Dictionary<string, TimelineAsset>();
        foreach (CutScene cutScene in cutScenes)
        {
            cutScenesByName.Add(cutScene.ShotName, cutScene.asset);
        }

        mainDirector.played += HidePlayers;
        mainDirector.stopped += ShowPlayers;
        playersGameObject = new List<GameObject>();
    }

    private void OnDestroy()
    {
        mainDirector.played -= HidePlayers;
        mainDirector.stopped -= ShowPlayers;
    }

    public void PlayCutSceneInAllPlayers(string cutSceneName)
    {
        photonView.RPC("Play", RpcTarget.All, cutSceneName);
    }
    
    [PunRPC]
    private void Play(string cutSceneName)
    {
        if (!cutScenesByName.ContainsKey(cutSceneName))
        {
            Debug.LogError($"Cut scene {cutSceneName} doesn't exist");
            return;
        }
        Debug.Log($"Play cut scene {cutSceneName}");
        mainDirector.playableAsset = cutScenesByName[cutSceneName];
        mainDirector.Play();

    }

    [PunRPC]
    public void HidePlayers(PlayableDirector playableDirector)
    {
        var players = FindObjectsOfType<PlayerManager>();
        foreach (PlayerManager playerManager in players)
        {
            playersGameObject.Add(playerManager.gameObject);
            playerManager.gameObject.SetActive(false);
        }
    }

    [PunRPC]
    public void ShowPlayers(PlayableDirector playableDirector)
    {
        if (playersGameObject.Count == 0)
        {
            Debug.Log("Players GameObject list is not initialized");
            return;
        }
        foreach (GameObject playerGameObject in playersGameObject)
        {
            playerGameObject.SetActive(true);
        }

    }
}
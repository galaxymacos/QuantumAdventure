using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutScenePlayer : MonoBehaviour
{
    public static CutScenePlayer instance;

    private List<GameObject> playersGameObject;
    
    public List<CutScene> cutScenes;

    private Dictionary<string, PlayableDirector> cutScenesByName;

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

        cutScenesByName = new Dictionary<string, PlayableDirector>();
        foreach (CutScene cutScene in cutScenes)
        {
            cutScenesByName.Add(cutScene.ShotName, cutScene.director);
        }

        foreach (string cutScene in cutScenesByName.Keys)
        {
            cutScenesByName[cutScene].played += HidePlayers;
            cutScenesByName[cutScene].stopped += ShowPlayers;
        }
        playersGameObject = new List<GameObject>();
    }

    private void OnDestroy()
    {
        foreach (string cutScene in cutScenesByName.Keys)
        {
            cutScenesByName[cutScene].played -= HidePlayers;
            cutScenesByName[cutScene].stopped -= ShowPlayers;
        }
    }


    public void Play(string cutSceneName)
    {
        if (!cutScenesByName.ContainsKey(cutSceneName))
        {
            Debug.LogError($"Cut scene {cutSceneName} doesn't exist");
            return;
        }
        Debug.Log($"Play cut scene {cutSceneName}");
        cutScenesByName[cutSceneName].Play();
        
    }

    public void HidePlayers(PlayableDirector playableDirector)
    {
        var players = FindObjectsOfType<PlayerManager>();
        foreach (PlayerManager playerManager in players)
        {
            playersGameObject.Add(playerManager.gameObject);
            playerManager.gameObject.SetActive(false);
        }
    }

    public void ShowPlayers(PlayableDirector playableDirector)
    {
        if (playersGameObject.Count == 0)
        {
            Debug.Log("Players GameObject list is not initialized");
            return;
        }
        foreach (GameObject playerGameObject in playersGameObject)
        {
            playerGameObject.SetActive(false);
        }

    }
}
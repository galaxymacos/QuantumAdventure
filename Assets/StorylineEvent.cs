using UnityEngine;

public class StorylineEvent: MonoBehaviour
{
    public static StorylineEvent instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError($"Destroy {gameObject.name} because there are more than one StoryLineEvent");
            Destroy(gameObject);
        }
    }

    public void Story_MariaBreakRoom()
    {
        CutScenePlayer.instance.PlayCutSceneInAllPlayers("BreakDoor");
    }
}
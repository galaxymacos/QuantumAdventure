using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "ScriptableObject/CutScene")]
public class CutScene : ScriptableObject
{
    public string ShotName;
    // public PlayableDirector director;
    public TimelineAsset asset;
}
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "ScriptableObject/CutScene")]
public class CutScene : ScriptableObject
{
    public string ShotName;
    public PlayableDirector director;
}
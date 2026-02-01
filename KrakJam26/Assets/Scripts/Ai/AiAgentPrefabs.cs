using UnityEngine;

[CreateAssetMenu(fileName = "AiAgentPrefabs", menuName = "ScriptableObjects/AiAgentPrefabs", order = 1)]
public class AiAgentPrefabs : ScriptableObject
{
    [SerializeField]
    private AiAgentBase _shyGuyPrefab;

    [SerializeField]
    private AiAgentBase _invisibleGuyPrefab;

    [SerializeField]
    private AiAgentBase _ghostPrefab;

    public AiAgentBase GetAiAgentPrefab(AiAgentType agentType)
    {
        return agentType switch
        {
            AiAgentType.ShyGuy => _shyGuyPrefab,
            AiAgentType.InvisibleGuy => _invisibleGuyPrefab,
            AiAgentType.Ghost => _ghostPrefab,
            _ => null,
        };
	}
}

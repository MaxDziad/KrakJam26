using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetMovementActiveAction", story: "Set Movement Active [IsActive]", category: "Action/Navigation", id: "9ed18ed5880812fd8043e05aa8a608e3")]
public partial class SetMovementActiveAction : Action
{
	[SerializeReference]
	public BlackboardVariable<GameObject> Agent;

	[SerializeReference]
	public BlackboardVariable<bool> IsActive;

	protected override Status OnStart()
	{
		NavMeshAgent navMeshAgent = Agent.Value.GetComponent<NavMeshAgent>();

		if (navMeshAgent == null)
		{
			LogFailure($"NavMeshAgent component not found on Agent.");
			return Status.Failure;
		}

		navMeshAgent.isStopped = !IsActive;
		return Status.Success;
	}
}


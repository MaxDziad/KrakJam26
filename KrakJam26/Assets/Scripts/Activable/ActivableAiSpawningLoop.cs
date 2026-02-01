public class ActivableAiSpawningLoop : ActivableBase
{
	public override void Activate()
	{
		AiAgentSystemManager.Instance.StartSpawningAis();
	}
	public override void Deactivate()
	{
		AiAgentSystemManager.Instance.StopSpawningAis();
	}
}

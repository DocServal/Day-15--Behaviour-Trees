using Core.Combat;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Health", story: "Set [Agent] health to [Health]", category: "Action/Boss Fight", id: "ae3faf8bfb8fe30fa75032a10d6673ea")]
public partial class SetHealthAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<int> Health;

    private Destructable destructable;

    protected override Status OnStart()
    {
        destructable = Agent.Value.GetComponentInChildren<Destructable>();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        destructable.CurrentHealth = Health.Value;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


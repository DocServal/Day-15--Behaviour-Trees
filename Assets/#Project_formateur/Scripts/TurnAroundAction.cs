using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TurnAround", story: "[Agent] turns around", category: "Action/Boss Fight", id: "2388de78e7d687851f19c885bcfb34c3")]
public partial class TurnAroundAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Agent;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var scale = Agent.Value.localScale;
        scale.x *= -1;
        Agent.Value.localScale = scale;
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


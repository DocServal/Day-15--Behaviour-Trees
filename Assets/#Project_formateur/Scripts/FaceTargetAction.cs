using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FaceTarget", story: "[Agent] faces [Target]", category: "Action/Boss Fight", id: "1a5ef4d4e8ba7bea05c90b63301b0d69")]
public partial class FaceTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    private float baseScale;

    protected override Status OnStart()
    {
        baseScale = Mathf.Abs(Agent.Value.localScale.x);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var scale = Agent.Value.localScale;
        scale.x = Agent.Value.position.x > Target.Value.position.x ? -baseScale : baseScale;
        Agent.Value.localScale = scale;
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


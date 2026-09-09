using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Core;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Freeze Time", story: "Freeze time for [Duration] seconds", category: "Action/Boss Fight", id: "c26694bc1e92920b80dc46d324e5a92e")]
public partial class FreezeTimeAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Duration;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        GameManager.Instance.FreezeTime(Duration.Value);

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


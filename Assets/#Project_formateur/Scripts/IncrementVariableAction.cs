using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Increment Variable", story: "Increment [Variable]", category: "Action", id: "134796014b9d341ec66dc608a42c4489")]
public partial class IncrementVariableAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Variable;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Variable.Value += 1;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


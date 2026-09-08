using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Target Position Comparison", story: "[Target] is to the left of [Agent]", category: "Conditions", id: "726c1c17275ea690ecd85a82e9275c8e")]
public partial class TargetPositionComparisonCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Transform> Agent;

    public override bool IsTrue()
    {
        return Target.Value.position.x < Agent.Value.position.x;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}

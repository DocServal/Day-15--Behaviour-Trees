using Core.Combat;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Hit Points", story: "[Agent] HP less than [Value]", category: "Conditions/Boss Fight", id: "8929f2b25b2666fbd2ee95caa68cfda0")]
public partial class HitPointsCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<int> Value;

    private Destructable destructable;

    public override bool IsTrue()
    {
        return destructable.CurrentHealth <= Value.Value;
    }

    public override void OnStart()
    {
        destructable = Agent.Value.GetComponentInChildren<Destructable>();
    }

    public override void OnEnd()
    {
    }
}

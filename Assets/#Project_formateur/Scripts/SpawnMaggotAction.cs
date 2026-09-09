using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Core.Combat;
using System.Collections.Generic;
using Core.Hazards;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Spawn Maggot", story: "[Agent] spawns [MaggotPrefab] at [Target]", category: "Action/Boss Fight", id: "5625a4e384f2b1f435e38a58a3e2767e")]
public partial class SpawnMaggotAction : Action
{
    [SerializeReference] public BlackboardVariable<Destructable> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> MaggotPrefab;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Hazard> Hazard;

    private Destructable maggot;

    protected override Status OnStart()
    {

        maggot = GameObject.Instantiate(MaggotPrefab.Value, Target.Value).GetComponent<Destructable>();
        maggot.transform.localPosition = Vector3.zero;
        Agent.Value.Invincible = true;
        SetHazardsActive(false);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (maggot.CurrentHealth > 0) return Status.Running;

        Agent.Value.Invincible = false;
        SetHazardsActive(true);

        return Status.Success;
    }

    private void SetHazardsActive(bool state)
    {

            Hazard.Value.gameObject.SetActive(state);
    }


    protected override void OnEnd()
    {
    }
}


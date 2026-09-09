using Core.Combat;
using Core.Character;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Shoot", story: "[Agent] shoots [Weapon] from [Origin]", category: "Action/Boss Fight", id: "c239eff85a3de79a8d2de5af1737566d")]
public partial class ShootAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Weapon> Weapon;
    [SerializeReference] public BlackboardVariable<Transform> Origin;
    [SerializeReference] public BlackboardVariable<bool> shakeCamera = new(false);

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var projectile = GameObject.Instantiate(Weapon.Value.projectilePrefab, Origin.Value.position,
    Quaternion.identity);
        projectile.Shooter = Agent.Value;

        var force = new Vector2(Weapon.Value.horizontalForce * Agent.Value.transform.localScale.x, Weapon.Value.verticalForce);
        projectile.SetForce(force);

        if (shakeCamera.Value)
            CameraController.Instance.ShakeCamera(0.5f);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


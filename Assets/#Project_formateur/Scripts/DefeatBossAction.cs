using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Core.Character;
using Core.Util;
using DG.Tweening;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Defeat Boss", story: "[Agent] is defeated", category: "Action/Boss Fight", id: "5558665f1889bf69fc2af94d0b441ed8")]
public partial class DefeatBossAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<ParticleSystem> bleedEffect;
    [SerializeReference] public BlackboardVariable<ParticleSystem> explodeEffect;
    [SerializeReference] public BlackboardVariable<float> bleedTime;

    private bool isDestroyed;

    protected override Status OnStart()
    {
        EffectManager.Instance.PlayOneShot(bleedEffect.Value, Agent.Value.transform.position);
        DOVirtual.DelayedCall(bleedTime.Value, () =>
        {
            EffectManager.Instance.PlayOneShot(explodeEffect.Value, Agent.Value.transform.position);
            CameraController.Instance.ShakeCamera(0.7f);
            isDestroyed = true;
            GameObject.Destroy(Agent.Value);
        }, false);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return isDestroyed ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
    }
}


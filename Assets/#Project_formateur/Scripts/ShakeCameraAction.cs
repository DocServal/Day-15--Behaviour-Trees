using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Core.Character;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShakeCamera", story: "Shake camera with intensity [Intensity]", category: "Action/Boss Fight", id: "c7ddca9519e9b2fe1d75f67803191ca1")]
public partial class ShakeCameraAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Intensity;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        CameraController.Instance.ShakeCamera(Intensity.Value);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


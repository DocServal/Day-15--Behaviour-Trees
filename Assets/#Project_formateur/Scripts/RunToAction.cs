using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Run to", story: "[Agent] run to [target]", category: "Action", id: "efee9bbf9342f54288c65f8fa129e23b")]
public partial class RunToAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Agent;
    [SerializeReference] public BlackboardVariable<Vector3> Target;

    [SerializeReference] public BlackboardVariable<float> speed = new BlackboardVariable<float>(5f);
    [SerializeReference] public BlackboardVariable<string> animationTriggerName = new BlackboardVariable<string>("Run");


    private Animator animator;
    private Rigidbody2D body;


    protected override Status OnStart()
    {
        animator = Agent.Value.GetComponentInChildren<Animator>();
        body = Agent.Value.GetComponent<Rigidbody2D>();
        buildupTween = DOVirtual.DelayedCall(buildupTime.Value, StartJump, false);
        animator.SetTrigger(animationTriggerName.Value);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


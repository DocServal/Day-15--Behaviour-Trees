using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Charge Towards", story: "[Agent] charge toward [target]", category: "Action", id: "dd12983132858e96c4bafc6ce0194e2d")]
public partial class ChargeTowardsAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<string> AnimationTriggerName = new("Run");

    [SerializeReference] public BlackboardVariable<float> Speed = new(4.0f);
    [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new(0.2f);

    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector3 selfHalfDimensions;

    Vector3 chargeStopPoint;


    protected override Status OnStart()
    {
        if (Agent.Value == null || Target.Value == null)
        {
            return Status.Failure;
        }
        animator = Agent.Value.GetComponentInChildren<Animator>();
        spriteRenderer = Agent.Value.GetComponentInChildren<SpriteRenderer>();
        selfHalfDimensions = spriteRenderer.bounds.extents;
        CalculateScreenBorders();
        animator.SetTrigger(AnimationTriggerName.Value);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Agent.Value == null || Target.Value == null)
        {
            return Status.Failure;
        }

        float distance = GetDistanceToTarget();
        bool destinationReached = distance <= DistanceThreshold;

        if (destinationReached)
        {
            animator.SetTrigger("Idle");
            return Status.Success;
        }
        else
        {
            NavigationUtility2D.SimpleMoveTowardsLocation(Agent.Value.transform, chargeStopPoint, Speed, distance);
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        animator.SetTrigger("Idle");
    }

    private float GetDistanceToTarget()
    {
        Vector3 agentPosition = Agent.Value.transform.position;
        return Vector3.Distance(new Vector3(agentPosition.x, chargeStopPoint.y, agentPosition.z), chargeStopPoint);
    }

    void CalculateScreenBorders()
    {
        Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));

        if (Target.Value.transform.position.x - Agent.Value.transform.position.x >= 0)
        {
            chargeStopPoint = new(right.x - selfHalfDimensions.x, 0, 0);
        }
        else
        {
            chargeStopPoint = new(left.x + selfHalfDimensions.x, 0, 0);

        }
    }
}


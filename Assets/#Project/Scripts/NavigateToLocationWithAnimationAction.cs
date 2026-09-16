using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Navigate To Location With Animation", story: "[Agent] navigates to [location] with [animationTriggerName]", category: "Action", id: "f3bf3f9d0595c5dad7225dc2aa09567e")]
public partial class NavigateToLocationWithAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Vector3> Location;
    [SerializeReference] public BlackboardVariable<string> AnimationTriggerName = new("Run");

    [SerializeReference] public BlackboardVariable<float> Speed = new(1.0f);
    [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new(0.2f);



    private NavMeshAgent m_NavMeshAgent;
    private Animator m_Animator;
    [CreateProperty] private float m_OriginalStoppingDistance = -1f;
    [CreateProperty] private float m_OriginalSpeed = -1f;

    protected override Status OnStart()
    {
        if (Agent.Value == null || Location.Value == null)
        {
            return Status.Failure;
        }

        return Initialize();
    }

    protected override Status OnUpdate()
    {
        if (Agent.Value == null || Location.Value == null)
        {
            return Status.Failure;
        }

        Vector3 agentPosition, locationPosition;
        float distance = GetDistanceToLocation(out agentPosition, out locationPosition);
        bool destinationReached = distance <= DistanceThreshold;

        if (destinationReached && (m_NavMeshAgent == null || !m_NavMeshAgent.pathPending))
        {
            m_Animator.SetTrigger("Idle");
            return Status.Success;
        }
        else if (m_NavMeshAgent == null) // transform-based movement
        {
            NavigationUtility2D.SimpleMoveTowardsLocation(Agent.Value.transform, locationPosition, Speed, distance);
        }


        return Status.Running;
    }

    protected override void OnEnd()
    {

        if (m_NavMeshAgent != null)
        {
            if (m_NavMeshAgent.isOnNavMesh)
            {
                m_NavMeshAgent.ResetPath();
            }
            m_NavMeshAgent.speed = m_OriginalSpeed;
            m_NavMeshAgent.stoppingDistance = m_OriginalStoppingDistance;
        }

        m_NavMeshAgent = null;
        m_Animator = null;
    }

    protected override void OnDeserialize()
    {
        // If using a navigation mesh, we need to reset default value before Initialize.
        m_NavMeshAgent = Agent.Value.GetComponentInChildren<NavMeshAgent>();
        if (m_NavMeshAgent != null)
        {
            if (m_OriginalSpeed >= 0f)
                m_NavMeshAgent.speed = m_OriginalSpeed;
            if (m_OriginalStoppingDistance >= 0f)
                m_NavMeshAgent.stoppingDistance = m_OriginalStoppingDistance;

            m_NavMeshAgent.Warp(Agent.Value.transform.position);
        }

        Initialize();
    }

    private Status Initialize()
    {
        if (GetDistanceToLocation(out Vector3 agentPosition, out Vector3 locationPosition) <= DistanceThreshold)
        {
            return Status.Success;
        }
        if (m_Animator == null)
        {
            m_Animator = Agent.Value.GetComponentInChildren<Animator>();

        }

        // If using a navigation mesh, set target position for navigation mesh agent.
        m_NavMeshAgent = Agent.Value.GetComponentInChildren<NavMeshAgent>();
        if (m_NavMeshAgent != null)
        {
            if (m_NavMeshAgent.isOnNavMesh)
            {
                m_NavMeshAgent.ResetPath();
            }

            m_OriginalSpeed = m_NavMeshAgent.speed;
            m_NavMeshAgent.speed = Speed;
            m_OriginalStoppingDistance = m_NavMeshAgent.stoppingDistance;
            m_NavMeshAgent.stoppingDistance = DistanceThreshold;
            m_NavMeshAgent.SetDestination(locationPosition);

        }

        m_Animator.SetTrigger("Run");
        return Status.Running;
    }

    private float GetDistanceToLocation(out Vector3 agentPosition, out Vector3 locationPosition)
    {
        agentPosition = Agent.Value.transform.position;
        locationPosition = Location.Value;
        return Vector3.Distance(new Vector3(agentPosition.x, locationPosition.y, agentPosition.z), locationPosition);
    }

}


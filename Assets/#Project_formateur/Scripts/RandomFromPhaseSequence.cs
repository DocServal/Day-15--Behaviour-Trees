using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Composite = Unity.Behavior.Composite;
using Unity.Properties;
using System.Linq;


[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Random from Phase", story: "Random from [Phases]", category: "Flow/Boss Fight", id: "19ba7fdbf32e0e8af914db90581dc9cc")]
public partial class RandomFromPhaseSequence : Composite
{
    [SerializeReference] public BlackboardVariable<List<string>> Phases;
    [SerializeReference] public BlackboardVariable<int> CurrentPhase;

    int m_RandomIndex = 0;
    private List<int> childIndexList = new List<int>();

    /// <inheritdoc cref="OnStart" />
    protected override Status OnStart()
    {
        childIndexList.Clear();
        childIndexList = Phases.Value[CurrentPhase.Value].Split(',').Select(int.Parse).ToList();

        m_RandomIndex = UnityEngine.Random.Range(0, childIndexList.Count);
        if (m_RandomIndex < childIndexList.Count)
        {
            var status = StartNode(Children[childIndexList[m_RandomIndex]]);
            if (status == Status.Success || status == Status.Failure)
                return status;

            return Status.Waiting;
        }

        return Status.Success;
    }

    /// <inheritdoc cref="OnUpdate" />
    protected override Status OnUpdate()
    {
        var status = Children[childIndexList[m_RandomIndex]].CurrentStatus;
        if (status == Status.Success || status == Status.Failure)
            return status;

        return Status.Waiting;
    }
}


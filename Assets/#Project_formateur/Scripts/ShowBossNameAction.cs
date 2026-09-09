using Core.Bosses;
using Core.UI;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShowBossName", story: "Show Boss Name from [BossInfo]", category: "Action", id: "73270e5f9bb34f38dbcc544d24b1fbfb")]
public partial class ShowBossNameAction : Action
{
    [SerializeReference] public BlackboardVariable<BossConfig> BossInfo;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        GuiManager.Instance.ShowBossName(BossInfo.Value.bossName);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


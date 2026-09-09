using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Animation Event")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Animation Event", message: "Animation event triggered", category: "Events/Boss Fight", id: "56c9df818c7d3fe12acd357cc33aaccb")]
public sealed partial class AnimationEvent : EventChannel { }


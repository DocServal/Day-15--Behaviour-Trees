using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/SpawnObjects")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "SpawnObjects", message: "Spawn [Amount] instances of [Object]", category: "Events", id: "63b1de6b9d4ef78b84d9d9e328fd0644")]
public sealed partial class SpawnObjects : EventChannel<int, GameObject> { }


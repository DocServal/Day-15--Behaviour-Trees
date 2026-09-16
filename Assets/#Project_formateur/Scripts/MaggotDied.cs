using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Maggot Died")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Maggot Died", message: "Maggot died", category: "Events/Boss Fight", id: "84190834e5c73f2907379b0029a921ca")]
public sealed partial class MaggotDied : EventChannel { }


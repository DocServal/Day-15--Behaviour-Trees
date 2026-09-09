using System;
using Core.Combat.Projectiles;
using UnityEngine;

namespace Core.Combat
{
    
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
    public class Weapon : ScriptableObject {
        public Transform weaponTransform;
        public AbstractProjectile projectilePrefab;
        public float horizontalForce = 5.0f;
        public float verticalForce = 4.0f;
    }
}
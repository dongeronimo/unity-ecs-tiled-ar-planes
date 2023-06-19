using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
namespace HelloJobSystem {
    public class RotationSpeedAuthoring : MonoBehaviour
    {
        public float DegreesPerSecond = 360.0f;

        public class RotationSpeedBaker : Baker<RotationSpeedAuthoring>
        {
            public override void Bake(RotationSpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RotationSpeedJob
                {
                    RadiansPerSecond = math.radians(authoring.DegreesPerSecond)
                });
            }
        }
        public struct RotationSpeedJob : IComponentData
        {
            public float RadiansPerSecond;
        }
    }
}
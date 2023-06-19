using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace PrefabSpawns.Authoring
{
    public class MyPrefabAuthoring : MonoBehaviour
    {
        //Prefab's data
        public float DegreesPerSecond = 360.0f;
        //Baker
        class Baker : Baker<MyPrefabAuthoring>
        {
            public override void Bake(MyPrefabAuthoring authoring)
            {
                //Creates the components, passing the data from the gameobject to the components
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PrefabRotationSpeed
                {
                    RadiansPerSecond = math.radians(authoring.DegreesPerSecond)
                });
            }
        }
    }
    //The rotation component
    public struct PrefabRotationSpeed : IComponentData
    {
        public float RadiansPerSecond;
    }
}

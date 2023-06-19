using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
namespace HelloWorld
{
    public class RotationSpeedAuthoring : MonoBehaviour
    {
        public float DegreesPerSecond = 360.0f;
    }
    public class RotationSpeedBaker : Baker<RotationSpeedAuthoring>
    {
        //Runs once for every RotationSpeedAuthoring instacne in the subscene.
        public override void Bake(RotationSpeedAuthoring authoring)
        {
            //Gets the entity and add the component to it
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotationSpeed
            {
                RadiansPerSecond = math.radians(authoring.DegreesPerSecond)
            }); 
        }
    }
    public struct RotationSpeed: IComponentData
    {
        public float RadiansPerSecond;
    }
}
using System.Collections;
using System.Collections.Generic;
using TiledPlane.Components;
using Unity.Entities;
using UnityEngine;
namespace TiledPlane.Authoring
{
    public class SphereAuthoring : MonoBehaviour
    {
        class Baker : Baker<SphereAuthoring>
        {
            public override void Bake(SphereAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new Sphere
                {
                  
                });
            }
        }
    }
}

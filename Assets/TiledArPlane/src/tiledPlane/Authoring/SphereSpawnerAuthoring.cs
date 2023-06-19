using System.Collections;
using System.Collections.Generic;
using TiledPlane.Components;
using Unity.Entities;
using UnityEngine;
namespace TiledPlane.Authoring
{
    public class SphereSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        class Baker : Baker<SphereSpawnerAuthoring>
        {
            public override void Bake(SphereSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new SphereSpawner
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}
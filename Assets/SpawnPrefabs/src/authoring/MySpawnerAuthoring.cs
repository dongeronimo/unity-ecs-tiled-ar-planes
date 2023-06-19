using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
namespace PrefabSpawns.Authoring
{
    public class MySpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;

        class Baker : Baker<MySpawnerAuthoring>
        {
            public override void Bake(MySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new Spawner { 
                    //The gotcha is here: gets the entity authored from the prefab and passes it to the component.
                    Prefab=GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic) 
                });
            }
        }
    }
    struct Spawner : IComponentData
    {
        public Entity Prefab;
    }
}
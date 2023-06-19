using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
namespace PrefabSpawns.Authoring
{
    public class MyPrefabExecute : MonoBehaviour
    {
        class Baker : Baker<MyPrefabExecute>
        {
            public override void Bake(MyPrefabExecute authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<PrefabExecute>(entity);
            }
        }
    }
    public struct PrefabExecute : IComponentData { }
}


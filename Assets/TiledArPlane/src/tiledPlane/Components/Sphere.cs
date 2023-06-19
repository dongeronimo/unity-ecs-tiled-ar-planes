using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
namespace TiledPlane.Components
{
    public struct SphereSpawner : IComponentData
    {
        public Entity Prefab;
    }
    public struct Sphere : IComponentData { }
}
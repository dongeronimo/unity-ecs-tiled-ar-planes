using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
namespace TiledPlane.Components
{
    public struct MyArPlane : IComponentData
    {
        public FixedString128Bytes Name;
        public NativeArray<float3> Boundary;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;
namespace UserInteractions.Components
{
    [MaterialProperty("_BaseColor")]
    public struct Colour : IComponentData
    {
        public float4 Value;
    }
}
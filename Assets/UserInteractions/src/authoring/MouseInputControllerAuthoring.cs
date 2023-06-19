using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UserInteractions.Components;

namespace UserInteractions.Authoring
{
    public class MouseInputControllerAuthoring : MonoBehaviour
    {
        class Baker : Baker<MouseInputControllerAuthoring>
        {
            public override void Bake(MouseInputControllerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<MyInput>(entity);
            }
        }
    }
}
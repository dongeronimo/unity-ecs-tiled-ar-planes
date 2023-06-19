
using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

using UserInteractions.Authoring;
using UserInteractions.Components;

namespace UserInteractions.Systems
{
    [UpdateAfter(typeof(InputSystem))]
    public partial class SelectableColourSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            //Will only update when a component of this type exists in the scene.
            RequireForUpdate<UserInteractionExamplesExecutor>();
            RequireForUpdate<MyInput>();
        }
        [BurstCompile]
        protected override void OnUpdate()
        {
            MyInput input = SystemAPI.GetSingleton<MyInput>();
            UnityEngine.Debug.Log($"input:{input.MouseX}, {input.MouseY}, {input.LeftClick}");
            Entities.ForEach((ref Colour color, in Selectable selectable) => {
                if (selectable.IsSelected)
                {
                    color.Value = new float4(1.0f, 0, 0, 1.0f);
                }
                else
                {
                    color.Value = new float4(0.0f, 0.0f, 0.0f, 1.0f);
                }
            }).ScheduleParallel();
        }
    }
}
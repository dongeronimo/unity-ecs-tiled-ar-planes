using PrefabSpawns.Authoring;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace PrefabSpawns.System
{
    public partial class RotationSystem : SystemBase
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PrefabExecute>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            float dt = SystemAPI.Time.DeltaTime;
            Entities
                .ForEach((ref LocalTransform transform, in PrefabRotationSpeed speed) =>
                {
                    transform = transform.RotateX(speed.RadiansPerSecond * dt)
                    .RotateY(speed.RadiansPerSecond * dt / 2)
                    .RotateZ(speed.RadiansPerSecond * dt / 4);
                }).ScheduleParallel();
            //foreach(var (transform,speed) in 
            //    SystemAPI.Query<RefRW<LocalTransform>, RefRO<PrefabRotationSpeed>>())
            //{
            //    transform.ValueRW = transform.ValueRO.RotateY(speed.ValueRO.RadiansPerSecond * dt)
            //        .RotateX(speed.ValueRO.RadiansPerSecond * dt / 1.33f)
            //        .RotateZ(speed.ValueRO.RadiansPerSecond * dt / 1.66f);
            //}
        }
    }
}

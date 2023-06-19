
using HelloWorld;

using Unity.Jobs;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using static HelloJobSystem.RotationSpeedAuthoring;
using Unity.Mathematics;

namespace HelloJobSystem
{
    public partial class MyJobSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            double elapsedTime = SystemAPI.Time.ElapsedTime;
            Entities
                .ForEach((ref LocalTransform transform, in RotationSpeedJob speed) =>
                {
                    transform = transform.RotateY(speed.RadiansPerSecond * deltaTime);
                    transform.Position.y = (float)math.sin(elapsedTime * speed.RadiansPerSecond);
                })
                .ScheduleParallel();

        }
    }

}
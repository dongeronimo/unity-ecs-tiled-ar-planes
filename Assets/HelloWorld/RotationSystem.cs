using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
namespace HelloWorld
{
    public partial struct RotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //The system won't update unless exists at least one entity having this component.
            //That is necessary bc this normally runs before the scene is loaded
            state.RequireForUpdate<ExecuteOnMainThread>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //Grabs the delta time
            float deltaTime = SystemAPI.Time.DeltaTime;
            //Queries for a set of tuples of transforms and RotationSpeed. RW and RO refers to ReadWrite and
            //RO to ReadOnly
            foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
            {
                transform.ValueRW = transform.ValueRO.RotateY(speed.ValueRO.RadiansPerSecond * deltaTime);
            }
        }
    }
}
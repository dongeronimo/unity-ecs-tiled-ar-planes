using PrefabSpawns.Authoring;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


namespace PrefabSpawns.System
{
    public partial struct SpawnSystem : ISystem
    {
        uint updateCounter;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //Wait for both the spawner entity and the prefab execute semaphore to exist. That
            //is evaluated checking for the existence of Spawner and PrefabExecute component.
            state.RequireForUpdate<Spawner>();
            state.RequireForUpdate<PrefabExecute>();
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //Seeks the components with the data needed for spinning cubes. The query is cached in source generation
            //so it has no extra costs
            var spinningGemsQuery = SystemAPI.QueryBuilder().WithAll<PrefabRotationSpeed>().Build();
            if(spinningGemsQuery.IsEmpty)
            {
                //The entity authored when the prefab was baked
                var prefab = SystemAPI.GetSingleton<Spawner>().Prefab;
                //create a thousand instances of the entity. They are copies with same component types and values. It is vaguely
                //analogue to the gameObject`s Instantiate
                var instances = state.EntityManager.Instantiate(prefab, 250000, Allocator.Temp);
                var random = Random.CreateFromIndex(updateCounter++);
                foreach(var instance in instances)
                {
                    var transform = SystemAPI.GetComponentRW<LocalTransform>(instance);
                    transform.ValueRW.Position = (random.NextFloat3() - new float3(0.5f, 0, 0.5f)) * 40;
                }
            }
        }
    }
}
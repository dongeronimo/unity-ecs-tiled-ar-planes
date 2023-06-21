
using TiledPlane.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.mathx;
namespace TiledPlane.Systems
{
    //Draw spheres at the boundaries' vertexes.
    public partial struct PlaneBoundaryVertexMarker : ISystem
    {
        const int SPHERE_POOL_SIZE = 2000;
        const float SCALE = 0.05f;
        const float RENDERABLE_OUBLIETTE = -999999.0F;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //Dont update unless we have at least one plane
            state.RequireForUpdate<MyArPlane>();
        }
        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //gets the entity authored when the prefab was baked
            SphereSpawner prefab = SystemAPI.GetSingleton<SphereSpawner>();
            var spheresQuery = SystemAPI.QueryBuilder().WithAll<Sphere>().Build();
            //Initializes the pool if it isnt initialized. The objects will be placed, far away, where they won't be seen.
            if(spheresQuery.IsEmpty == true)
            {
                NativeArray<Entity> instances = state.EntityManager.Instantiate(prefab.Prefab, SPHERE_POOL_SIZE, Allocator.Temp);
                int v = 0;
                foreach(var instance in instances)
                {
                    var name = $"sphere {v}";
                    v++;
                    state.EntityManager.SetName(instance, name);
                    var transform = SystemAPI.GetComponentRW<LocalTransform>(instance);
                    transform.ValueRW.Position = new float3(RENDERABLE_OUBLIETTE, RENDERABLE_OUBLIETTE, RENDERABLE_OUBLIETTE);
                }
            }
            NativeArray<Entity> spheres = spheresQuery.ToEntityArray(Allocator.Temp);
            int count = 0;
            //Foreach arplane places the spheres. The spheres that won't be used will be sent to the oubliette
            foreach (var (arPlane, transform) in SystemAPI.Query<RefRO<MyArPlane>, RefRO<LocalTransform>>())
            {
                foreach (float3 p in arPlane.ValueRO.Boundary)
                {
                    LocalTransform t = new LocalTransform
                    {
                        Position = p + transform.ValueRO.Position,
                        Scale = SCALE
                    };
                    //Wont crash but won't draw if the number of boundary points is greater then the buffer size.
                    if(count < spheres.Length)  
                        state.EntityManager.SetComponentData(spheres[count], t);
                    count++;
                }
            }
            //the ones that weren't used will be hidden
            for(var i=count; i<spheres.Length; i++)
            {
                LocalTransform t = new LocalTransform
                {
                    Position = new float3(RENDERABLE_OUBLIETTE, RENDERABLE_OUBLIETTE, RENDERABLE_OUBLIETTE),
                    Scale = SCALE
                };
                state.EntityManager.SetComponentData(spheres[i], t);
            }


            /////////////////////////////////////////////////////////////////////////////////
            //if (spheresQuery.IsEmpty == false)
            //{
            //    //1st attempt: this is a stupid but simple way to do so: delete all existing spheres
            //    state.EntityManager.DestroyEntity(spheresQuery);
            //}
            ////For each ar plane A create a sphere for each vertex V on it's Boundary list and correctly position this sphere in the space
            //foreach ( var (arPlane, transform) in SystemAPI.Query<RefRO<MyArPlane>, RefRO<LocalTransform>>())
            //{
            //    //Instantiates a sphere on the center.
            //    Entity centerSphere = state.EntityManager.Instantiate(prefab.Prefab);
            //    LocalTransform currentSphereTransform = SystemAPI.GetComponent<LocalTransform>(centerSphere);
            //    currentSphereTransform.Position = transform.ValueRO.Position;
            //    currentSphereTransform.Rotation = transform.ValueRO.Rotation;
            //    currentSphereTransform.Scale = SCALE;
            //    state.EntityManager.SetComponentData(centerSphere, currentSphereTransform);
            //    for(var i=0;i<arPlane.ValueRO.Boundary.Length;i++)
            //    {
            //        float3 currentVertex = arPlane.ValueRO.Boundary[i];
            //        float3 planePosition = transform.ValueRO.Position;
            //        float3 spherePosition = planePosition + currentVertex;
            //        Entity currentVertexSphere = state.EntityManager.Instantiate(prefab.Prefab);

            //        LocalTransform currentVertexSphereTransform = SystemAPI.GetComponent<LocalTransform>(currentVertexSphere);
            //        currentVertexSphereTransform.Position = spherePosition;
            //        currentVertexSphereTransform.Scale = SCALE;
            //        state.EntityManager.SetComponentData(currentVertexSphere, currentVertexSphereTransform);
            //    }
            //}
        }

    }

}
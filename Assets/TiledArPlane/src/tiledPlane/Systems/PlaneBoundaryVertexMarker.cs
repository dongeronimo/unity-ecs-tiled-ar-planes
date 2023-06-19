
using TiledPlane.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.mathx;
namespace TiledPlane.Systems
{
    //Draw spheres at the boundaries' vertexes.
    public partial struct PlaneBoundaryVertexMarker : ISystem
    {
        const float SCALE = 0.05f;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //Dont update unless we have at least one plane
            state.RequireForUpdate<MyArPlane>();
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //gets the entity authored when the prefab was baked
            SphereSpawner prefab = SystemAPI.GetSingleton<SphereSpawner>();
            var spheresQuery = SystemAPI.QueryBuilder().WithAll<Sphere>().Build();
            if (spheresQuery.IsEmpty == false)
            {
                //1st attempt: this is a stupid but simple way to do so: delete all existing spheres
                state.EntityManager.DestroyEntity(spheresQuery);
            }
            //For each ar plane A create a sphere for each vertex V on it's Boundary list and correctly position this sphere in the space
            foreach ( var (arPlane, transform) in SystemAPI.Query<RefRO<MyArPlane>, RefRO<LocalTransform>>())
            {
                //Instantiates a sphere on the center.
                Entity centerSphere = state.EntityManager.Instantiate(prefab.Prefab);
                LocalTransform currentSphereTransform = SystemAPI.GetComponent<LocalTransform>(centerSphere);
                currentSphereTransform.Position = transform.ValueRO.Position;
                currentSphereTransform.Rotation = transform.ValueRO.Rotation;
                currentSphereTransform.Scale = SCALE;
                state.EntityManager.SetComponentData(centerSphere, currentSphereTransform);
                for(var i=0;i<arPlane.ValueRO.Boundary.Length;i++)
                {
                    float3 currentVertex = arPlane.ValueRO.Boundary[i];
                    float3 planePosition = transform.ValueRO.Position;
                    float3 spherePosition = planePosition + currentVertex;
                    Entity currentVertexSphere = state.EntityManager.Instantiate(prefab.Prefab);
                    LocalTransform currentVertexSphereTransform = SystemAPI.GetComponent<LocalTransform>(currentVertexSphere);
                    currentVertexSphereTransform.Position = spherePosition;
                    currentVertexSphereTransform.Scale = SCALE;
                    state.EntityManager.SetComponentData(currentVertexSphere, currentVertexSphereTransform);
                }
            }
        }

    }
    /*
     void rotate_vector_by_quaternion(const Vector3& v, const Quaternion& q, Vector3& vprime)
    {
        // Extract the vector part of the quaternion
        Vector3 u(q.x, q.y, q.z);

        // Extract the scalar part of the quaternion
        float s = q.w;

        // Do the math
        vprime = 2.0f * dot(u, v) * u
              + (s*s - dot(u, u)) * v
              + 2.0f * s * cross(u, v);
    }*/
}
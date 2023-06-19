using System.Collections;
using System.Collections.Generic;
using TiledPlane.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
namespace TiledPlane
{
    [RequireComponent(typeof(ARPlane))]
    public class TiledPlaneController : MonoBehaviour
    {
        //For now I'll assume that the prefab is 1x1 centered in 0,0,0
        [SerializeField] private Transform TilePrefab;
        //The tiles are created using unity's scale, this value scales them to the AR world. The smaller the more tiles.
        [SerializeField] private float UniformScale = 0.01f;
        private bool EntityWasDeletedBecausePlaneWasSubsumed = false;
        private Entity MyEntity;

        private void OnDestroy()
        {
            Debug.Log($"OnDestroy {gameObject.name}");
            World w = World.DefaultGameObjectInjectionWorld;
            EntityWasDeletedBecausePlaneWasSubsumed = true;
            w.EntityManager.DestroyEntity(MyEntity);
        }
        // Start is called before the first frame update
        void Start()
        {
            var ArPlane = GetComponent<ARPlane>();
            //Creates the entity of this plane in the ECS context
            World w = World.DefaultGameObjectInjectionWorld;
            MyEntity = w.EntityManager.CreateEntity();
            w.EntityManager.AddComponent<LocalTransform>(MyEntity);
            var myPlane = new MyArPlane
            {
                Boundary = new NativeArray<float2>(),
                Name = gameObject.name
            };
            w.EntityManager.AddComponentData(MyEntity, myPlane);
            //Will be called when the boundaries of the plane change. 
            ArPlane.boundaryChanged += OnBoundaryChanged;
        }
        
        private void OnBoundaryChanged(ARPlaneBoundaryChangedEventArgs eventArgs)
        {
            var plane = eventArgs.plane;
            World w = World.DefaultGameObjectInjectionWorld;
            //If the plane isn't subsumed on another plane nor it never was then update the
            //entities. Else destroy the entity and mark as deleted.
            if (plane.subsumedBy == null && EntityWasDeletedBecausePlaneWasSubsumed == false)
            {
                UpdateLocalTransformComponent(w, plane);
                UpdateARPlaneComponent(w, plane);
            }
        }
        
        private void UpdateLocalTransformComponent(World w, ARPlane plane)
        {
            var localTransform = w.EntityManager.GetComponentData<LocalTransform>(MyEntity);
            localTransform.Position = transform.position;
            localTransform.Rotation = transform.rotation;
            localTransform.Scale = transform.localScale.x;//WARNING! I ASSUME THAT THE SCALE IS THE SAME ON THE THREE AXES.
            w.EntityManager.SetComponentData<LocalTransform>(MyEntity, localTransform);
        }
        private void UpdateARPlaneComponent(World w, ARPlane plane)
        {
            var arPlaneComponent = w.EntityManager.GetComponentData<MyArPlane>(MyEntity);
            //moves the data from the vector2 to a nativeArray of float2 and passes to the
            //component
            var arr = new float2[plane.boundary.Length];
            for(var i = 0; i < plane.boundary.Length; i++)
            {
                arr[i].x = plane.boundary[i].x;
                arr[i].y = plane.boundary[i].y;
            }
            var nativeArr = new NativeArray<float2>(arr, Allocator.Persistent);
            arPlaneComponent.Boundary = nativeArr;
            
            w.EntityManager.SetComponentData(MyEntity, arPlaneComponent);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
namespace HelloJobSystem
{
    public class HelloJobsExecutor : MonoBehaviour
    {
        public enum ExecutionType { MainThread, JobSystem }
        public ExecutionType executionType;
        class Baker : Baker<HelloJobsExecutor>
        {
            public override void Bake(HelloJobsExecutor authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                switch (authoring.executionType)
                {
                    case ExecutionType.MainThread:
                        AddComponent<HelloWorld.ExecuteOnMainThread>(entity);
                        break;
                    case ExecutionType.JobSystem:
                        AddComponent<HelloWorld.ExecuteOnJob>(entity);
                        break;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
namespace HelloWorld
{
    public class HelloWorldExecutorHack : MonoBehaviour
    {
        public enum ExecutionType { MainThread, JobSystem }
        public ExecutionType executionType;
        class Baker : Baker<HelloWorldExecutorHack>
        {
            public override void Bake(HelloWorldExecutorHack authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                switch (authoring.executionType)
                {
                    case ExecutionType.MainThread:
                        AddComponent<ExecuteOnMainThread>(entity);
                        break;
                    case ExecutionType.JobSystem:
                        AddComponent<ExecuteOnJob>(entity);
                        break;
                }
            }
        }
    }
}

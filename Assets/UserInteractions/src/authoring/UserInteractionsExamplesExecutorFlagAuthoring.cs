using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
namespace UserInteractions.Authoring
{
    /// <summary>
    /// A single component of the type UserInteractionExamplesExecutor to verify if the
    /// scene is built before running the systems.
    /// </summary>
    public class UserInteractionsExamplesExecutorFlagAuthoring : MonoBehaviour
    {
        class Baker : Baker<UserInteractionsExamplesExecutorFlagAuthoring>
        {
            public override void Bake(UserInteractionsExamplesExecutorFlagAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<UserInteractionExamplesExecutor>(entity);
            }
        }
    }

    public struct UserInteractionExamplesExecutor:IComponentData { }
}
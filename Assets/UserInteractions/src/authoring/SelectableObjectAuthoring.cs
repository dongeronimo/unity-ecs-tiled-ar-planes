using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UserInteractions.Components;

namespace UserInteractions.Authoring
{
    /// <summary>
    /// All objects that are selectable will need this component attached to the gameobject
    /// </summary>
    public class SelectableObjectAuthoring : MonoBehaviour
    {
        /// <summary>
        /// Bakes the component, adding the Selectable component to the entity baked from this
        /// gameObject
        /// </summary>
        class Baker : Baker<SelectableObjectAuthoring>
        {
            public override void Bake(SelectableObjectAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<Selectable>(entity);
                AddComponent<Colour>(entity);
            }
        }
    }
}
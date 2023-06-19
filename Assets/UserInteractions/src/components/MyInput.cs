using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
namespace UserInteractions.Components
{
    public struct MyInput : IComponentData
    {
        public float MouseX;
        public float MouseY;
        public bool LeftClick;
    }
}

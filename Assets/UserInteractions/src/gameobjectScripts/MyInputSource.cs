using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UserInteractions.Components;

namespace UserInteractions.Systems
{
    public class MyInputSource : MonoBehaviour
    {
        public static MyInputSource Instance;
        public float MouseX;
        public float MouseY;
        public bool LeftClick;
        private void Awake()
        {
            Instance = this;
        }
        private void Update()
        {
            MouseX = Input.mousePosition.x;
            MouseY = Input.mousePosition.y;
            LeftClick = Input.GetMouseButton(0);
        }
    }
}

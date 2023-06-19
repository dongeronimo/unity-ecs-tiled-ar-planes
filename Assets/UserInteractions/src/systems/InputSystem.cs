using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UserInteractions.Components;

namespace UserInteractions.Systems
{
    public partial class InputSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            if (SystemAPI.HasSingleton<MyInput>() && MyInputSource.Instance != null)
            {
                RefRW<MyInput> inputSingleton = SystemAPI.GetSingletonRW<MyInput>();
                inputSingleton.ValueRW.MouseX = MyInputSource.Instance.MouseX;
                inputSingleton.ValueRW.MouseY = MyInputSource.Instance.MouseY;
                inputSingleton.ValueRW.LeftClick = MyInputSource.Instance.LeftClick;
                UnityEngine.Debug.Log("HasSingleton in InputSystem = true");
            }
        }
    }
}
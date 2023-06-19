using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
namespace HelloWorld
{
    public struct ExecuteOnMainThread : IComponentData
    {
        //I'm just a dummy to guarantee that the scene exists and the systems won't
        //update before the scene is there
    }
    public struct ExecuteOnJob: IComponentData
    {

    }
}
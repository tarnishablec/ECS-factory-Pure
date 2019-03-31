using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct RobotNameComponent : IComponentData
{
    public NativeString64 Value;
}

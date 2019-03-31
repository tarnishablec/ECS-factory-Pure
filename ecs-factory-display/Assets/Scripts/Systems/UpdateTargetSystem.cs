using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class UpdateTargetSystem : ComponentSystem
{
    struct Group
    {
        public readonly int Length;
        public ComponentDataArray<RobotNameComponent> robotNames;
        public ComponentDataArray<TargetPositionComponent> targetPositions;
    }

    [Inject]
    Group group;

    protected override void OnUpdate()
    {
        var robotsInfo = RobotPool.robotsData;
        for (int i = 0; i < group.Length; i++)
        {
        }
    }
}

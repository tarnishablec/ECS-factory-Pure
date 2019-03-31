using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class MoveToTargetSystem : ComponentSystem
{
    struct Group
    {
        public readonly int Length;
        public ComponentDataArray<Position> positions;
        public ComponentDataArray<TargetPositionComponent> targetPosition;
    }

    [Inject]
    Group group;
    //readonly float moveTime = Bootstrap.GetBootstrap<RobotEntityBootstrap>().moveTime;

    protected override void OnUpdate()
    {
        float moveTime = Bootstrap.GetBootstrap<RobotEntityBootstrap>().moveTime;

        var deltaTime = Time.deltaTime;

        for (int i = 0; i < group.Length; i++)
        {
            float3 nowPos = group.positions[i].Value;
            float3 tarPos = group.targetPosition[i].Value;
            //float distance = math.distance(tarPos, nowPos);
            group.positions[i] = new Position { Value = math.lerp(nowPos, tarPos, deltaTime/moveTime) };
        }
    }
}

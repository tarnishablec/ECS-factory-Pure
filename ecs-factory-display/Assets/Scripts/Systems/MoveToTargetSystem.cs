using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class MoveToTargetSystem : ComponentSystem
{
    struct Group
    {
        public readonly int Length;
        public ComponentArray<Transform> transforms;
        public ComponentDataArray<TargetPositionComponent> targetPosition;
    }

    [Inject]
    Group group;
    //readonly float moveTime = Bootstrap.GetBootstrap<RobotEntityBootstrap>().moveTime;

    protected override void OnUpdate()
    {
        float moveTime = Bootstrap.GetBootstrap<RobotEntityBootstrap>().moveTime;

        for (int i = 0; i < group.Length; i++)
        {
            Vector3 nowPos = group.transforms[i].position;
            float3 tarPos = group.targetPosition[i].Value;
            float distance = math.distance(tarPos, nowPos);
            group.transforms[i].position = Vector3.MoveTowards(nowPos, tarPos, distance / moveTime);
        }
    }
}

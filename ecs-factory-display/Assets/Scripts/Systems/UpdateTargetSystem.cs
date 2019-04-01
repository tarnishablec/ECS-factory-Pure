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

    Dictionary<string, Dictionary<string, string>> robotsInfo;

    private readonly int width = 2478;
    private readonly int height = 997;

    private readonly int mapleftbottomx = -46893;
    private readonly int mapleftbottomy = 81419;

    protected override void OnUpdate()
    {
        robotsInfo = RobotPool.robotsData;
        for (int i = 0; i < group.Length; i++)
        {
            var info = robotsInfo[group.robotNames[i].Value.ToString()];

            var x = int.Parse(info["x"]);
            var y = int.Parse(info["y"]);

            int xPos = width * Mathf.Abs(mapleftbottomy - y) / 230113;
            int yPos = height * (1 - Mathf.Abs(x - mapleftbottomx)) / 93276;

            float3 target = new float3(xPos, 0, yPos);
            group.targetPositions[i] = new TargetPositionComponent { Value = target };
        }
    }
}

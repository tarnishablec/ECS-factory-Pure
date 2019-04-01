using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using System.Text.RegularExpressions;
using Unity.Collections;

public class SwitchColorSystem : ComponentSystem
{
    struct Group
    {
        public readonly int Length;
        public EntityArray entities;
        [ReadOnly] public SharedComponentDataArray<RenderMesh> renderMeshes;
        public ComponentDataArray<RobotNameComponent> robotNames;
    }

    [Inject]
    Group group;

    protected override void OnUpdate()
    {
        RobotEntityBootstrap robotEntityBootstrap = Bootstrap.GetBootstrap<RobotEntityBootstrap>();

        var robotsinfo = RobotPool.robotsData;
        for (int i = 0; i < group.Length; i++)
        {
            var status = robotsinfo[group.robotNames[i].Value.ToString()]["robot_status"];

            if ((status.IndexOf("ArrivedAtPark_none") >=0)||(status.IndexOf("runto_Park") >=0)||(status.ToLower().IndexOf("idle")>=0) || (status.ToLower().IndexOf("undocking") >= 0))
            {
                PostUpdateCommands.SetSharedComponent(group.entities[i], new RenderMesh { mesh = robotEntityBootstrap.robotMesh, material = robotEntityBootstrap.ST });
            }
            if ((status.ToLower().IndexOf("charging") >= 0) || (status.ToLower().IndexOf("reflecting") >= 0) || (status.ToLower().IndexOf("idle") >= 0) || (status.IndexOf("runto_Dock") >= 0)||(status.IndexOf("Dock move finished")>=0) || (status.IndexOf("Dock") >= 0)||(status.IndexOf("Dock finish reflection")>=0) || (status.IndexOf("Dock reflecting") >= 0))
            {
                PostUpdateCommands.SetSharedComponent(group.entities[i], new RenderMesh { mesh = robotEntityBootstrap.robotMesh, material = robotEntityBootstrap.CH });
            }
            if ((status.IndexOf("runto") >= 0) || (status.IndexOf("waiting") >= 0) || (status.IndexOf("Set_Top") >= 0)||(status.IndexOf("CheckEach_Top")>=0) || (status.IndexOf("reflecting") >= 0) || (status.IndexOf("reflector") >= 0)||(status.IndexOf("BaseStart")>=0) || (status.IndexOf("move") >= 0) || (status.IndexOf("Query_MES") >= 0) || (status.IndexOf("point") >= 0) || (status.IndexOf("curve_move") >= 0)||(status.IndexOf("Finish")>=0))
            {
                PostUpdateCommands.SetSharedComponent(group.entities[i], new RenderMesh { mesh = robotEntityBootstrap.robotMesh, material = robotEntityBootstrap.PT });
            }
            if ((status.IndexOf("Failed") >= 0) || (status.IndexOf("Stopped") >= 0))
            {
                PostUpdateCommands.SetSharedComponent(group.entities[i], new RenderMesh { mesh = robotEntityBootstrap.robotMesh, material = robotEntityBootstrap.UD });
            }

        }
    }
}

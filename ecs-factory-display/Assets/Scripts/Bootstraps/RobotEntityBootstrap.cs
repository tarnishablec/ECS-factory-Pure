using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class RobotEntityBootstrap : Bootstrap
{
    RobotEntityBootstrap instace = GetBootstrap<RobotEntityBootstrap>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Boot()
    {

    }
}

using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Hablar", story: "[Self] habla con [Player]", category: "Action", id: "1dc076858af891a3d04b991772927d4b")]
public partial class HablarAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    protected override Status OnStart()
    {
        return Status.Running;
        
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
        
    }

    protected override void OnEnd()
    {
    }
    
}


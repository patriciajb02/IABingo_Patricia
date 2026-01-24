using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Autoincrementar", story: "Increment [int] in 1 (monke suma)", category: "Action", id: "6dacd28ded69a2f4b548b1ded5d00e41")]
public partial class AutoincrementarAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Int;
    protected override Status OnStart()
    {
        Int.Value++;
        return Status.Success;
    }
    
}


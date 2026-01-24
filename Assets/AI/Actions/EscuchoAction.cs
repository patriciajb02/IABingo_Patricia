using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Escucho", story: "[Self] is listening noise in [Location] (monke escucha)", category: "Action", id: "2f107557c107f3f862021dc0a5043c8b")]
public partial class EscuchoAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector3> Location;
    [SerializeReference] public BlackboardVariable<float> ListeningRange;
    
    private bool noiseDetected = false;

    protected override Status OnStart()
    {
        return Status.Running;
        
    }

    protected override Status OnUpdate()
    {
        if (noiseDetected)
            return Status.Running;
        
        return Status.Success;
        
    }
    
    private void ReceiveNoise(Vector3 noiseLocation)
    {
        Debug.Log("Recibido el escuching 😔👍");
        Vector3 distanceToNoise = noiseLocation - Self.Value.transform.position;
        
        if (distanceToNoise.magnitude <= ListeningRange.Value)
        {
            Location.Value = noiseLocation;
            noiseDetected = true;
            
        }

    }

    protected override void OnEnd()
    {
        noiseDetected = false;
        
    }
}


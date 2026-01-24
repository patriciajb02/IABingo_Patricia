using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CalcularPuntoCercano", story: "[Self] calculates [PuntoCercano] around [PointOfInterest] (dónde está monke?)", category: "Action", id: "b12cf08bc54190469630332ac1bb793e")]
public partial class CalcularPuntoCercanoAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector3> NearbyPoint;
    [SerializeReference] public BlackboardVariable<Vector3> PointOfInterest;
    [SerializeReference] public BlackboardVariable<float> SearchRadius;
    [SerializeReference] public BlackboardVariable<float> MinimumDistance;
    
    
    protected override Status OnStart()
    {
        Vector3 randomPoint;
        //Mientras se calcula el punto devuelve Running
        do
        {
            randomPoint = PointOfInterest.Value + Random.insideUnitSphere * SearchRadius.Value;

        } while (!IsValidPoint(randomPoint));

        NearbyPoint.Value = randomPoint;
        //Cuando lo acabe de calcular devuelve Success
        return Status.Success;
    }

    private bool IsValidPoint(Vector3 point)
    {
        if(Vector3.Distance(Self.Value.transform.position, point) < MinimumDistance.Value)
            return false;
        
        return NavMesh.SamplePosition(point, out NavMeshHit hit, 0.02f, NavMesh.AllAreas);
        
    }

}


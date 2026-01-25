using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BuscarTargetPerdido", story: "[Self] checks if [Target] is lost (mono pierde a mono)", category: "Action", id: "d1d7c6608867b761ebefa6f7f3bba8b2")]
public partial class BuscarTargetPerdidoAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<Vector3> PointOfInterest;
    [SerializeReference] public BlackboardVariable<float> Radius;
    [SerializeReference] public BlackboardVariable<float> Angle;
    [SerializeReference] public BlackboardVariable<String> WhatIsTarget;
    [SerializeReference] public BlackboardVariable<String> WhatIsObstacle;

        //⚠️ CONVERTIR A LAYERMASK LOS STRINGS
    private int whatIsTargetLayer;
    private int whatIsObstacleLayer;
    
    private Collider[] results = new Collider[1];
    
    protected override Status OnStart()
    {
            //❗⬆️ Desplazamiento de bits para CONVERTIR A LAYERMASK LOS STRINGS
        whatIsTargetLayer = 1 << LayerMask.NameToLayer(WhatIsTarget.Value);
        whatIsObstacleLayer = 1 << LayerMask.NameToLayer(WhatIsObstacle.Value);
        
        return Status.Running;
        
    }

    protected override Status OnUpdate()
    {
        if (Self.Value == null || Target.Value == null) return Status.Failure;
        
            //Busca colliders del target dentro del radio de visión
        if (Physics.OverlapSphereNonAlloc(Self.Value.transform.position, Radius.Value, 
                                          results, whatIsTargetLayer) <= 0)
        {
            PointOfInterest.Value = Target.Value.transform.position;
            Target.Value = null;
            return Status.Success;
            
        }

        if (results[0] == null) return Status.Running;

        Vector3 directionToTarget = results[0].transform.position - Self.Value.transform.position;

        if (Vector3.Angle(Self.Value.transform.forward, directionToTarget) > Angle.Value)
        {
            PointOfInterest.Value = Target.Value.transform.position;
            Target.Value = null;
            return Status.Success;
            
        }

        if (Physics.Raycast(Self.Value.transform.position, directionToTarget,
                            directionToTarget.magnitude, whatIsObstacleLayer))
        {
            PointOfInterest.Value = Target.Value.transform.position;
            Target.Value = null;
            return Status.Success;
            
        }

        return Status.Running;
        
    }


    protected override void OnEnd()
    {
    }
    
}


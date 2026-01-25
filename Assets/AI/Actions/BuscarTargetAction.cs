using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BuscarTarget", story: "[Self] is searching for [Target] (monke busca monke)", category: "Action", id: "a956e60fc5acd4f68a5691b111bdd30b")]
public partial class BuscarTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
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
            //Mirar si hay un objetivo en el radio
        if (Physics.OverlapSphereNonAlloc(Self.Value.transform.position, Radius, results, whatIsTargetLayer) <= 0)
            return Status.Running;
            
        Vector3 directionToTarget = results[0].transform.position - Self.Value.transform.position;
        
            //El ángulo de visión
        if (Vector3.Angle(Self.Value.transform.forward, directionToTarget) > Angle)
            return Status.Running;
            
            //Si no se encuentra nada sigue corriendo la tarea de Patrullar
            //Un rayo desde enemigo hasta el objetivo y detectas si hay obstáculo, si lo hay sigue a lo suyo
        if (Physics.Raycast(Self.Value.transform.position, directionToTarget, directionToTarget.magnitude, whatIsObstacleLayer))
            return Status.Running;
        
            //Los posibles resultados que ha podido tener se meten al array de collider
            //Modificamos el valor de Target. Target es el gameObject introducido en el array
            //Esto es tras pasar los 3 filtros anteriores
        Target.Value = results[0].gameObject;
        
            //Si encuentra algo entra en Success y acaba
        return Status.Success;

    }

    protected override void OnEnd()
    {
    }
}


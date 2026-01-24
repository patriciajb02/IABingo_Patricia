using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Guiar", story: "Alcanzar [Destino] por los [PuntosGuia] en [numeroPosicion]", category: "Action", id: "89a616c2eca2a1ffaac7c0bd0b8e330b")]
public partial class GuiarAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Destino;
    [SerializeReference] public BlackboardVariable<List<GameObject>> PuntosGuia;
    [SerializeReference] public BlackboardVariable<int> NumeroPosicion;

    protected override Status OnStart()
       {
           if (PuntosGuia.Value == null || PuntosGuia.Value.Count == 0)
           {
               return Status.Failure;
           }
   
           if (NumeroPosicion.Value >= PuntosGuia.Value.Count)
           {
               return Status.Failure; 
           }
   
           Destino.Value = PuntosGuia.Value[NumeroPosicion.Value];
   
           NumeroPosicion.Value++;
   
           return Status.Success;
       }

}


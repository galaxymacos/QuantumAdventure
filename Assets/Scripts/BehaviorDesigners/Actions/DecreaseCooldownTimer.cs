using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables;
using UnityEngine;

public class DecreaseCooldownTimer : Action
{
    private BehaviorTree behaviorTree;
    private SharedFloat cooldownCounter;
    // Start is called before the first frame update
    void Start()
    {
    }

    public override void OnStart()
    {
        base.OnStart();
        behaviorTree = GetComponent<BehaviorTree>();
        cooldownCounter = (SharedFloat)behaviorTree.GetVariable("CooldownCounter");
    }

    public override TaskStatus OnUpdate()
    {
        cooldownCounter.Value -= Time.deltaTime;
        behaviorTree.SetVariableValue("CooldownCounter", cooldownCounter);
        return TaskStatus.Success;
    }
}

using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables;
using UnityEngine;

public class DecreaseCooldownTimer : Action
{
    private BehaviorTree _behaviorTree;
    private SharedFloat _cooldownCounter;
    // Start is called before the first frame update
    void Start()
    {
    }

    public override void OnStart()
    {
        base.OnStart();
        _behaviorTree = GetComponent<BehaviorTree>();
        _cooldownCounter = (SharedFloat)_behaviorTree.GetVariable("CooldownCounter");
    }

    public override TaskStatus OnUpdate()
    {
        _cooldownCounter.Value -= Time.deltaTime;
        _behaviorTree.SetVariableValue("CooldownCounter", _cooldownCounter);
        return TaskStatus.Success;
    }
}

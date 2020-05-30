using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsGameObjectNull : Conditional
{
    public SharedGameObject Target;
    public override TaskStatus OnUpdate()
    {
        if (Target.Value == null)
        {
            return TaskStatus.Failure;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}
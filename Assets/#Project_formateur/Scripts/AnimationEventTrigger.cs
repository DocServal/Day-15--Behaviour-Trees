using UnityEngine;

public class AnimationEventTrigger : MonoBehaviour
{

    public AnimationEvent animationEvent;
    public AnimationEvent attackStart;
    public AnimationEvent attackEnd;

    public AnimationEvent rollEnd;

    public void OnAnimationEvent()
    {
        animationEvent.SendEventMessage();
    }

    public void OnAttackStart()
    {
        attackStart.SendEventMessage();
    }

    public void OnAttackEnd()
    {
        attackEnd.SendEventMessage();
    }

    public void OnRollEnd()
    {
        rollEnd.SendEventMessage();
    }


}

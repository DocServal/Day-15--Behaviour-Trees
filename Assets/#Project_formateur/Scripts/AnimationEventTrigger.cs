using UnityEngine;

public class AnimationEventTrigger : MonoBehaviour
{

    public AnimationEvent animationEvent;

    public void OnAnimationEvent()
    {
        animationEvent.SendEventMessage();
    }

}

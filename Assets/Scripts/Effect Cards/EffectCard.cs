using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectCard : MonoBehaviour, IEffectable
{
    public virtual void Dispose()
    {
        //Burn Effect
        //Wait until end of burn effect
        //Teleport Card To Dispose Pile
        Execute();
        
    }

    public virtual void Draw()
    {
        //Animate Card To Reveal itself
        //Wait a few Seconds
        Dispose();
    }

    public virtual void Execute()
    {
        
    }
}

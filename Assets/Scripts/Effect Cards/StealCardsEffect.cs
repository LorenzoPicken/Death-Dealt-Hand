using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealCardsEffect : EffectCard
{
    [SerializeField] private AIBehaviour enemy;
    [SerializeField] private PlayerStateManager player;
    public override void Draw()
    {
        base.Draw();
    }

    public override void Dispose()
    {
        base.Dispose();

    }

    public override void Execute()
    {
        
    }
}

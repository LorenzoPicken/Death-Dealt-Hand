using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectLastCards : EffectCard
{
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
        if(GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            GAMEMANAGER.Instance.WasPickupOverride = true;
            GAMEMANAGER.Instance.currentPrio = PickupPrio.PLAYER;
        }
        else
        {
            GAMEMANAGER.Instance.WasPickupOverride = true;
            GAMEMANAGER.Instance.currentPrio = PickupPrio.ENEMY;
        }
    }
}

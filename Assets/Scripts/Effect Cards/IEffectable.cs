using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectable 
{
    public void Draw();

    public void Execute();


    public void Dispose();
}

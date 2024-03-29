using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectCard : MonoBehaviour
{
    [SerializeField] public Material frontMaterial;
    [SerializeField] public Material backMaterial;

    public abstract void Execute();

    void Awake()
    {
        frontMaterial.SetFloat("_Dissolve_Value", -1);
        backMaterial.SetFloat("_Dissolve_Value", -1);
    }

}

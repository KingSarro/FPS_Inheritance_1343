using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State: MonoBehaviour{

    protected StateCore sCore;
    public bool isComplete = false;
    protected float distanceFromTarget;
    [SerializeField] protected float targetDistanceThreshold;
    protected Vector3 targetLocation;

    public virtual void Enter_State(){}
    public virtual void Update_State(){}
    public virtual void Exit_State(){}

    public void SetCore(StateCore core){
        sCore = core;
    }
}

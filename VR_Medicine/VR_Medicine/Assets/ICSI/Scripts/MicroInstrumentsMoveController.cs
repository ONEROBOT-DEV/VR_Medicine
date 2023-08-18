using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MicroInstrumentsMoveController : ActionInteractableObject
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody stinger;
    [SerializeField] private Rigidbody holder;
    [SerializeField] private Vector2 boxEnterSize;
    [SerializeField] private UnityEvent onStingerEnterZone;
    [SerializeField] private UnityEvent onHolderEnterZone;
    [SerializeField] private UnityEvent onAllInZoneEvent;
    private bool isInstrumentsInCameraZone;
    private bool freezeStinger;
    private bool freezeHolder;
    private Vector3 localBarrier;
    private Vector3 localStingerPosition;
    private Vector3 localHolderPosition;
    private Vector3 defaultStingerPosition;
    private Vector3 defaultHolderPosition;
    private InstrumentMovements stingerController;
    private InstrumentMovements holderController;
    private int counterWaitingInstruments;

    public void FreezeStinger()
    {
        stinger.isKinematic = true;
        freezeStinger = true;
    }

    public void MoveStingerToRightBarrier()
    {
        stingerController.SetToBarrierPosition(ref localStingerPosition);


        stinger.transform.DOLocalMove(localStingerPosition, .1f);
    }
    public void UnfreezeStinger()
    {
        freezeStinger = false;
    }
    public void FreezeHolder()
    {
        holder.isKinematic = true;
        freezeHolder = true;
    }
    public void StingerFreezeMoveZPosition() => stingerController.isFreezeZMove = true;
    public void HolderFreezeMoveZPosition() => holderController.isFreezeZMove = true;
    public void UnfreezeInstruments()
    {
        freezeHolder = false;
        freezeStinger = false;
    }

    public void SetHolderToDefaultPosition()
    {
        holder.transform.position = defaultHolderPosition;
        holder.isKinematic = true;
    }

    public void SetStingerToDefaultPosition()
    {
        stinger.transform.position = defaultStingerPosition;
        stinger.isKinematic = true;
    }

    public void MoveStinger(Vector2 direction)
    {
        if (freezeStinger) return;
        
        localStingerPosition += new Vector3(direction.x, 0, direction.y) * (speed * Time.deltaTime);

        stingerController.OnMove(ref localStingerPosition);

        stinger.transform.localPosition = localStingerPosition;
    }

    public void MoveHolder(Vector2 direction)
    {
        if (freezeHolder) return;
        
        localHolderPosition += new Vector3(direction.x, 0, direction.y) * (speed * Time.deltaTime);

        holderController.OnMove(ref localHolderPosition);

        holder.transform.localPosition = localHolderPosition;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(boxEnterSize.x, 0, boxEnterSize.y));
    }

    private void Start()
    {
        localBarrier = new Vector3(boxEnterSize.x, 0, boxEnterSize.y);
        defaultStingerPosition = localStingerPosition = stinger.transform.localPosition;
        defaultHolderPosition = localHolderPosition = holder.transform.localPosition;
        
        counterWaitingInstruments = 2;
        stingerController = new InstrumentsOutsideCamera(localBarrier, () =>
        {
            onStingerEnterZone.Invoke();
            stingerController = new InstrumentsInCamera(localBarrier);
            OnInstrumentInZone();
        });

        holderController = new InstrumentsOutsideCamera(localBarrier, () =>
        {
            onHolderEnterZone.Invoke();
            holderController = new InstrumentsInCamera(localBarrier);
            OnInstrumentInZone();
        });
    }

    private void OnInstrumentInZone()
    {
        counterWaitingInstruments--;
        if (counterWaitingInstruments == 0)
            OnStopInstruments();
    }

    private void OnStopInstruments()
    {
        onAllInZoneEvent.Invoke();
        //InvokeEndAction();
    }
}

public abstract class InstrumentMovements
{
    protected readonly Vector3 cameraBarrier;
    public bool isFreezeZMove;

    protected InstrumentMovements(Vector3 barrier)
    {
        cameraBarrier = barrier / 2;
    }

    public abstract void OnMove(ref Vector3 newLocalPosition);

    public virtual void SetToBarrierPosition(ref Vector3 newLocalPosition)
    {
        
    }
}

public class InstrumentsOutsideCamera : InstrumentMovements
{
    private readonly Action onInstrumentEnterZone;
    private bool isInZone;

    public InstrumentsOutsideCamera(Vector3 barrier, Action onEnterZone) : base(barrier)
    {
        onInstrumentEnterZone = onEnterZone;
    }

    public override void OnMove(ref Vector3 newLocalPosition)
    {
        if (isInZone) return;

        if (isFreezeZMove) newLocalPosition.z = 0;

        if (newLocalPosition.x > -cameraBarrier.x && newLocalPosition.x < cameraBarrier.x
                                                  && newLocalPosition.z > -cameraBarrier.z &&
                                                  newLocalPosition.z < cameraBarrier.z)
        {
            isInZone = true;
            onInstrumentEnterZone?.Invoke();
        }
    }
}

public class InstrumentsInCamera : InstrumentMovements
{
    public InstrumentsInCamera(Vector3 barrier) : base(barrier)
    {
    }

    public override void SetToBarrierPosition(ref Vector3 newLocalPosition)
    {
        if (newLocalPosition.x > 0) newLocalPosition.x = cameraBarrier.x;
        else newLocalPosition.x = -cameraBarrier.x;

        newLocalPosition.z = 0;
    }

    public override void OnMove(ref Vector3 newLocalPosition)
    {
        if (isFreezeZMove) newLocalPosition.z = 0;
        
        newLocalPosition.x = Mathf.Clamp(newLocalPosition.x, -cameraBarrier.x, cameraBarrier.x);
        newLocalPosition.z = Mathf.Clamp(newLocalPosition.z, -cameraBarrier.z, cameraBarrier.z);
    }
}
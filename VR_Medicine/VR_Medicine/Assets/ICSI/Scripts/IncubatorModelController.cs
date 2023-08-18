using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class IncubatorModelController : MonoBehaviour
{
    [SerializeField] private GameObject defaultHandCollider;
    [SerializeField] private Vector3 defaultRotate;
    [SerializeField] private GameObject openHandCollider;
    [SerializeField] private Vector3 openRotate;
    [SerializeField] private Transform boneCap;
    [SerializeField] private UnityEvent onOpenEvent;
    [SerializeField] private UnityEvent onCloseEvent;

    public void SetOpenCap()
    {
        defaultHandCollider.SetActive(false);
        boneCap.DOKill();
        boneCap.DOLocalRotate(openRotate, .25f).OnComplete(() =>
        {
            //openHandCollider.SetActive(true);
            onOpenEvent.Invoke();
        });
    }

    public void SetCloseCap()
    {
        openHandCollider.SetActive(false);
        boneCap.DOKill();
        boneCap.DOLocalRotate(defaultRotate, .25f).OnComplete(() =>
        {
            //defaultHandCollider.SetActive(true);
            onCloseEvent.Invoke();
        });
    }
}

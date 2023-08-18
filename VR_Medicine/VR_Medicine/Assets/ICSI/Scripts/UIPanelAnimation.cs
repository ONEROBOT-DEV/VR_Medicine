using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class UIPanelAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform background;
    [SerializeField] private Transform[] panelParts;
    [SerializeField] private float delayTODisable;
    [SerializeField] private UnityEvent onDisable;
    [SerializeField] private UnityEvent onEnable;
    private Tween delaDisable;
    [Button]
    public void DisablePanel()
    {
        delaDisable = DOVirtual.DelayedCall(delayTODisable, DisableAnimation);
    }
    
    public void EnablePanel()
    {
        EnableAnimation();
    }

    private void OnEnable()
    {
        EnableAnimation();
    }

    private void DisableAnimation()
    {
        delaDisable.Kill();
        foreach (var part in panelParts)
        {
            part.DOKill();
            part.DOScale(Vector3.zero, .2f).OnComplete(() =>
            {
                part.transform.localScale = Vector3.zero;
            });
        }

        delaDisable = DOVirtual.DelayedCall(panelParts.Any() ? .2f : 0f, () =>
        {
            background.DOKill();
            background.DOScaleY(0, .25f).OnComplete(() =>
            {
                onDisable.Invoke();
                gameObject.SetActive(false);
            });
        });
    }

    private void EnableAnimation()
    {
        gameObject.SetActive(true);
        delaDisable.Kill();
        onEnable.Invoke();
        foreach (var part in panelParts)
        {
            part.DOKill();
            part.transform.localScale = Vector3.zero;
        }

        background.DOKill();
        background.localScale = Vector3.right + Vector3.forward;
        background.DOScaleY(1, .25f).OnComplete(() =>
        {
            foreach (var part in panelParts)
            {
                part.DOKill();
                part.DOScale(Vector3.one, .2f);
            }
        });
    }
}
using System;
using System.Linq;
using Autohand;
using Autohand.Demo;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialTriggerController : MonoBehaviour, ITutorialDisabler, IOculusControllerButtonDownListener
{
    [SerializeField] private UIPanelAnimation UIPanelAnimation;
    [SerializeField] private Image buttonDownImage;
    [SerializeField] private Image wave;
    [SerializeField] private Hand targetHand;
    [SerializeField] private CommonButton listenButtonDown;
    [SerializeField] private float defaultLocalMove;
    [SerializeField] private float targetLocalMove;
    private HandButtonHandler buttonHandler;
    private bool isCompleted;
    private bool isStartAnimation;
    private Tween animation;

    public void OnClickPrimaryButtonDown(Hand hand, CommonButton button)
    {
        isCompleted = true;
        OnTutorialIsEnd?.Invoke();
        buttonHandler.OnButtonDown -= OnClickPrimaryButtonDown;
        UIPanelAnimation?.DisablePanel();

        OnTutorialIsEnd = null;
    }

    public void DisablePanel()
    {
        UIPanelAnimation?.DisablePanel();
        isStartAnimation = false;
        buttonHandler.OnButtonDown -= OnClickPrimaryButtonDown;
        animation.Kill();
    }

    public Action OnTutorialIsEnd { get; set; }

    [Sirenix.OdinInspector.Button]
    public void StartTutorial(bool value = true)
    {
        if (buttonHandler == null || isStartAnimation) return;

        isCompleted = false;
        UIPanelAnimation?.EnablePanel();
        StartAnimation();
        buttonHandler.OnButtonDown += OnClickPrimaryButtonDown;
    }

    private void Start()
    {
        buttonHandler = targetHand.GetComponents<HandButtonHandler>()
            .FirstOrDefault(x => x.GetHandlerButton == listenButtonDown);
        UIPanelAnimation?.DisablePanel();
    }

    private void StartAnimation()
    {
        if (isCompleted) return;
        isStartAnimation = true;

        wave.transform.localScale = Vector3.one;
        wave.color = new Color(1, 1, 1, 0);
        animation = DOVirtual.DelayedCall(.5f, () =>
        {
            animation = buttonDownImage.transform.DOLocalMoveX(targetLocalMove, .23f).OnComplete(() =>
            {
                wave.color = Color.white;
                wave.DOFade(0, .25f);
                wave.transform.DOScale(1.5f, .25f);
                animation = DOVirtual.DelayedCall(.25f,
                    () =>
                    {
                        animation = buttonDownImage.transform.DOLocalMoveX(defaultLocalMove, .23f)
                            .OnComplete(StartAnimation);
                    });
            });
        });
    }
}
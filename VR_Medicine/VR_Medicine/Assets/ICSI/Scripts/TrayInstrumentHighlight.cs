using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autohand;
using Common.DataManager;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrayInstrumentHighlight : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private InstrumentDataDescription[] data;
    [SerializeField] private float direction;
    [SerializeField] private float speedMove = 1;
    [SerializeField] private TMP_Text description;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Camera camera;
    [SerializeField] private Color fadeColor;
    [SerializeField] private Color enableColor;
    [SerializeField] private ContentSizeFitter[] contents;
    private Grabbable lastGrabbable;

    [Button]
    public void FindContentSizeFilter()
    {
        contents = GetComponentsInChildren<ContentSizeFitter>(true);
    }
    
    private void Start()
    {
        lineRenderer.positionCount = 2;
    }

    public void OnHighlightGrabbable(Grabbable grabbable)
    {
        if (!SimulationManagerDataContainer.IsLightTrainingMode) return;
        if (grabbable.IsHeld()) return;
        
        var lastGrabbableIsNull = lastGrabbable == null;
        lastGrabbable = grabbable;

        //var targetPosition = lastGrabbable.transform.position + (camera.transform.forward + camera.transform.up) * direction;
        var targetPosition = lastGrabbable.transform.position +
                             Quaternion.Euler(Vector3.up * camera.transform.rotation.eulerAngles.y) *
                             ((Vector3.right + Vector3.up) * direction);

        if (lastGrabbableIsNull)
            parent.position = targetPosition;

        var currentDescription = data.Where(x => x.Grabbable == grabbable).ToArray()[0];

        description.text = currentDescription.Description;
        RefreshContentSize();

        lineRenderer.enabled = true;
        var disable2Color = new Color2(fadeColor, fadeColor);
        lineRenderer.DOColor(disable2Color, disable2Color, 0).OnComplete(() =>
            lineRenderer.DOColor(disable2Color, new Color2(enableColor, fadeColor), .5f));
        canvasGroup.DOKill();
        canvasGroup.DOFade(1, 0.5f);
    }
    
    

    private void RefreshContentSize()
    {
        IEnumerator Routine()
        {
            for (var i  = 0; i < 2;i++)
            {
                yield return null;
                foreach (var contentSizeFitter in contents)
                {
                    contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
                    contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                }

                yield return null;

                foreach (var contentSizeFitter in contents)
                {
                    contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                    contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
            }
        }

        StartCoroutine(Routine());
    }

    public void OnExitHighlightGrabbable(Grabbable grabbable)
    {
        if (lastGrabbable != grabbable) return;

        lineRenderer.enabled = false;
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => lastGrabbable = null);

        var disable2Color = new Color2(fadeColor, fadeColor);
        var enable2Color = new Color2(enableColor, fadeColor);
        lineRenderer.DOColor(enable2Color, enable2Color, 0).OnComplete(() =>
            lineRenderer.DOColor(enable2Color, disable2Color, .5f));
    }

    private void Update()
    {
        if (lastGrabbable == null) return;

        lineRenderer.SetPosition(0, parent.position);
        lineRenderer.SetPosition(1, lastGrabbable.transform.position);

        //var targetPosition = lastGrabbable.transform.position + (camera.transform.forward + camera.transform.up) * direction;
        var targetPosition = lastGrabbable.transform.position +
                             Quaternion.Euler(Vector3.up * camera.transform.rotation.eulerAngles.y) *
                             ((Vector3.right + Vector3.up) * direction);
        parent.position = Vector3.MoveTowards(parent.position, targetPosition, Time.deltaTime * speedMove);
    }
}

[Serializable]
public class InstrumentDataDescription
{
    public string Description;
    public Grabbable Grabbable;
}
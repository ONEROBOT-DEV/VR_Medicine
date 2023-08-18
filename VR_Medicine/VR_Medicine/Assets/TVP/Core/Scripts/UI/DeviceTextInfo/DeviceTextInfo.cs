using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using NaughtyAttributes;
using Common.DataManager;
using UnityEngine.UI;

public class DeviceTextInfo : MonoBehaviour
{
    [SerializeField] string _textToSet;
    [SerializeField] RectTransform _image;
    [SerializeField] Grabbable _device;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Transform _camera;
    [SerializeField] CanvasGroup _canvasPivot;
    [SerializeField] private bool _isOkayDistance;
    [Space, SerializeField] private ContentSizeFitter sizeFilter;
    [SerializeField] private RectTransform imageBackground;
    private Coroutine resizeText;

    public float ViewDistance = 1.5f;
    public bool IsShowing { get; private set; }

    public bool IsOkayDistance
    {
        get => _isOkayDistance;
        set
        {
            _isOkayDistance = value;

            if (_isOkayDistance)
                _canvasPivot.alpha = 1;
            else
                _canvasPivot.alpha = 0;
        }
    }

    public string TextToSet
    {
        get => _textToSet;
        set => _textToSet = value;
    }

    private void OnEnable()
    {
        if (resizeText != null) StopCoroutine(resizeText);

        resizeText = StartCoroutine(ResizeView());
    }

    private void OnDisable()
    {
        if (resizeText != null) StopCoroutine(resizeText);
    }

    [Sirenix.OdinInspector.Button]
    public void SetText()
    {
        _text.text = TextToSet;

        resizeText = StartCoroutine(ResizeView());
    }

    IEnumerator ResizeView()
    {
        sizeFilter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        yield return null;
        sizeFilter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        yield return null;
        var nexSizeDeltaX = sizeFilter.GetComponent<RectTransform>().rect.width;
        var newSizeDeltaFromSizeFilter = new Vector2(nexSizeDeltaX + 300, imageBackground.sizeDelta.y);
        imageBackground.sizeDelta = newSizeDeltaFromSizeFilter;
        resizeText = null;
    }

    private void Awake()
    {
        _device = GetComponentInParent<Grabbable>();
        SetText();
        _camera = Camera.main.transform;
        if (_device != null)
        {
            _device.OnGrabEvent += HideInfo;
            _device.OnReleaseEvent += DisableHint;
            _device.OnHighlightEvent += ShowInfo;
            _device.OnHighlightEvent += HiglightInfo;
            _device.OnUnhighlightEvent += MakeWhite;
        }
    }

    private void DisableHint(Hand hand, Grabbable grabbable)
    {
        gameObject.SetActive(false);
        _text.gameObject.SetActive(false);

        if (_device != null)
        {
            _device.OnGrabEvent -= HideInfo;
            _device.OnReleaseEvent -= DisableHint;
            _device.OnHighlightEvent -= ShowInfo;
            _device.OnHighlightEvent -= HiglightInfo;
            _device.OnUnhighlightEvent -= MakeWhite;
        }
    }

    private void HiglightInfo(Hand hand, Grabbable grabbable)
    {
        _text.color = Color.red;
    }

    private void Start()
    {
        _camera = Camera.main.transform;
        IsShowing = true;
        if (SimulationManagerDataContainer.IsTestMode) _canvasPivot.gameObject.SetActive(false);
    }

    public void HideInfo(Hand hand, Grabbable grabbable)
    {
        if (SimulationManagerDataContainer.IsTestMode) return;


        _text.color = Color.white;
        IsShowing = false;
        _canvasPivot.gameObject.SetActive(false);
    }

    public void MakeWhite(Hand hand, Grabbable grabbable)
    {
        _text.color = Color.black;
    }

    public void ShowInfo(Hand hand, Grabbable grabbable)
    {
        if (SimulationManagerDataContainer.IsTestMode) return;
        _text.color = Color.white;
        IsShowing = true && IsOkayDistance;
        _canvasPivot.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (SimulationManagerDataContainer.IsTestMode) return;

        if (IsShowing)
        {
            IsOkayDistance = Vector3.Distance(transform.position, _camera.position) < ViewDistance;

            if (!IsOkayDistance)
                return;

            _canvasPivot.transform.LookAt(_camera.forward * 100);
        }
    }
}
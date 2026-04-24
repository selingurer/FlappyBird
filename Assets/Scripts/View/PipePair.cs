using System;
using DefaultNamespace;
using Event;
using Event.Boosters;
using Event.GameFlow;
using Service;
using UnityEngine;
using VContainer;

public class PipePair : MonoBehaviour
{
    [SerializeField] private Transform _top;
    [SerializeField] private Transform _bottom;
    [SerializeField] private Transform _middle;
    [SerializeField] private Transform _topMouth;
    [SerializeField] private Transform _bottomMouth;
    [SerializeField] private PipePassTrigger _pipePassTrigger;
    [SerializeField] private BoxCollider2D _middleCol;


    [SerializeField] private PipeMover _mover;
    [Inject] IScoreService _scoreService;
    public int Index { get; set; }
    public Transform PipePassTriggerTransform => _pipePassTrigger.transform;

    private void OnEnable()
    {
        _pipePassTrigger.OnPassed += OnPassed;
    }

    private void OnDisable()
    {
        _pipePassTrigger.OnPassed -= OnPassed;
    }

    private void OnPassed()
    {
        _scoreService.AddScore();
        Debug.Log(Index);
        EventBus<OnPipePassTrigger>.Publish(new OnPipePassTrigger
        {
            Index = Index,
        });
    }

    public void ApplyLayout(PipeLayout layout)
    {
        float camHeight = Camera.main.orthographicSize;
        float camTop = camHeight;
        float camBottom = -camHeight;

        float groundHeight = 1f;
        float minPipeHeight = 1f;
        float halfGap = layout.GapSize * 0.5f;


        float availableBottom = layout.CenterY - halfGap - (camBottom + groundHeight);
        float availableTop = camTop - (layout.CenterY + halfGap);


        float bottomHeight = Mathf.Max(availableBottom, minPipeHeight);
        float topHeight = Mathf.Max(availableTop, minPipeHeight);


        _bottom.localPosition = new Vector3(0, camBottom + groundHeight, 0);
        _bottom.localScale = new Vector3(1, bottomHeight, 1);
        _bottomMouth.position = _bottom.position + new Vector3(0, bottomHeight, 0);

        _top.localPosition = new Vector3(0, camTop, 0);
        _top.localScale = new Vector3(1, -topHeight, 1);
        _topMouth.position = _top.position + new Vector3(0, -topHeight, 0);

        _middle.localPosition = new Vector3(0, layout.CenterY, 0);
        _middleCol.size = new Vector2(_middleCol.size.x, layout.GapSize);
    }

    public void StartMove()
    {
        _mover.Move(OnMoveCompleted);
    }

    private void OnMoveCompleted()
    {
        EventBus<PipePairMoveEndEvent>
            .Publish(new PipePairMoveEndEvent(this));
    }
}


public struct PipeLayout
{
    public float GapSize;
    public float CenterY { get; set; }
}
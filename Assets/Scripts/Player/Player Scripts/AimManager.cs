using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AimManager : MonoBehaviour
{
    [Header("References")]
    public Transform childTransform;
    public GameObject swipeImage;
    public LineRenderer aimRenderer;
    public float tweenDuration;

    private const int aimMaxDistance = 100;
    private const int rightAngle = 90;
    private const int maxReflection = 2;
    private const int swipePosX = 350;
    private const string boundaryLayer = "Boundary";
    private const string bricksLayer = "Bricks";

    private int countlaser;
    private Sequence tweenSeq;
    private float currentAngle;
    private Vector3 childPosition;
    private Vector3 fingerPosition;
    private Vector3 lineDirection;
    //private Vector2 swipeCenterPos;

    private void Awake()
    {
        //aimRenderer.enabled = false;
        SwipeImageTween();
    }

    private void Update()
    {
        if (PlayerManager.playerInstance.touchCheck)
        {
            tweenSeq.Kill();
            swipeImage.SetActive(false);
            //aimRenderer.enabled = true;
            SetLineRendererPosition();
            LineRendererReflection();
        }
        else
        {
            //aimRenderer.enabled = false;
            Debug.Log($"touchCheck : {PlayerManager.playerInstance.touchCheck}");
        }

    }

    private void SwipeImageTween()
    {
        tweenSeq = DOTween.Sequence();
        tweenSeq.Append(swipeImage.GetComponent<RectTransform>().DOAnchorPosX(-swipePosX, tweenDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo));
    }

    private void SetPostionCount()
    {
        countlaser++;
        aimRenderer.positionCount = countlaser;
    }

    private void SetLineRendererPosition()
    {
        countlaser = 1;
        childPosition = childTransform.position;
        fingerPosition = Camera.main.ScreenToWorldPoint(PlayerManager.playerInstance.fingerTouch.position);
        fingerPosition.z = childPosition.z;
        lineDirection = fingerPosition - childPosition;
        aimRenderer.SetPosition(0, childPosition);
        currentAngle = Mathf.Atan2(lineDirection.y, lineDirection.x) * Mathf.Rad2Deg - rightAngle;//Calculating the rotation angle
        childTransform.transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
      
        
    }
   private void LineRendererReflection()
    {
        for (int i = 0; i < maxReflection; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(childPosition, lineDirection, aimMaxDistance,
                1 << LayerMask.NameToLayer(boundaryLayer) | 1 << LayerMask.NameToLayer(bricksLayer));
            if (hit.collider != null)
            {
                SetPostionCount();
                lineDirection = Vector3.Reflect(lineDirection, hit.normal);
                childPosition = (Vector2)lineDirection.normalized + hit.point;
                aimRenderer.SetPosition(countlaser - 1, hit.point);
            }
            else
            {
                SetPostionCount();
                aimRenderer.SetPosition(countlaser - 1, childPosition + (lineDirection.normalized * aimMaxDistance));
            }
        }
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputController : Singleton<InputController>
{
    [SerializeField] private float minimunDistance = .2f;
    [SerializeField] private float maximumTime = 1f;
    [SerializeField] private float cameraSpeed = 4f;

    private TouchControls inputs;

    private float swipeStartTime;
    private float swipeEndTime;
    private Vector2 swipeStartPosition;
    private Vector2 swipeEndPosition;

    private Transform cameraTransform;
    private Coroutine zoomCoroutine;

    #region Events
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;
    public delegate void SwipeEvent(Vector2 directionValue, SwipeDirection directionName);
    public event SwipeEvent OnSwipe;
    #endregion

    #region Starter Functions
    private void Awake()
    {
        inputs = new TouchControls();
        cameraTransform = Camera.main.transform;
        Debug.LogWarning($"Input Controller awakened");
    }

    private void OnEnable()
    {
        inputs.Enable();
        Debug.LogWarning($"Input Controller enabled");
    }

    private void OnDisable()
    {
        inputs.Disable();
        Debug.LogWarning($"Input Controller disabled");

    }

    private void Start()
    {
        inputs.Touch.PrimaryTouchPress.started += context => StartTouch(context);
        inputs.Touch.PrimaryTouchPress.canceled += context => EndTouch(context);
        inputs.Touch.SecondaryTouchPress.started += context => StartZoom(context);
        inputs.Touch.SecondaryTouchPress.canceled += context => EndZoom(context);
        Debug.LogWarning($"Input Controller Started");
    }
    #endregion

    public void StartTouch(InputAction.CallbackContext context)
    {
        Vector2 currentTouchPosition = inputs.Touch.PrimaryTouchPosition.ReadValue<Vector2>();
        swipeStartPosition = currentTouchPosition;
        swipeStartTime = Time.time;

        Debug.LogWarning($"Touch started: {currentTouchPosition}");

        OnStartTouch?.Invoke(currentTouchPosition, (float)context.startTime);
    }

    public void EndTouch(InputAction.CallbackContext context)
    {
        Vector2 currentTouchPosition = inputs.Touch.PrimaryTouchPosition.ReadValue<Vector2>();
        swipeEndPosition = currentTouchPosition;
        swipeEndTime = Time.time;

        Debug.LogWarning($"Touch ended: {currentTouchPosition}");

        OnEndTouch?.Invoke(currentTouchPosition, (float)context.time);
        SwipeDetection();
    }

    private void SwipeDetection()
    {
        if (Vector2.Distance(swipeStartPosition, swipeEndPosition) >= minimunDistance && (swipeEndTime - swipeStartTime) <= maximumTime)
        {
            Debug.DrawLine(swipeStartPosition, swipeEndPosition, Color.red, 5f);

            Vector3 direction = swipeEndPosition - swipeStartPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;

            SwipeDirection directionName = GetDirection(direction2D);
            
            Debug.LogWarning($"Swipe Detected and it's going to {directionName}");
            OnSwipe?.Invoke(direction2D, directionName);
        }
    }

    private void StartZoom(InputAction.CallbackContext context)
    {
        zoomCoroutine = StartCoroutine(ZoomDetection());
    }

    private void EndZoom(InputAction.CallbackContext context)
    {
        StopCoroutine(zoomCoroutine);
    }

    private IEnumerator ZoomDetection()
    {
        float previousDistance = 0f;
        float distance = 0f;
        while (true)
        {
            distance = Vector2.Distance(inputs.Touch.PrimaryTouchPosition.ReadValue<Vector2>(), inputs.Touch.SecondaryTouchPosition.ReadValue<Vector2>());
            bool doZoom = false;
            Vector3 targetPosition = cameraTransform.position;

            if (distance > previousDistance)
            {
                //Zoom Out
                targetPosition.z -= 1f;
                doZoom = true;
                Debug.LogWarning("Zoom out");
            }
            else if (distance < previousDistance)
            {
                //Zoom In
                targetPosition.z += 1f;
                doZoom = true;
                Debug.LogWarning("Zoom In");
            }

            if (doZoom)
            {
                cameraTransform.position = Vector3.Slerp(cameraTransform.position, targetPosition, Time.deltaTime * cameraSpeed);
            }
            previousDistance = distance;
            yield return null;
        }
    }

    private SwipeDirection GetDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            if (direction.y > 0)
            {
                return SwipeDirection.UpRight;
            }
            else if (direction.y < 0)
            {
                return SwipeDirection.DownRight;
            }
            else
            {
                return SwipeDirection.Right;
            }
        }
        else if (direction.x < 0)
        {
            if (direction.y > 0)
            {
                return SwipeDirection.UpLeft;
            }
            else if (direction.y < 0)
            {
                return SwipeDirection.DownLeft;
            }
            else
            {
                return SwipeDirection.Left;
            }
        }

        if (direction.y < 0)
        {
            return SwipeDirection.Down;
        }
        else if (direction.y > 0) 
        {
            return SwipeDirection.Up;
        }

        return SwipeDirection.None;
    }
}

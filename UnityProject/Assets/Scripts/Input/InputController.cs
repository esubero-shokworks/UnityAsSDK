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

    private bool isHoldingTouch;
    private float swipeStartTime;
    private float swipeEndTime;
    private Vector2 swipeStartPosition;
    private Vector2 swipeEndPosition;

    private Transform cameraTransform;
    private Coroutine zoomCoroutine;

    #region Events
    public delegate void StartTouchEvent(Vector2 position);
    public event StartTouchEvent OnStartTouch;
    public delegate void HoldingTouchEvent(Vector2 position);
    public event HoldingTouchEvent OnHoldingTouch;
    public delegate void EndTouchEvent(Vector2 position);
    public event EndTouchEvent OnEndTouch;
    public delegate void SwipeEvent(Vector2 directionValue, SwipeDirection directionName);
    public event SwipeEvent OnSwipe;
    #endregion

    #region Starter Functions
    private void Awake()
    {
        inputs = new TouchControls();
        cameraTransform = Camera.main.transform;
        Debug.Log($"Input Controller awakened");
    }

    private void OnEnable()
    {
        inputs.Enable();
        Debug.Log($"Input Controller enabled");
    }

    private void OnDisable()
    {
        inputs.Disable();
        Debug.Log($"Input Controller disabled");
    }

    private void Start()
    {
        inputs.Touch.PrimaryTouchPress.started += context => StartTouch(context);
        inputs.Touch.PrimaryTouchPress.canceled += context => EndTouch(context);
        inputs.Touch.SecondaryTouchPress.started += context => StartZoom(context);
        inputs.Touch.SecondaryTouchPress.canceled += context => EndZoom(context);

        Debug.Log($"Input Controller Started");
    }

    private void Update()
    {
        HoldingTouch();
    }

    #endregion

    #region Simple Touch

    public void StartTouch(InputAction.CallbackContext context)
    {
        try
        {
            Vector2 currentTouchPosition = inputs.Touch.PrimaryTouchPosition.ReadValue<Vector2>();
            swipeStartPosition = currentTouchPosition;
            swipeStartTime = Time.time;

            Debug.Log($"Touch started: {currentTouchPosition}");
            isHoldingTouch = true;

            OnStartTouch?.Invoke(currentTouchPosition);
        }
        catch (Exception thrownException)
        {
            ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"InputController: {thrownException.Message}");
            throw;
        }
    }

    public void EndTouch(InputAction.CallbackContext context)
    {
        try
        {
            Vector2 currentTouchPosition = inputs.Touch.PrimaryTouchPosition.ReadValue<Vector2>();
            swipeEndPosition = currentTouchPosition;
            swipeEndTime = Time.time;

            Debug.Log($"Touch ended: {currentTouchPosition}");
            isHoldingTouch = false;

            OnEndTouch?.Invoke(currentTouchPosition);
            SwipeDetection();
        }
        catch (Exception thrownException)
        {
            ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"InputController: {thrownException.Message}");
            throw;
        }
    }

    #endregion

    #region Holding Touch
    private void HoldingTouch()
    {
        if (isHoldingTouch)
        {
            OnHoldingTouch?.Invoke(inputs.Touch.PrimaryTouchPosition.ReadValue<Vector2>());
        }
    }

    #endregion

    #region Swipe

    private void SwipeDetection()
    {
        try
        {
            if (Vector2.Distance(swipeStartPosition, swipeEndPosition) >= minimunDistance && (swipeEndTime - swipeStartTime) <= maximumTime)
            {
                Debug.DrawLine(swipeStartPosition, swipeEndPosition, Color.red, 5f);

                Vector3 direction = swipeEndPosition - swipeStartPosition;
                Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;

                SwipeDirection directionName = GetDirection(direction2D);

                Debug.Log($"Swipe Detected and it's going to {directionName}");
                OnSwipe?.Invoke(direction2D, directionName);
            }
        }
        catch (Exception thrownException)
        {
            ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"InputController: {thrownException.Message}");
            throw;
        }
    }

    #endregion

    #region Zoom
    
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
        while (true)
        {
            try
            {
                float distance = Vector2.Distance(inputs.Touch.PrimaryTouchPosition.ReadValue<Vector2>(), inputs.Touch.SecondaryTouchPosition.ReadValue<Vector2>());
                bool doZoom = false;
                Vector3 targetPosition = cameraTransform.position;

                if (distance > previousDistance)
                {
                    //Zoom Out
                    targetPosition.z -= 1f;
                    doZoom = true;
                    Debug.Log("Zoom out");
                }
                else if (distance < previousDistance)
                {
                    //Zoom In
                    targetPosition.z += 1f;
                    doZoom = true;
                    Debug.Log("Zoom In");
                }

                if (doZoom)
                {
                    cameraTransform.position = Vector3.Slerp(cameraTransform.position, targetPosition, Time.deltaTime * cameraSpeed);
                }
                previousDistance = distance;
            }
            catch (Exception thrownException)
            {
                ServiceLocator.Instance.GetService<ICallbackManagerService>().SendCallbackMessage($"InputController: {thrownException.Message}");
                throw;
            }
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
    
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;
    
    [SerializeField] private float _maxFreezeTime = 0.1f;
    [SerializeField] private float _maxSlowMotionTime = 1f;
    [SerializeField] private float _slowMotionRecoveryTime = 0.04f;
    [SerializeField] private float _slowMotionBuildUpTime = 0.04f;
    private float _currentFreezeTime;
    private float _targetTimeScale;
    private float _currentTimeScale;
    private bool _slowMotion;
    private float _currentSlowMotionTime;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        _currentTimeScale = 1.0f;
        _targetTimeScale = 1.0f;
    }
    public static void Freeze(float time)
    {
        _instance._currentFreezeTime += time;
    }
    public static void SlowMotion(float time, float timeScale)
    {
        _instance._currentSlowMotionTime = time;
        _instance._targetTimeScale = timeScale;
        _instance._slowMotion = true;
    }
    private void Update()
    {
          //Input for testing
//        if(Input.GetKeyDown(KeyCode.Alpha1))
//            Freeze(0.05f);
//        if(Input.GetKeyDown(KeyCode.Alpha2))
//            SlowMotion(1.0f, .3f);

        float deltaTimeScale = 0.0f;
        _currentTimeScale = Mathf.SmoothDamp(_currentTimeScale, _targetTimeScale, ref deltaTimeScale, _slowMotion ? _slowMotionBuildUpTime : _slowMotionRecoveryTime, float.MaxValue, Time.unscaledDeltaTime);
        Time.timeScale = _currentTimeScale;
        UpdateSlowMotion();
        UpdateFreeze();
    }
    private void UpdateFreeze()
    {
        if (_currentFreezeTime <= 0.0f) 
            return;
        _currentFreezeTime -= Time.unscaledDeltaTime;
        _currentFreezeTime = Mathf.Clamp(_currentFreezeTime, 0.0f, _maxFreezeTime);
        Time.timeScale = 0.0f;   
    }
    private void UpdateSlowMotion()
    {
        _currentSlowMotionTime -= Time.unscaledDeltaTime;
        _currentSlowMotionTime = Mathf.Clamp(_currentSlowMotionTime, 0.0f, _maxSlowMotionTime);
        if (_currentSlowMotionTime > 0.0f)
            return;
        _slowMotion = false;
        _targetTimeScale = 1.0f;
    }

}


using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public static ClockManager Instance;

    [SerializeField] private float startTime;
    [SerializeField, Range(-10f, 10f)] private float scaleTime = 1;

    private float _scaleDefault;
    private float _time;
    private string _timeText;

    private void Awake()
    {
        if (ClockManager.Instance == null)
        {
            ClockManager.Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _scaleDefault = scaleTime;
        _time = startTime;
        
        UpdateClock();
    }

    private void Update()
    {
        _time += Time.deltaTime * scaleTime;

        UpdateClock();
        GetTimeInMinutes();
    }

    private void UpdateClock()
    {
        var minutes = _time / 60;
        var seconds = _time % 60;

        if (_time < 0) _time = 0;

        _timeText = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void ResetDefaultScale()
    {
        scaleTime = _scaleDefault;
    }

    public void SetScaleTime(float scale)
    {
        scale = Mathf.Clamp(scale, -10f, 10f);
        scaleTime = scale;
    }

    public float GetTimeInSeconds()
    {
        return _time;
    }
    
    public float GetTimeInMinutes()
    {
        var minutes = _time / 60;
        Debug.Log($"sin floor = {minutes}");
        minutes = (float)Mathf.Floor(minutes);
        Debug.Log($"con floor = {minutes}");
        return minutes;
    }
}
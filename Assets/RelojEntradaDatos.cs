using UnityEngine;
using UnityEngine.UI;

public class RelojEntradaDatos : MonoBehaviour
{
    [SerializeField] private Text timeText = null;
    [SerializeField] private Text currentPeriodText = null;

    private int currentPeriod = 1;
    private float time;
    [HideInInspector] public bool paused = true;

    private Animator animator;
    private bool open;

    private void Start()
    {
        animator = GetComponent<Animator>();
        open = true;
    }

    private void Update()
    {
        if (!paused)
            time += Time.deltaTime;
        SetTimeText();
    }

    public void Initiate()
    {
        currentPeriod = 1;
        currentPeriodText.text = currentPeriod + "°";
        time = 0f;
        paused = true;
    }

    public void TogglePause()
    {
        paused = !paused;
    }

    public void NextPeriod()
    {
        currentPeriod++;
        currentPeriodText.text = currentPeriod + "°";
        time = 0f;
        paused = true;
    }

    private void SetTimeText()
    {
        int sec = (int)time%60;
        int min = (int)Mathf.Floor(time/60f);

        if (min < 10)
            timeText.text = "0";
        timeText.text += min + " : ";

        if (sec < 10)
            timeText.text += "0";
        timeText.text += sec;
    }

    public int GetCurrentPeriod()
    {
        return currentPeriod;
    }

    public float GetCurrentTime()
    {
        return time;
    }

    public void ToggleVerReloj()
    {
        open = !open;
        animator.SetBool("open", open);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    public int CurrentWalkCount;
    public int CurrentRescueCount;

    public int MaxWalkCount;
    public int TotalWalkCount;

    public int MaxRescueCount;
    public int TotalRescueCount;

    public int PlayCount;
    public float PlayTime;

    private void Update() {
        if(!Application.isPlaying) return;

        CurrentWalkCount = Status.Instance.CurrentWalkCount;
        CurrentRescueCount = Status.Instance.CurrentRescueCount;

        MaxWalkCount = Status.Instance.MaxWalkCount;
        TotalWalkCount = Status.Instance.TotalWalkCount;

        MaxRescueCount = Status.Instance.MaxRescueCount;
        TotalRescueCount = Status.Instance.TotalRescueCount;

        PlayCount = Status.Instance.PlayCount;
        PlayTime = Status.Instance.PlayTime;
    }
}

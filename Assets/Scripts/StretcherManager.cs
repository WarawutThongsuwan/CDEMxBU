using UnityEngine;
using System.Collections.Generic;

public class StretcherManager : MonoBehaviour
{
    public static StretcherManager Instance;

    public List<StretcherController> stretchers = new List<StretcherController>();

    private void Start()
    {
        
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public StretcherController GetAvailableStretcher()
    {
        foreach (var stretcher in stretchers)
        {
            if (!stretcher.IsBusy())
                return stretcher;
        }

        return null; // ไม่มีเปลว่าง
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    private IStatusService _status;

    private void Awake()
    {
        _status = ServiceLocator.Instance.GetService<IStatusService>();
    }

    private void Start()
    {
        StartCoroutine(_status.RecoverMP());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public GameObject player;
    public Transform startPoint;

    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        player.transform.position = startPoint.position;
        yield return null;
        progress = 1f;
    }
}

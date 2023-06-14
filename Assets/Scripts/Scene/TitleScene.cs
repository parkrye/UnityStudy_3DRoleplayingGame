using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public void OnStartButton()
    {
        GameManager.Scene.LoadScene("MainScene");
    }

    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        yield return new WaitForSeconds(0.5f);
        progress = 1f;
    }
}

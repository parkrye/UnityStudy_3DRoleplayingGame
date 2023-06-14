using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    Animator animator;
    Slider slider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        slider = GetComponentInChildren<Slider>();
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    public void SetProgress(float progress)
    {
        slider.value = progress;
    }
}

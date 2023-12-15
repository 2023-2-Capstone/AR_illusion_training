using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AnimatorController : MonoBehaviour
{
    public DataSystem dataSystem;
    public Scrollbar EffectWeightScrollbar;
    // Start is called before the first frame update
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 어떤 조건에서 IsDead를 true로 설정하면서 애니메이션 전환을 유도
        if (dataSystem.GetReps()==EffectWeightScrollbar.value*47+3)
        {
            animator.SetBool("IsDead", true);
        }


    }
}

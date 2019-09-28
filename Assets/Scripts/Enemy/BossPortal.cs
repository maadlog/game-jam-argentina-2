using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortal : MonoBehaviour
{
    public void SpawnBoss()
    {
        boss?.PortalActive();
    }

    public void PortalClosed()
    {
        this.transform.localScale = Vector2.zero;
        this.GetComponent<Animator>().Play("Invisible");
        boss?.PortalClosed();
    }

    public void Appear(Transform at)
    {
        this.transform.position = at.position;
        this.GetComponent<Animator>().Play("Appear");
    }

    private Boss boss;
    public void Handle(Boss boss)
    {
        this.boss = boss;
    }
}

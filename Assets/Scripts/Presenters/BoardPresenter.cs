using VecrticalFighter.Model;
using UnityEngine;
using System;

public class BoardPresenter : Presenter
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyBoardPart"))
        {
            Model.Destroy();
        }
    }

    protected override void OnDestroying()
    {
        gameObject.SetActive(false);
    }
}

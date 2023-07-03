using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
  protected virtual void OnPicked(Collider2D parCollider2D)
  {
    // Тут можно сделать например звук, эффект сбора
  }

  //=======================================

  private void OnCollisionEnter2D(Collision2D collision)
  {
    var tank = collision.collider.GetComponent<Health>();

    if (tank)
      OnPicked(collision.collider);
  }

  //=======================================
}
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public static bool imortal;
    public float tempoIP;
    private Renderer mainRenderer;

    private void Start()
  {
    mainRenderer = GetComponent<SpriteRenderer>();
  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
    if (!imortal)
    {
      if (collision.gameObject.layer == 13)
      {
        StartCoroutine(PiscarDano());
        imortal = true;
        Invoke("ResetImortal", tempoIP);
      }
    }
  }

  IEnumerator PiscarDano()
  {
    for (int i = 0; i < tempoIP; i++)
    {
      mainRenderer.enabled = true;
      yield return new WaitForSeconds(0.1f);
      mainRenderer.enabled = false;
      yield return new WaitForSeconds(0.1f);
    }
    mainRenderer.enabled = true;
  }

  void ResetImortal()
  {
    imortal = false;
  }
}
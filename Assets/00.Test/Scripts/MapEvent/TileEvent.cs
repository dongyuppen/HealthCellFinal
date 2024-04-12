using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileEvent : MonoBehaviour
{
    public Tilemap BreakTile;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            if(collision.contacts[0].normal.y < 0.5f)
            {
                Vector3 breakposision = Vector3.zero;
                Invoke("OnBreakTile", 1f);
            }
        }

        //void OnBreakTile()
        //{
            //BreakTile.SetTile(BreakTile.WorldToCell(breakposision), null);
        //}
    }
}

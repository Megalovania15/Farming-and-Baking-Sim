using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/Custom Tiles")]
public class AdvancedRuleTile : RuleTile<AdvancedRuleTile.Neighbor> 
{
    public TileBase[] groundTilesToConnect;
    public TileBase[] waterTilesToConnect;
    public bool checkSelf;
    public bool isConnectable;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Any = 3;
        public const int Ground = 4;
        public const int Water = 5;
        public const int Nothing = 6;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.This: return IsThis(tile);
            case Neighbor.NotThis: return IsNotThis(tile);
            case Neighbor.Any: return IsAny(tile);
            case Neighbor.Ground: return IsGroundTile(tile);
            case Neighbor.Water: return IsWaterTile(tile);
            case Neighbor.Nothing: return IsNothing(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }

    //check whether the neighbouring tile is a connectable one otherwise check whether
    //it is in the array and connect it to this tile.
    private bool IsThis(TileBase tile)
    {
        /*if (!isConnectable)
        {
            return tile == this;
        }*/
        return groundTilesToConnect.Contains(tile) || tile == this;
    }
        
    private bool IsNotThis(TileBase tile) => tile != this;

    private bool IsAny(TileBase tile)
    {
        if (checkSelf)
        {
            return tile != null;
        }
        return tile != null && tile != this;
    } 

    private bool IsGroundTile(TileBase tile) => groundTilesToConnect.Contains(tile);
    private bool IsWaterTile(TileBase tile) => waterTilesToConnect.Contains(tile);

    private bool IsNothing(TileBase tile) => tile == null;
}
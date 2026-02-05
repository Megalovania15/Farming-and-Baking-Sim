using UnityEngine;

public class Grid
{
    private int _width;
    private int _height;
    private int[,] gridArray;

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;

        gridArray = new int[width, height];

        Debug.Log($"Grid created of width {width}, and height {height}.");
    }
}

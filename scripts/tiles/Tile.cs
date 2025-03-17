using Godot;
using System;

public class Tile : Node2D
{
	public Vector2 atlasCoord = Vector2.Zero;
	public string name = "Unknown";
	
	public Vector2 GetAtlasCoord()
	{
		return atlasCoord;
	}
	
	public string GetName() {
		return name;
	}
}

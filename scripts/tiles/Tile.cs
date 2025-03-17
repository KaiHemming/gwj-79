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
	
	// Returns atlas coordinate for new tile given input tile
	// e.g., If this tile is water, and param atlasCord is dirt, return grass
	public virtual Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		return atlasCoord;
	}
}

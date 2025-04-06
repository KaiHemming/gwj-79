using Godot;
using System;

public class Tile : Control
{
	public Vector2 atlasCoord;
	public string name = "Unknown";
	public int score = 1;
	public int discoveryAddition = 5;
	public String discoveryTitle = "You discovered a new tile!";
	public string discoveryDescription = "5 more have been added to your bag.";
	
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

	protected void AddToScore(int addition) {
		//GetNode<UI>("root/UI").AddScore(addition);
		GetTree().Root.GetNode("Main").GetNode<UI>("UI").AddScore(addition);
	}
}

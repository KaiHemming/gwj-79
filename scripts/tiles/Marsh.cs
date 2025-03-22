using Godot;
using System;

public class Marsh : Tile
{
	public Marsh() {
		name = "Marsh";
		score = 3;
		atlasCoord = new Vector2 (3,0);
		discoveryTitle = "You discovered marsh!";
	}
}

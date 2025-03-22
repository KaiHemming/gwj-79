using Godot;
using System;

public class Marsh : Tile
{
	public Marsh() {
		name = "Marsh";
		score = 3;
		atlasCoord = new Vector2 (3,0);
		discoveryAddition = 0;
		discoveryTitle = "You discovered marsh! +3 points.";
		discoveryDescription = "By placing grass near 2 water.";
	}
}

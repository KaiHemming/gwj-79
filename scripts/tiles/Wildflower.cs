using Godot;
using System;

public class Wildflower : Tile
{
	public Wildflower() {
		name = "Wildflowers";
		score = 4;
		atlasCoord = new Vector2 (5,0);
		discoveryTitle = "You discovered wildflowers!";
		discoveryAddition = 0;
		discoveryDescription = "By placing a bee with grass neighbours, +4 points each.";
	}
}

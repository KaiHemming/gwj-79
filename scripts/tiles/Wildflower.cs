using Godot;
using System;

public class Wildflower : Tile
{
	public Wildflower() {
		name = "Wildflowers";
		score = 4;
		atlasCoord = new Vector2 (5,0);
		discoveryTitle = "You discovered wildflowers!";
	}
}

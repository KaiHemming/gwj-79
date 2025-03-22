using Godot;
using System;

public class Grass : Tile
{
	public Grass() {
		name = "Grass";
		score = 1;
		atlasCoord = new Vector2 (1,0);
		discoveryTitle = "You discovered grass!";
	}
}

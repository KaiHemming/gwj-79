using Godot;
using System;
using System.Collections.Generic;

public class Habitat : Tile
{

	public Habitat() {
		name = "Habitat Token";
		atlasCoord = new Vector2(-1,-1);
		score = 0;
		discoveryTitle = "A new critter has moved in!";
		discoveryDescription = "+1 habitat token.";
		discoveryAddition = 1;
	}
}

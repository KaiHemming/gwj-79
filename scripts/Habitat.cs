using Godot;
using System;
using System.Collections.Generic;

public class Habitat : Tile
{

	public Habitat() {
		name = "Token (???)";
		atlasCoord = new Vector2(-1,-1);
		score = 0;
		discoveryTitle = "A new critter has moved in!";
		discoveryDescription = "2 more habitat tokens have been added to your bag.";
		discoveryAddition = 2;
	}
}

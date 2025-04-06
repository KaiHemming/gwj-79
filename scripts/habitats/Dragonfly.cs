using Godot;
using System;

public class Dragonfly : Habitat
{
	public Dragonfly() {
		atlasCoord = new Vector2(2,0);
		name = "Dragonfly";
        score = 3;
		discoveryAddition = 1;
		discoveryTitle = "A Dragonfly has moved in! +3 points +1 habitat token";
	}
}

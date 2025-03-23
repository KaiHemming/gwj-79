using Godot;
using System;

public class Newt : Habitat
{
	public Newt() {
		atlasCoord = new Vector2(11,0);
		name = "Newt";
        score = 6;
        discoveryAddition = 1;
		discoveryTitle = "A Newt has moved in! +6 points";
        discoveryDescription = "Near grass and marsh. +1 habitat token +2 per neighbouring marsh.";
	}

    public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		if (atlasCoord == new Vector2(3,0)) {
            AddToScore(2);
        }
		return atlasCoord;
	}
}

using Godot;
using System;

public class KingFisher : Habitat
{
	public KingFisher() {
		atlasCoord = new Vector2(10,0);
		name = "King Fisher";
        score = 6;
        discoveryAddition = 0;
		discoveryTitle = "A King Fisher has moved in! +6 points";
        discoveryDescription = "Belongs in woods next to water. +1 per neighbouring waters.";
	}

    public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		if (atlasCoord == new Vector2(2,0)) {
            AddToScore(1);
        }
		return atlasCoord;
	}
}

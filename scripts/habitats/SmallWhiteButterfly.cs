using Godot;
using System;

public class SmallWhiteButterfly : Habitat
{
	public SmallWhiteButterfly() {
		atlasCoord = new Vector2(8,0);
		name = "Small White Butterfly";
        score = 2;
        discoveryAddition = 1;
		discoveryTitle = "A Small White Butterfly has moved in!";
        discoveryDescription = "+2 points +1 habitat token.";
	}

    public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		return atlasCoord;
	}
}

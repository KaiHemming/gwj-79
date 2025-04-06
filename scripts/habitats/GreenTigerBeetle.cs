using Godot;
using System;

public class GreenTigerBeetle : Habitat
{
	public GreenTigerBeetle() {
		atlasCoord = new Vector2(6,0);
		name = "Green Tiger Beetle";
        score = 6;
        discoveryAddition = 3;
		discoveryTitle = "A Green Tiger Beetle has moved in! +6 points";
        discoveryDescription = "A rare find! Must be near butterflies. +3 habitat tokens.";
	}

    public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		return atlasCoord;
	}
}

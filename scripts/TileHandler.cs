using Godot;
using System;
using System.Collections.Generic;

public class TileHandler : Node
{
	// Tile Scenes
	private static PackedScene grassScene = GD.Load<PackedScene>("res://scenes/tiles/Grass.tscn");
	private static PackedScene dirtScene = GD.Load<PackedScene>("res://scenes/tiles/Dirt.tscn");
	private static PackedScene waterScene = GD.Load<PackedScene>("res://scenes/tiles/Water.tscn");

	private static Dictionary<Vector2, PackedScene> tiles = new Dictionary<Vector2, PackedScene>{
		{ new Vector2(0,0), grassScene },
		{ new Vector2(0,3), dirtScene },
		{ new Vector2(6,0), waterScene }
	};

	public static PackedScene GetTileScene(Vector2 atlasCoord) {
		return tiles[atlasCoord];
	}
}

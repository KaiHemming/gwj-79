using Godot;
using System.Collections.Generic;

public class TileHandler
{
	// Tile Scenes
	public static PackedScene grassScene = GD.Load<PackedScene>("res://scenes/tiles/Grass.tscn");
	public static PackedScene dirtScene = GD.Load<PackedScene>("res://scenes/tiles/Dirt.tscn");
	public static PackedScene waterScene = GD.Load<PackedScene>("res://scenes/tiles/Water.tscn");
	public static PackedScene marshScene = GD.Load<PackedScene>("res://scenes/tiles/Marsh.tscn");
	public static PackedScene wildflowerScene = GD.Load<PackedScene>("res://scenes/tiles/Wildflower.tscn");
	public static PackedScene habitatScene = GD.Load<PackedScene>("res://scenes/habitats/Habitat.tscn");

	// TODO: Given Habitat atlasCoord (-1,-1), maybe refactor.
	private static Dictionary<Vector2, PackedScene> tiles = new Dictionary<Vector2, PackedScene>{
		{ new Vector2(1,0), grassScene },
		{ new Vector2(0,0), dirtScene },
		{ new Vector2(2,0), waterScene },
		{ new Vector2(-1,-1), habitatScene},
		{ new Vector2(3,0), marshScene},
		{ new Vector2(5,0), wildflowerScene}
	};

	public static PackedScene GetTileScene(Vector2 atlasCoord) {
		return tiles[atlasCoord];
	}
}

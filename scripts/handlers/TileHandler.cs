using Godot;
using System.Collections.Generic;
using System.Drawing;

public class TileHandler
{
	// Tile Scenes
	public static PackedScene grassScene = GD.Load<PackedScene>("res://scenes/tiles/Grass.tscn");
	public static PackedScene dirtScene = GD.Load<PackedScene>("res://scenes/tiles/Dirt.tscn");
	public static PackedScene waterScene = GD.Load<PackedScene>("res://scenes/tiles/Water.tscn");
	public static PackedScene marshScene = GD.Load<PackedScene>("res://scenes/tiles/Marsh.tscn");
	public static PackedScene wildflowerScene = GD.Load<PackedScene>("res://scenes/tiles/Wildflower.tscn");
	public static PackedScene woodsScene = GD.Load<PackedScene>("res://scenes/tiles/Woods.tscn");
	public static PackedScene habitatScene = GD.Load<PackedScene>("res://scenes/habitats/Habitat.tscn");

	// Tile being placed, Output tile, Land requirements, icon requirements
	public static Dictionary<Vector2, (Tile, LandRequirement[], IconRequirement[])[]> requirementMapping = new Dictionary<Vector2, (Tile, LandRequirement[], IconRequirement[])[]> {
		{((Tile)dirtScene.Instance()).atlasCoord, new (Tile, LandRequirement[], IconRequirement[])[]
			{
				((Tile)grassScene.Instance(),
				new LandRequirement[]{ new LandRequirement(waterScene, 1)},
				new IconRequirement[]{}),
				((Tile)marshScene.Instance(),
				new LandRequirement[]{ new LandRequirement(waterScene, 3)},
				new IconRequirement[]{}),
				((Tile)woodsScene.Instance(),
				new LandRequirement[] {new LandRequirement(waterScene, 1), new LandRequirement(grassScene,2)},
				new IconRequirement[]{})
			}
		},
		{((Tile)grassScene.Instance()).atlasCoord, new (Tile, LandRequirement[], IconRequirement[])[]
			{
				((Tile)marshScene.Instance(),
				new LandRequirement[]{ new LandRequirement(waterScene, 3)},
				new IconRequirement[]{}),
				((Tile)woodsScene.Instance(),
				new LandRequirement[] {new LandRequirement(grassScene, 3)},
				new IconRequirement[]{}),
			}
		},
	};

	// TODO: Given Habitat atlasCoord (-1,-1), maybe refactor.
	private static Dictionary<Vector2, PackedScene> tiles = new Dictionary<Vector2, PackedScene>{
		{ new Vector2(1,0), grassScene },
		{ new Vector2(0,0), dirtScene },
		{ new Vector2(2,0), waterScene },
		{ new Vector2(-1,-1), habitatScene},
		{ new Vector2(3,0), marshScene},
		{ new Vector2(5,0), wildflowerScene},
		{ new Vector2(6,0), woodsScene}
	};

	public static PackedScene GetTileScene(Vector2 atlasCoord) {
		return tiles[atlasCoord];
	}
}

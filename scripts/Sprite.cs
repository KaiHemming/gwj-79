using Godot;
using System;
using System.Collections;

public class Sprite : Godot.Sprite
{
	private TileMap tileMap;
	public Vector2 curTile = Vector2.Zero;
	public RandomNumberGenerator rng = new RandomNumberGenerator();
	private Vector2 availableTileType = new Vector2(1,5);
	
	// Tile Scenes
	private static PackedScene grassScene = GD.Load<PackedScene>("res://scenes/Grass.tscn");
	private static PackedScene dirtScene = GD.Load<PackedScene>("res://scenes/Dirt.tscn");
	
	// Tile collection array
	private PackedScene[] tiles = {
		grassScene,
		dirtScene
	};
	
	// Bag of tiles
	private ArrayList bag = new ArrayList();
	
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		tileMap = GetParent().GetNode("Camera2D").GetNode<TileMap>("TileMap");
		bag.Add(0);
		for (int i = 0; i < 8; i++) {
			bag.Add(1);
		}
		GD.Randomize();
		rng.Randomize();
		GetNextTile();
	}
	public override void _Process(float delta) {
		this.GlobalPosition = GetGlobalMousePosition(); 
	}
	public override void _Input(InputEvent inputEvent) {
		// Check if place tile
		if (inputEvent.IsActionPressed("ui_accept")) {
			var mousePos = tileMap.GetMousePosition();
			var selectedTileType = tileMap.GetCellAutotileCoord((int) mousePos.x, (int) mousePos.y);
			if (selectedTileType == availableTileType) {
				PlaceTile(mousePos);
			}
		}
	}
	// Places a tile at pos
	public void PlaceTile(Vector2 pos) {
		tileMap.SetCellv(pos, 0, false, false, false, curTile);
		GetNextTile();
	}
	// Gets next tile from bag
	public void GetNextTile() {
		int random = rng.RandiRange(0, bag.Count-1);
		PackedScene tileScene = tiles[(int) bag[random]];
		Control CurrentTileTextureControl = GetParent().GetNode("UI").GetNode<Control>("CurrentTileTexture");
		foreach (Node i in CurrentTileTextureControl.GetChildren()) {
			i.QueueFree();
		}
		Tile tile = (Tile) tileScene.Instance();
		CurrentTileTextureControl.AddChild(tile);
		GD.Print(tile.atlasCoord);
		curTile = tile.GetAtlasCoord();
	}
}

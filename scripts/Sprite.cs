using Godot;
using System;
using System.Collections;

public class Sprite : Godot.Sprite
{
	private TileMap tileMap;
	public Tile curTile;
	public RandomNumberGenerator rng = new RandomNumberGenerator();
	
	// Tile Scenes
	private static PackedScene grassScene = GD.Load<PackedScene>("res://scenes/tiles/Grass.tscn");
	private static PackedScene dirtScene = GD.Load<PackedScene>("res://scenes/tiles/Dirt.tscn");
	private static PackedScene waterScene = GD.Load<PackedScene>("res://scenes/tiles/Water.tscn");
	
	// Tile collection array
	private PackedScene[] tiles = {
		grassScene,
		dirtScene,
		waterScene
	};
	
	// Bag of tiles
	private ArrayList bag = new ArrayList();
	
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		tileMap = GetParent().GetNode("Camera2D").GetNode<TileMap>("TileMap");
		
		// Starting bag
		// One Grass
		bag.Add(0);
		// 8 Dirt
		for (int i = 0; i < 8; i++) {
			bag.Add(1);
		}
		// 5 Water
		for (int i = 0; i < 3; i++) {
			bag.Add(2);
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
			if (tileMap.IsAvailable(mousePos)) {
				PlaceTile(mousePos);
			} 
		}
		// Check if zoom
		
	}
	// Places a tile at pos
	public void PlaceTile(Vector2 pos) {
		tileMap.PlaceTile(pos, curTile);
		GetNextTile();
		// Update neighbouring tiles
		
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
		curTile = tile;
	}
}

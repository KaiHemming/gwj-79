using Godot;
using System;
using System.Collections;

public class Sprite : Godot.Sprite
{
	private TileMap tileMap;
	private TileMap iconTileMap;
	public Tile curTile;
	public RandomNumberGenerator rng = new RandomNumberGenerator();
	
	// Tile collection array
	private Vector2[] tiles = {
		new Vector2(0,0),
		new Vector2(0,3),
		new Vector2(6,0),
		new Vector2(-1,-1)
	};
	
	// Bag of tiles
	private ArrayList bag = new ArrayList();
	
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		tileMap = GetParent().GetNode("Camera2D").GetNode<TileMap>("TileMap");
		iconTileMap = GetParent().GetNode("Camera2D").GetNode<TileMap>("IconTileMap");
		
		// Starting bag
		// One Grass and habitat
		bag.Add(0);
		bag. Add(3);
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
			PlaceTile(mousePos);
		}
		// Check if zoom
		
	}
	// Places a tile at pos
	public void PlaceTile(Vector2 pos) {
		if (curTile is Habitat) {
			GD.Print(tileMap.GetCellAutotileCoord((int) pos.x, (int) pos.y));
			GD.Print(tileMap.availableTileType);
			if (tileMap.GetCellAutotileCoord((int) pos.x, (int) pos.y) != tileMap.availableTileType) {
				iconTileMap.PlaceTile(pos, curTile);
				GetNextTile();
			}
			else {
			}
		}
		else {
			if (tileMap.IsAvailable(pos)) {
				tileMap.PlaceTile(pos, curTile);
				GetNextTile();
			} 
		}
	}
	// Gets next tile from bag
	public void GetNextTile() {
		int random = rng.RandiRange(0, bag.Count-1);
		Vector2 tileAtlas = tiles[(int) bag[random]];
		Control CurrentTileTextureControl = GetParent().GetNode("UI").GetNode<Control>("CurrentTileTexture");
		foreach (Node i in CurrentTileTextureControl.GetChildren()) {
			i.QueueFree();
		}
		var tileScene = TileHandler.GetTileScene(tileAtlas);
		Tile tile = (Tile) tileScene.Instance();
		CurrentTileTextureControl.AddChild(tile);
		curTile = tile;
	}
}

using Godot;
using System;
using System.Collections;

public class Sprite : Godot.Sprite
{
	private TileMap tileMap;
	private TileMap iconTileMap;
	public Tile curTile;
	private int curTileIndex;
	public RandomNumberGenerator rng = new RandomNumberGenerator();
	private Label scoreLabel;
	private VBoxContainer notificationHolder;
	private static PackedScene tileNotificationScene = GD.Load<PackedScene>("res://scenes/DiscoveredTileNotification.tscn");
	private static int HABITAT_INDEX = 3;
	public bool paused = false;
	
	// Tile collection array
	private Vector2[] tiles = {
		new Vector2(0,0),
		new Vector2(1,0),
		new Vector2(2,0),
		new Vector2(-1,-1)
	};
	
	// Bag of tiles
	private ArrayList bag = new ArrayList();
	
	public override void _Ready()
	{
		//Input.MouseMode = Input.MouseModeEnum.Hidden;
		tileMap = GetParent().GetNode("Camera2D").GetNode<TileMap>("TileMap");
		iconTileMap = GetParent().GetNode("Camera2D").GetNode<TileMap>("IconTileMap");
		scoreLabel = GetParent().GetNode<Control>("UI").GetNode<HBoxContainer>("HBoxContainer").GetNode<Label>("Score");
		notificationHolder = GetParent().GetNode("UI").GetNode<VBoxContainer>("NotificationHolder");
		
		// Starting bag
		// One Grass and habitat
		//bag.Add(0);
		//bag. Add(3);
		// 30 Dirt
		for (int i = 0; i < 30; i++) {
			bag.Add(0);
		}
		// 10 Water
		for (int i = 0; i < 10; i++) {
			bag.Add(2);
		}
		// 5 Habitats
		for (int i = 0; i < 5; i++) {
			bag.Add(3);
		}
		
		GD.Randomize();
		rng.Randomize();
		var tileScene = TileHandler.GetTileScene(tiles[1]);
		curTile = (Tile) tileScene.Instance();
		curTileIndex = 0;
	}
	public override void _Process(float delta) {
		this.GlobalPosition = GetGlobalMousePosition(); 
	}
	public override void _Input(InputEvent inputEvent) {
		if (paused) return;
		// Check if place tile
		if (inputEvent.IsActionPressed("ui_accept")) {
			var mousePos = tileMap.GetMousePosition();
			PlaceTile(mousePos);
			var score = tileMap.score + iconTileMap.score;
			scoreLabel.Text = "" + score;
		}
		// Check if zoom
		
	}
	// Places a tile at pos
	public void PlaceTile(Vector2 pos) {
		if (curTileIndex == -1) return;
		if (curTile is Habitat) {
			if (tileMap.GetCellAutotileCoord((int) pos.x, (int) pos.y) == tileMap.availableTileType) {
				return;
			}
			if (tileMap.GetCell((int) pos.x, (int) pos.y) == TileMap.InvalidCell) {
				return;
			}
			iconTileMap.PlaceTile(pos, curTile);
			GetNextTile();
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
		//GD.Print(bag.Count);
		bag.RemoveAt(curTileIndex);
		if (bag.Count == 0) {
			curTileIndex = -1;
			TriggerEndOfGame();
			return;
		}
		curTileIndex = rng.RandiRange(0, bag.Count-1);
		Vector2 tileAtlas = tiles[(int) bag[curTileIndex]];
		Control CurrentTileTextureControl = GetParent().GetNode("UI").GetNode<VBoxContainer>("VBoxContainer").GetNode<Control>("CurrentTileTexture");
		foreach (Node i in CurrentTileTextureControl.GetChildren()) {
			i.QueueFree();
		}
		var tileScene = TileHandler.GetTileScene(tileAtlas);
		Tile tile = (Tile) tileScene.Instance();
		CurrentTileTextureControl.AddChild(tile);
		curTile = tile;
	}

	// TODO: tile discovered function in sprite
	public void tileDiscovered(Tile tile) {
		var notification = (DiscoveredTileNotification)tileNotificationScene.Instance();
		notification.GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Title").Text= tile.discoveryTitle;
		notification.GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Description").Text = tile.discoveryDescription;
		notificationHolder.AddChild(notification);
		// Control textureControl = notification.GetNode("VBoxContainer").GetNode<Container>("Container");
		// foreach (Node i in textureControl.GetChildren()) {
		// 	i.QueueFree();
		// }
		// if (tile is Habitat) {
		// 	textureControl.AddChild(HabitatHandler.GetTileScene(tile.atlasCoord));
		// }
		// else {
		// 	textureControl.AddChild(TileHandler.GetTileScene(tile.atlasCoord).Instance());
		// }
		notification.RectPosition = GetViewportRect().Size/2;
		if (tile is Habitat) {
			for (int i = 0; i < tile.discoveryAddition; i++) {
				bag.Add(HABITAT_INDEX);
			}
		} else {
			int indexOfTile = 0;
			for (int i = 0; i < tiles.Length; i++) { // TODO: This could be optimised.
				if (tiles[i] == tile.atlasCoord) {
					indexOfTile = i;
					break;
				}
			}
			for (int i = 0; i < tile.discoveryAddition; i++) {
				bag.Add(indexOfTile);
			}
		}
	}

	// TODO: Trigger end of game
	public void TriggerEndOfGame() {
		var endScreen = GetParent().GetNode("UI").GetNode<EndScreen>("EndScreen");
		var animationPlayer = endScreen.GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("FadeIn");
		endScreen.Visible = true;
		endScreen.SetScore(tileMap.score + iconTileMap.score);
	}
}

using Godot;
using System;

public class Sprite : Godot.Sprite
{
	TileMap tileMap;
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		tileMap = GetParent().GetNode<TileMap>("TileMap");
	}
	public override void _Process(float delta) {
		this.GlobalPosition = GetGlobalMousePosition(); 
	}
	public override void _Input(InputEvent inputEvent) {
		if (inputEvent.IsActionPressed("ui_accept")) {
			var mousePos = tileMap.GetMousePosition();
			tileMap.SetCellv(mousePos, 0);
			GD.Print("Set");
		}
	}
}

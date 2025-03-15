using Godot;
using System;

public class Sprite : Godot.Sprite
{
	public override void _Ready()
	{
		// Input.SetMouseMode(Input.MOUSE_MODE_HIDDEN);
	}
	public override void _Process(float delta) {
		this.GlobalPosition = GetGlobalMousePosition(); 
	}
	public override void _Input(InputEvent inputEvent) {
		if (inputEvent.IsActionPressed("ui_accept")) {
			GD.Print("Beep");
		}
	}
}

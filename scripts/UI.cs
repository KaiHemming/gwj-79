using Godot;
using System;

public class UI : Control
{
	public override void _Ready()
	{
		PopUp();
	}
	public void PopUp() {
		GetParent().GetNode<Sprite>("Sprite").paused = true;
		Input.MouseMode = Input.MouseModeEnum.Visible;
		GetNode<WindowDialog>("Tutorial").Show();
	}
	public override void _Process(float delta) {
		//GD.Print(GetViewportRect().Size);
		this.RectSize = GetViewportRect().Size;
		//GetNode<AcceptDialog>("AcceptDialog").Popup_();
	}
	private void _on_CloseButton_pressed()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		GetParent().GetNode<Sprite>("Sprite").paused = false;
		GetNode<WindowDialog>("Tutorial").Hide();
	}
}

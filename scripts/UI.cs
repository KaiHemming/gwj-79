using Godot;
using System;

public class UI : CanvasLayer
{
	private int score = 0;
	public static PackedScene scoreAdditionScene;
	private Sprite sprite;
	
	public override void _Ready()
	{
		scoreAdditionScene = GD.Load<PackedScene>("res://scenes/ScoreAddition.tscn");
		sprite = GetNode<Sprite>("Sprite");
		PopUp();
	}
	public void PopUp() {
		sprite.paused = true;
		GetNode<WindowDialog>("Tutorial").Show();
	}
	public override void _Process(float delta) {
		//GD.Print(GetViewportRect().Size);
		//GetNode<AcceptDialog>("AcceptDialog").Popup_();
	}
	private void _on_CloseButton_pressed()
	{
		sprite.paused = false;
		GetNode<WindowDialog>("Tutorial").Hide();
	}
	public void AddScore(int increment) {
		if (increment == 0) return;
		this.score += increment;
		GetNode("HBoxContainer").GetNode<Label>("Score").Text = "" + score;
		ScoreAddition scoreAddition = (ScoreAddition)scoreAdditionScene.Instance();
		scoreAddition.SetScoreAddition(increment);
		GetNode("HBoxContainer").GetNode<VBoxContainer>("ScoreAdditions").AddChild(scoreAddition);
	}
	public void DeductScore(int increment) {
		if (increment == 0) return;
		this.score -= increment;
		GetNode("HBoxContainer").GetNode<Label>("Score").Text = "" + score;
		ScoreAddition scoreAddition = (ScoreAddition)scoreAdditionScene.Instance();
		scoreAddition.SetScoreReduction(increment);
		GetNode("HBoxContainer").GetNode<VBoxContainer>("ScoreAdditions").AddChild(scoreAddition);
	}
	public int GetScore() {
		return score;
	}
	private void _on_HelpButton_pressed()
	{
		PopUp();
	}
	public void UpdateNumRemaining(int bagSize) {
		GetNode("VBoxContainer").GetNode<Label>("RemainingTiles").Text = bagSize + " remaining";
	}
	public void UpdateTileName(String tileName) {
		GetNode("VBoxContainer").GetNode<Label>("TileName").Text = tileName;
	}
}

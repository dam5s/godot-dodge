using Godot;
using GameLogic.PlayerLogic;


public class Player : Area2D {

    [Export]
    public float Speed  = 0.0f;

    private Vector2 _screenSize;


    public override void _Ready() {
        _screenSize = GetViewport().GetSize();
    }


    public override void _Process(float delta) {
        var velocity = PlayerMovement.velocity(Speed, CurrentInputs());
        var animatedSprite = (AnimatedSprite) GetNode("AnimatedSprite");

        if (velocity.Length () > 0.0f) {
            animatedSprite.Play();
        } else {
            animatedSprite.Stop();
        }

        Position = PlayerMovement.newPosition(Position, velocity, delta, _screenSize);
    }


    private static PlayerInputs CurrentInputs() {
        return new PlayerInputs(
            Input.IsActionPressed("ui_up"),
            Input.IsActionPressed("ui_down"),
            Input.IsActionPressed("ui_left"),
            Input.IsActionPressed("ui_right")
        );
    }
}

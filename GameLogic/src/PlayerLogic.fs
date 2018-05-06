namespace GameLogic.PlayerLogic

open Godot


type PlayerInputs =
    { UpPressed : bool
    ; DownPressed : bool
    ; LeftPressed : bool
    ; RightPressed : bool
    }


module Direction =
    type Vertical =
        | Up
        | Down
        | VNeutral

    type Horizontal =
        | Left
        | Right
        | HNeutral

    let velocityY (v: Vertical) : float32 =
        match v with
        | Up -> -1.0f
        | Down -> 1.0f
        | _ -> 0.0f

    let velocityX (h: Horizontal) : float32 =
        match h with
        | Left -> -1.0f
        | Right -> 1.0f
        | _ -> 0.0f


module PlayerMovement =
    open Direction

    let private verticalDirection (inputs : PlayerInputs) : Vertical =
        if inputs.UpPressed && inputs.DownPressed then
            VNeutral
        else if inputs.UpPressed then
            Up
        else if inputs.DownPressed then
            Down
        else
            VNeutral

    let private horizontalDirection (inputs : PlayerInputs) : Horizontal =
        if inputs.LeftPressed && inputs.RightPressed then
            HNeutral
        else if inputs.LeftPressed then
            Left
        else if inputs.RightPressed then
            Right
        else
            HNeutral

    let velocity (speed : float32) (inputs : PlayerInputs) : Vector2 =
        let velocityX = velocityX (horizontalDirection inputs)
        let velocityY = velocityY (verticalDirection inputs)
        let newVelocity = new Vector2 (velocityX, velocityY)

        newVelocity.Normalized () * speed

    let newPosition (oldPosition : Vector2) (velocity : Vector2) (delta : float32) (boundaries : Vector2) : Vector2 =
        let unboundedPosition = oldPosition + (velocity * delta)
        let boundedX = Mathf.Clamp (unboundedPosition.x, 0.0f, boundaries.x)
        let boundedY = Mathf.Clamp (unboundedPosition.y, 0.0f, boundaries.y)

        new Vector2 (boundedX , boundedY)

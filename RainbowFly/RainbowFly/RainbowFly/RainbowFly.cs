using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class RainbowFly : PhysicsGame
{
    public override void Begin()
    {
        hahmoluo();
        luoraha();
        Image bgImg = LoadImage("tausta");
        Level.Size = new Vector(bgImg.Width * 10, bgImg.Height);
        Level.Background.Image = LoadImage("tausta");
        Level.Background.TileToLevel();
        Level.Background.MoveTo(new Vector(-Screen.Width, 0), 100);

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void hahmoluo()
    {
        PhysicsObject hahmo = new PhysicsObject(40, 65);
        hahmo.Shape = Shape.Circle;
        Add(hahmo);
        Camera.Follow(hahmo);
        hahmo.Velocity = new Vector(200, 0);
        hahmo.CanRotate = false;

    }
    void luoraha()
    {
        PhysicsObject raha = new PhysicsObject(20, 20);
        raha.Color = Color.Yellow;
        raha.Shape = Shape.Circle;
        raha.Position = RandomGen.NextVector(100, 400);
        Add(raha);

    }
}

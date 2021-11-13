using System;
using cse210_batter.Services;
using cse210_batter.Casting;
using cse210_batter.Scripting;
using System.Collections.Generic;

namespace cse210_batter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the cast
            Dictionary<string, List<Actor>> cast = new Dictionary<string, List<Actor>>();

            // Bricks
            cast["bricks"] = new List<Actor>();

            // TODO: Add your bricks here
            for (int y = 5; y < Constants.MAX_Y / 3; 
                y += (Constants.BRICK_HEIGHT + 3))  // +3 is arbitrary for
                                                    // extra padding
            {
                for(int x = 5; x < (Constants.MAX_X - 5);
                    x += (Constants.BRICK_WIDTH + Constants.BRICK_SPACE))
                {
                    Point position = new Point(x,y);
                    cast["bricks"].Add(new Brick(position));
                }
            }

            // The Ball (or balls if desired)
            cast["balls"] = new List<Actor>();

            // TODO: Add your ball here
            Ball ball = new Ball();
            cast["balls"].Add(ball);

            // The paddle
            cast["paddle"] = new List<Actor>();

            // TODO: Add your paddle here
            Paddle paddle = new Paddle();
            cast["paddle"].Add(paddle);

            // Create the script
            Dictionary<string, List<Action>> script = new Dictionary<string, 
                List<Action>>();

            OutputService outputService = new OutputService();
            InputService inputService = new InputService();
            PhysicsService physicsService = new PhysicsService();
            AudioService audioService = new AudioService();

            script["output"] = new List<Action>();
            script["input"] = new List<Action>();
            script["update"] = new List<Action>();

            DrawActorsAction drawActorsAction = new DrawActorsAction(outputService);
            script["output"].Add(drawActorsAction);

            // TODO: Add additional actions here to handle the input, move the
            // actors, handle collisions, etc.
            MoveActorsAction moveActorsAction = new MoveActorsAction();
            script["update"].Add(moveActorsAction);

            HandleCollisionsAction handleCollisionsAction = new HandleCollisionsAction(physicsService);
            script["update"].Add(handleCollisionsAction);

            ControlActorsAction controlActorsAction = new ControlActorsAction(inputService);
            script["input"].Add(controlActorsAction);

            // Start up the game
            outputService.OpenWindow(Constants.MAX_X, Constants.MAX_Y, 
                "Batter", Constants.FRAME_RATE);
            audioService.StartAudio();
            audioService.PlaySound(Constants.SOUND_START);

            Director theDirector = new Director(cast, script);
            theDirector.Direct();

            audioService.StopAudio();
        }
    }
}

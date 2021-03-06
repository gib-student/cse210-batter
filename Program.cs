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

            for (int y = 5; y < Constants.MAX_Y / 2.5;
                y += (Constants.BRICK_HEIGHT + Constants.BRICK_SPACE))
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
            cast["balls"].Add(new Ball());

            // The paddle
            cast["paddle"] = new List<Actor>();
            cast["paddle"].Add(new Paddle());

            // Lives text
            cast["lives"] = new List<Actor>();
            Lives livesText = new Lives();
            cast["lives"].Add(livesText);

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

            HandleCollisionsAction handleCollisionsAction =
                new HandleCollisionsAction(physicsService, audioService);
            script["update"].Add(handleCollisionsAction);

            HandleOffScreenAction handleOffScreenAction =
                new HandleOffScreenAction(audioService);
            script["update"].Add(handleOffScreenAction);

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

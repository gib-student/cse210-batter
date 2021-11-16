using System;
using System.Collections.Generic;
using cse210_batter.Services;
using cse210_batter.Casting;
using cse210_batter.Scripting;

namespace cse210_batter
{
   public class Test
   {
      public void RunTests()
      {
         // Step 8
         Dictionary<string, List<Actor>> cast = new Dictionary<string, List<Actor>>();
         cast["bricks"] = new List<Actor>();

         for (int y = 5; y < Constants.MAX_Y / 2.5; 
            y += (Constants.BRICK_HEIGHT + Constants.BRICK_SPACE))  // +3 is arbitrary for
                                                                         // extra padding
         {
           for(int x = 5; x < (Constants.MAX_X - 5);
               x += (Constants.BRICK_WIDTH + Constants.BRICK_SPACE))
            {
               Point position = new Point(x,y);
               cast["bricks"].Add(new Brick(position));
            }
         }
         
         OutputService outputService = new OutputService();
         outputService.OpenWindow(Constants.MAX_X,Constants.MAX_Y,"Test",
            Constants.FRAME_RATE);
         outputService.StartDrawing();
         
         // Step 9
         Ball ball = new Ball();
         cast["balls"] = new List<Actor>();
         cast["balls"].Add(ball);

         // Step 10
         MoveActorsAction move = new MoveActorsAction();
         
         bool done = false;
         while (!done)
         {
            move.Execute(cast);
            foreach (List<Actor> group in cast.Values)
            {
               outputService.DrawActors(group);
            }
         }

         outputService.EndDrawing();
      }
   }
}
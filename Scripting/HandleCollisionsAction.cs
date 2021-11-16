using System;
using System.Collections.Generic;
using cse210_batter.Casting;
using cse210_batter.Services;

namespace cse210_batter.Scripting
{
   /// <summary>
   /// An action to handle the collisions for all the actors in the game
   /// </summary>
   public class HandleCollisionsAction : Action
   {
      PhysicsService _physicsService;
      AudioService _audioService;
      
      public HandleCollisionsAction(PhysicsService physicsService, 
         AudioService audioService)
      {
         _physicsService = physicsService;
         _audioService = audioService;
      }

      public override void Execute(Dictionary<string, List<Actor>> cast)
      {
         // Handle ball collisions
         foreach (Ball ball in cast["balls"])
         {
            // Handle if ball hits paddle
            if (BallHitPaddle(ball, cast["paddle"][0]))
            {
               InverseYVelocity(ball);
               _audioService.PlayBounce();
            }
            // Handle if ball hits brick
            List<Brick> bricksToRemove = new List<Brick>();
            bool ballInversed = false;
            foreach (Brick brick in cast["bricks"])
            {
               if (BallHitBrick(ball, brick) && !ballInversed)
               {
                  InverseYVelocity(ball);
                  ballInversed = true;
                  bricksToRemove.Add(brick);
                  _audioService.PlayBounce();
               }
            }
            // Remove the bricks
            foreach (Brick brick in bricksToRemove)
            {
               cast["bricks"].Remove(brick);
            }
         }
      }

      private bool BallHitPaddle(Ball ball, Actor paddle)
      {
         if (_physicsService.IsCollision(ball, paddle))
         {
            return true;
         }
         // No collision
         return false;
      }

      private bool BallHitBrick(Ball ball, Brick brick)
      {
         if (_physicsService.IsCollision(brick, ball))
         {
            return true;
         }
         // No collision
         return false;
      }
   }
}
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
      private PhysicsService _physicsService;
      bool _ballHitBrick = false;
      const string _leftWall = "left";
      const string _rightWall = "right";
      const string _bottomWall = "bottom";

      public HandleCollisionsAction(PhysicsService physicsService)
      {
         _physicsService = physicsService;
      }

      public override void Execute(Dictionary<string, List<Actor>> cast)
      {
         // Go through the actors. For each one, determine if it has collided
         // with something (a wall, a brick, or the paddle) and change its 
         // movement accordingly
         foreach (string key in cast.Keys)
         {
            foreach (Actor actor in cast[key])
            {
               // Handle if it's a ball.
               // NOTE: if the ball hits a brick, that will be tested for
               // and handled down below in the "brick" section
               if (key == "ball")
               {
                  // Test if it hit a wall
                  string wallHit = BallHitWall(actor);
                  ChangeBallVelocity(actor, wallHit);
                  // Test if it hit the paddle
                  if (BallHitPaddle(actor))
                  {
                     InverseYVelocity(actor);
                  }
               }
               // Handle if it's a paddle
               if (key == "paddle")
               {
                  // Test if it hit a wall
                  if (PaddleHitWall(actor))
                  {
                     Paddle paddle = actor;
                  }
               }
               // Handle if its a brick
               if (key == "brick")
               {
                  // Just change the color of the brick, and nothing else
                  if (BrickHitBall(actor))
                  {
                     _ballHitBrick = true;
                     actor.SetImage("Assets\brick-4.png"); // Orange brick
                  }
               }
            }
         }
         // After testing all of the bricks if there was a ball-brick collision,
         // revrse the direction of the ball if there was.
         if (_ballHitBrick)
         {
            InverseYVelocity(cast["ball"][0]);
         }
      }

      /// <summary>
      /// Test if the actor hit a wall. If it did, then return a string 
      /// containing a description of which wall it hit.
      /// </summary>
      /// <param name="actor">The actor which is to be tested if it hit a wall</param>
      private string BallHitWall(Actor actor)
      {
         // Get its position and velocity first
         int x = actor.GetX();
         int y = actor.GetY();

         if (x < 0)
         {
            return _leftWall;
         }
         else if (x > Constants.MAX_X)
         {
            return _rightWall;
         }
         if (y > Constants.MAX_Y)
         {
            return _bottomWall;
         }

         return "no collision";
      }

      private bool BallHitPaddle(Actor actor)
      {
         // use Raylib_cs.CheckCollisionRecs()
         throw new NotImplementedException();
      }

      private bool PaddleHitWall(Actor actor)
      {
         // use Raylib_cs.CheckCollisionRecs()
         throw new NotImplementedException();
      }
      
      private bool BrickHitBall(Actor actor)
      {
         // use Raylib_cs.CheckCollisionRecs()
         throw new NotImplementedException();         
      }

      private void ChangeBallVelocity(Actor actor, string wallHit)
      {
         if (wallHit == _leftWall || wallHit == _rightWall)
         {
            InverseXVelocity(actor);
         }
         else if (wallHit == _bottomWall)
         {
            // TODO: End the game! How do I do this???
            throw new NotImplementedException();
         }
      }

      private void InverseXVelocity(Actor actor)
      {
         int dx = actor.GetVelocity().GetX();
         int dy = actor.GetVelocity().GetY();

         int newDX = -dx;

         actor.SetVelocity(new Point(newDX, dy));
      }

      private void InverseYVelocity(Actor actor)
      {
         int dx = actor.GetVelocity().GetX();
         int dy = actor.GetVelocity().GetY();

         int newDY = -dy;

         actor.SetVelocity(new Point(dx, newDY));
      }

      private void StopVelocity(Actor actor)
      {
         actor.SetVelocity(new Point(0,0));
      }
   }
}
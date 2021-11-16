using System.Collections.Generic;
using cse210_batter.Casting;
using cse210_batter.Services;

namespace cse210_batter.Scripting
{
   public class HandleOffScreenAction : Action
   {
      AudioService _audioService;
      const string _leftWall = "left";
      const string _rightWall = "right";
      const string _upperWall = "upper";
      const string _bottomWall = "bottom";
      const string _noCollision = "no collision";

      public HandleOffScreenAction(AudioService audioService)
      {
         _audioService = audioService;
      }

      public override void Execute(Dictionary<string, List<Actor>> cast)
      {
         // Handle balls goiong off screen
         foreach (Ball ball in cast["balls"])
         {
            // Test if it hit a wall
            string wallHit = BallHitWall(ball);
            if (wallHit != _noCollision)
            {
               _audioService.PlayBounce();
               InverseBallVelocity(ball, wallHit);
            }
         }

         // Handle paddles going off screen
         foreach (Paddle paddle in cast["paddle"])   
         {
            // Test if it has gone off-screen
            if (PaddleHitsWall(paddle))
            {
               StopVelocity(paddle);
            }
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
         else if (x + Constants.BALL_WIDTH > Constants.MAX_X)
         {
            return _rightWall;
         }
         if (y < 0)
         {
            return _upperWall;
         }
         else if (y + Constants.BALL_HEIGHT > Constants.MAX_Y)
         {
            return _bottomWall;
         }

         return _noCollision;
      }
      
      /// <summary>
      /// Test if the paddle has collided with a horizontal wall.
      /// </summary>
      /// <param name="actor"> Actor whose position is to be tested</param>
      private bool PaddleHitsWall(Actor actor)
      {
         int x = actor.GetPosition().GetX();

         if (x - Constants.PADDLE_WIDTH < 0 || 
             x + Constants.PADDLE_WIDTH > Constants.MAX_X)
         {
            return true;
         }
         // No wall collision
         return false;
      }

      private void StopVelocity(Actor actor)
      {
         actor.SetVelocity(new Point(0,0));
      }

      /// <summary>
      /// Change the velocity of the ball according to which wall it hit.
      /// If it hit a vertical wall, inverse x velocity; if a horizontal wall,
      /// inverse y velocity.
      /// </summary>
      /// <param name="actor">Actor whose velocity is to be changed</param>
      /// <param name="wallHit">Name of the wall which was hit</param>
      private void InverseBallVelocity(Actor actor, string wallHit)
      {
         if (wallHit == _leftWall || wallHit == _rightWall)
         {
            InverseXVelocity(actor);
         }
         else if (wallHit == _upperWall)
         {
            InverseYVelocity(actor);
         }
         else if (wallHit == _bottomWall)
         {
            InverseYVelocity(actor);
         }
      }
   }
}
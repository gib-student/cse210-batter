using System.Collections.Generic;
using cse210_batter.Casting;
using cse210_batter.Services;

namespace cse210_batter.Scripting
{
   public class HandleOffScreenAction : Action
   {
      Dictionary<string, List<Actor>> _cast;
      AudioService _audioService;
      Lives _lives;
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
         _cast = cast;
         _lives = (Lives)_cast["lives"][0];
         // Handle balls goiong off screen
         List<Ball> ballsToRemove = new List<Ball>();
         foreach (Ball ball in _cast["balls"])
         {
            // Test if it hit a wall
            string wallHit = BallHitWall(ball);
            // If the ball went out on the bottom, remove a life or remove the
            // ball from play if no more lives
            if (wallHit == _bottomWall)
            {
               if (_lives.NoLivesLeft())
               {
                  _lives.TakeALife();
                  ballsToRemove.Add(ball);
               }
               else
               {
                  InverseBallVelocity(ball, wallHit);
                  _audioService.PlayBounce();
                  _lives.TakeALife();
               }
            }
            // If the ball hit the left, right or upper walls, make it bounce
            else if (wallHit != _noCollision)
            {
               _audioService.PlayBounce();
               InverseBallVelocity(ball, wallHit);
            }
         }
         // Remove balls that went out of play
         RemoveBallsFromPlay(ballsToRemove);
         
         // Handle paddles going off screen
         foreach (Paddle paddle in _cast["paddle"])
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

         if (actor.GetLeftEdge() < 0)
         {
            return _leftWall;
         }
         else if (actor.GetRightEdge() > Constants.MAX_X)
         {
            return _rightWall;
         }
         if (actor.GetTopEdge() < 0)
         {
            return _upperWall;
         }
         else if (actor.GetBottomEdge() > Constants.MAX_Y)
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

         if (actor.GetLeftEdge() < 0 ||
             actor.GetRightEdge() > Constants.MAX_X)
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
      private void InverseBallVelocity(Ball ball, string wallHit)
      {
         if (wallHit == _leftWall || wallHit == _rightWall)
         {
            InverseXVelocity(ball);
         }
         else if (wallHit == _upperWall || wallHit == _bottomWall)
         {
            InverseYVelocity(ball);
         }
      }

      private void RemoveBallsFromPlay(List<Ball> ballsToRemove)
      {
         foreach(Ball ball in ballsToRemove)
         {
            RemoveBall(ball);
         }
      }
      
      private void RemoveBall(Ball ball)
      {
         // Remove the ball from the cast
         _cast["balls"].Remove(ball);
      }
   }
}
using System;

namespace cse210_batter.Casting
{
   public class Paddle : Actor
   {
      private bool _againstWall = false;
      /// <summary>
      /// Creates a new Paddle
      /// </summary>
      public Paddle()
      {
         Point position = new Point(Constants.PADDLE_X, Constants.PADDLE_Y);
         Point velocity = new Point(0,0);
         // Only moves horizontally
         SetPosition(position);
         SetVelocity(velocity);
         SetImage(Constants.IMAGE_PADDLE);
         SetWidth(Constants.PADDLE_WIDTH);
         SetHeight(Constants.PADDLE_HEIGHT);
      }

      public bool GetAgainstWall()
      {
         return _againstWall;
      }
      
      public void SetAgainstWall(bool againstWall)
      {
         _againstWall = againstWall;
      }
   }
}
using System;

namespace cse210_batter.Casting
{
   public class Paddle : Actor
   {
      /// <summary>
      /// Creates a new Paddle
      /// </summary>
      public Paddle()
      {
         Point position = new Point(Constants.PADDLE_X, Constants.PADDLE_Y);
         // Only moves horizontally
         Point velocity = new Point(Constants.PADDLE_SPEED, 0);
         SetPosition(position);
         SetVelocity(velocity);
         SetImage(Constants.IMAGE_PADDLE);
         SetWidth(Constants.PADDLE_WIDTH);
         SetHeight(Constants.PADDLE_HEIGHT);
      }
   }
}
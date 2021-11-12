using System;

namespace cse210_batter.Casting
{
   public class Ball : Actor
   {
      /// <summary>
      /// Creates a new Ball
      /// </summary>
      public Ball()
      {
         Point position = new Point(Constants.BALL_X, Constants.BALL_Y);
         Point velocity = new Point(Constants.BALL_DX, Constants.BALL_DY);
         SetPosition(position);
         SetVelocity(velocity);
         SetImage(Constants.IMAGE_BALL);
         SetWidth(Constants.BALL_WIDTH);
         SetHeight(Constants.BALL_HEIGHT);
      }
   }
}
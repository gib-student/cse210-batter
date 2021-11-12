using System;

namespace cse210_batter.Casting
{
   public class Brick : Actor
   {
      /// <summary>
      /// Creates a new Brick
      /// </summary>
      /// <param name="position">x and y coords of the brick</param>
      public Brick(Point position)
      {
         Point velocity = new Point(0,0); // stationary
         SetPosition(position);
         SetVelocity(velocity);
         SetImage(Constants.IMAGE_BRICK);
         SetWidth(Constants.BRICK_WIDTH);
         SetHeight(Constants.BRICK_HEIGHT);
      }
   }
}
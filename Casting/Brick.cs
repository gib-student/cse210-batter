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
         SetPosition(position);
         SetImage(Constants.IMAGE_BRICK);
         SetWidth(Constants.BRICK_WIDTH);
         SetHeight(Constants.BRICK_HEIGHT);
      }
   }
}
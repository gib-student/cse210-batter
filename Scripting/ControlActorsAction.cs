using System.Collections.Generic;
using cse210_batter.Casting;
using cse210_batter.Services;

namespace cse210_batter.Scripting
{
   /// <summary>
   /// An action to get user input, and update actors accordingly.
   /// </summary>
   public class ControlActorsAction: Action
   {
      private InputService _inputService;
      
      public ControlActorsAction(InputService inputService)
      {
         _inputService = inputService;
      }

      public override void Execute(Dictionary<string, List<Actor>> cast)
      {
         Point direction = _inputService.GetDirection();

         foreach (Paddle paddle in cast["paddle"])
         {
            Point velocity = direction.Scale(Constants.PADDLE_SPEED);
            
            if (!(PaddleViolatesLeftWall(paddle) && GoingLeft(velocity)) &&
                !(PaddleViolatesRightWall(paddle) && GoingRight(velocity)))
            {
               paddle.SetVelocity(velocity);
            }            
         }
      }

      private bool PaddleViolatesLeftWall(Paddle paddle)
      {
         int x = paddle.GetPosition().GetX();
         return (x < 0);
      }

      private bool PaddleViolatesRightWall(Paddle paddle)
      {
         int x = paddle.GetPosition().GetX();
         return ((x + Constants.PADDLE_WIDTH) > Constants.MAX_X);
      }

      private bool GoingLeft(Point velocity)
      {
         int x = velocity.GetX();
         return (x < 0);
      }

      private bool GoingRight(Point velocity)
      {
         int x = velocity.GetX();
         return (x > 0);
      }
   }
}
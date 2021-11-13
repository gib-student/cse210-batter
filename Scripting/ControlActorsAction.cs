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

         Actor paddle = cast["paddle"][0];

         Point paddleVelocity = direction.Scale(Constants.PADDLE_SPEED);
         paddle.SetVelocity(paddleVelocity);
      }
   }
}
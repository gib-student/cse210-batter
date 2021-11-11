using System.Collections.Generic;
using cse210_batter.Casting;
using cse210_batter.Services;

namespace cse210_batter.Scripting
{
   public class ControlActorsAction: Action
   {
      private InputService _inputService;
      
      public ControlActorsAction(InputService inputService)
      {
         _inputService = inputService;
      }

      public override void Execute(Dictionary<string, List<Actor>> cast)
      {
         throw new System.NotImplementedException();
      }
   }
}
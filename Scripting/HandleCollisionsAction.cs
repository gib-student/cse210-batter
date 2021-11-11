using System.Collections.Generic;
using cse210_batter.Casting;
using cse210_batter.Services;

namespace cse210_batter.Scripting
{
   public class HandleCollisionsAction : Action
   {
      private PhysicsService _physicsService;

      public HandleCollisionsAction(PhysicsService physicsService)
      {
         _physicsService = physicsService;
      }

      public override void Execute(Dictionary<string, List<Actor>> cast)
      {
         throw new System.NotImplementedException();
      }
   }
}
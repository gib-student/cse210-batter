using System.Collections.Generic;
using cse210_batter.Casting;
using cse210_batter.Services;

namespace cse210_batter.Scripting
{
   /// <summary>
   /// An action to move all the actors in the game
   /// </summary>
   public class MoveActorsAction : Action
   {
      public MoveActorsAction()
      {

      }

      public override void Execute(Dictionary<string, List<Actor>> cast)
      {
         foreach (List<Actor> group in cast.Values)
         {
            foreach (Actor actor in group)
            {
               MoveActor(actor);
            }
         }
      }

      private void MoveActor(Actor actor)
      {

         int x = actor.GetX();
         int y = actor.GetY();

         // Get the current velocity
         int dx = actor.GetVelocity().GetX();
         int dy = actor.GetVelocity().GetY();

         // Set the new position
         int newX = (x + dx);
         int newY = (y + dy);

         actor.SetPosition(new Point(newX, newY));
      }
   }
}
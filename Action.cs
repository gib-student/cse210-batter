using System;
using System.Collections.Generic;
using cse210_batter.Casting;

namespace cse210_batter
{
    /// <summary>
    /// The base class of all other actions.
    /// </summary>
    public abstract class Action
    {
        public abstract void Execute(Dictionary<string, List<Actor>> cast);
    
        protected void InverseYVelocity(Actor actor)
        {
            int dx = actor.GetVelocity().GetX();
            int dy = actor.GetVelocity().GetY();

            int newDY = -dy;

            actor.SetVelocity(new Point(dx, newDY));
        }

        protected void InverseXVelocity(Actor actor)
        {
            int dx = actor.GetVelocity().GetX();
            int dy = actor.GetVelocity().GetY();

            int newDX = -dx;

            actor.SetVelocity(new Point(newDX, dy));
        }
    }
    
}
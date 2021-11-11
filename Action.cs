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
    }
}
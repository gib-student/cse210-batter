using System.Collections.Generic;
using cse210_batter.Casting;
using cse210_batter.Services;


namespace cse210_batter.Scripting
{
    /// <summary>
    /// An action to draw all of the actors in the game.
    /// </summary>
    public class DrawActorsAction : Action
    {
        private OutputService _outputService;

        public DrawActorsAction(OutputService outputService)
        {
            _outputService = outputService;
        }

        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            _outputService.StartDrawing();

            foreach (List<Actor> group in cast.Values)
            {
                _outputService.DrawActors(group);
            }
            
            // Draw lives
            Lives lives = (Lives)cast["lives"][0];
            string livesText = $"Lives: " + lives.GetLives();
            _outputService.DrawText(Constants.LIVES_X, Constants.LIVES_Y, 
                livesText, false);

            _outputService.EndDrawing();
        }

    }
}
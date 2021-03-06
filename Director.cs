using System;
using System.Collections.Generic;
using cse210_batter.Casting;
using cse210_batter.Services;
using cse210_batter.Scripting;

namespace cse210_batter
{
    /// <summary>
    /// The director is responsible to direct the game, including to keep track of all
    /// the actors and to control the sequence of play.
    /// 
    /// Stereotype:
    ///     Controller
    /// </summary>
    public class Director
    {
        private bool _keepPlaying = true;
        private Dictionary<string, List<Actor>> _cast;
        private Dictionary<string, List<Action>> _script;

        public Director(Dictionary<string, List<Actor>> cast, Dictionary<string, List<Action>> script)
        {
            _cast = cast;
            _script = script;
        }

        /// <summary>
        /// This method starts the game and continues running until it is finished.
        /// </summary>
        public void Direct()
        {
            while (_keepPlaying)
            {
                CueAction("input");
                CueAction("update");
                CueAction("output");

                // Two end conditions are: user closes the window, and the 
                // user loses the game. 
                if (Raylib_cs.Raylib.WindowShouldClose())
                {
                    _keepPlaying = false;
                }
                else if (IfGameOver())
                {
                    _keepPlaying = false;
                    EndGame();
                }
            }
        }

        /// <summary>
        /// Executes all of the actions for the provided phase.
        /// </summary>
        /// <param name="phase"></param>
        private void CueAction(string phase)
        {
            List<Action> actions = _script[phase];

            foreach (Action action in actions)
            {
                action.Execute(_cast);
            }
        }

        private int GetNumBalls()
        {
            int numBalls = 0;
            foreach (Ball ball in _cast["balls"])
            {
                numBalls++;
            }

            return numBalls;
        }

        private bool IfGameOver()
        {
            if(GetNumBalls() == 0)
            {
                return true;
            }
            return false;
        }

        private void EndGame()
        {
            AudioService audioService = new AudioService();
            OutputService outputService = new OutputService();

            // Play game-over sound
            audioService.PlaySound(Constants.SOUND_OVER);

            // Display the last frame of the game plus "game over" until the
            // user closes the window
            while(!Raylib_cs.Raylib.WindowShouldClose())
            {
                _script["output"][0].Execute(_cast);
                outputService.DrawText(Constants.GAME_OVER_X, 
                    Constants.GAME_OVER_Y, Constants.GAME_OVER_TEXT, false);
            }
        }
    }
}

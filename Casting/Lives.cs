namespace cse210_batter.Casting
{
   public class Lives : Actor
   {
      int _lives;

      public Lives()
      {
         _lives = 3;
         SetPosition(new Point(Constants.LIVES_X, Constants.LIVES_Y));
      }

      public int GetLives()
      {
         return _lives;
      }

      public void TakeALife()
      {
         _lives--; 
      }

      public void AddALife()
      {
         _lives++;
      }

      public bool NoLivesLeft()
      {
         if (_lives == 1)
         {
            return true;
         }
         return false;
      }
   }
}
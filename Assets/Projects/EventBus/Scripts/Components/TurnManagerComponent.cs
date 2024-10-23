namespace EventBus
{
    public class TurnManagerComponent
    {
        public int SkipNumOfTurns { get; private set; }
        
        public void EditSkipTurns(int value)
        {
            SkipNumOfTurns += value;
            
            if(SkipNumOfTurns < 0) SkipNumOfTurns = 0;
        }
    }
}
namespace BlackJack.Logic
{
    // EndResult maintains the game result state
    public enum EndResult
    {
        DealerBlackJack, PlayerBlackJack, PlayerBust, DealerBust, Push, PlayerWin, DealerWin
    }

    public enum PlayerStatus
    {
        FirstTurn, BlackJack, Bust, Push, Win, Lose
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPagerScissor
{
    public class Program
    {
        static void Main(string[] args)
        {
            int totalMatches = 100;
            List<TournamentSummary> matchResult = new List<TournamentSummary>();
            for (int i=1; i<= totalMatches; i++)
            {
                var playerA = new Player()
                {
                    Name = "Player A",
                    Bet = GameAction.Rock
                };

                var playerB = new Player()
                {
                    Name = "Player B",
                    Bet = (GameAction)new Random().Next(0, 2)
                };

                var winner = new Battle(playerA, playerB).PlayMatchUp();
                if (winner == null)
                {
                    Console.WriteLine("Player A action '{0}', Player B action '{1}', Result = Draw",playerA.Bet,playerB.Bet);
                    matchResult.Add(new TournamentSummary { WinnerName = "None", MatchStatus = "Draw" });
                }
                else
                {
                    Console.WriteLine("Player A action '{0}', Player B action '{1}', Result = {2} is Winner ", playerA.Bet, playerB.Bet, winner.Name);
                    matchResult.Add(new TournamentSummary { WinnerName = winner.Name, MatchStatus = "win" });
                }
            }
            var summary = matchResult.GroupBy(p=>p.WinnerName).Select(r => new  { Winner = r.Key, Matches = r.ToList() }).ToList();
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("{0} Matches were palyed, here is the summary", totalMatches);
            foreach (var s in summary)
            {
                string winner = s.Winner;
                int winnings = s.Matches.Count();
                Console.WriteLine("{0} : {1}", s.Winner.ToString(), s.Matches.Count().ToString());

            }
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.ReadKey();
        }
    }

    public enum GameAction
    {
        Rock,
        Paper,
        Scissors
    }
    public class Player
    {
        public string Name { get; set; }
        public GameAction Bet { get; set; }
        public  GameAction Act()
        {
            return Bet;
        }
    }

    public class Battle
    {
        private readonly Player _player1;
        private readonly Player _player2;

        public Battle(Player player1, Player player2)
        {
            this._player1 = player1;
            this._player2 = player2;
        }

        public Player PlayMatchUp()
        {

            var result = WinningHand(_player1.Act(), _player2.Act());

            if (_player1.Act() == result)
            {
                return _player1;
            }

            if (_player2.Act() == result)
            {
                return _player2;
            }

            return null;
        }
        private GameAction? WinningHand(GameAction p1, GameAction p2)
        {
            if (p1 == GameAction.Paper && p2 == GameAction.Rock)
            {
                return GameAction.Paper;
            }

            if (p1 == GameAction.Paper && p2 == GameAction.Scissors)
            {
                return GameAction.Scissors;
            }

            if (p1 == GameAction.Scissors && p2 == GameAction.Paper)
            {
                return GameAction.Scissors;
            }

            if (p1 == GameAction.Scissors && p2 == GameAction.Rock)
            {
                return GameAction.Rock;
            }

            if (p1 == GameAction.Rock && p2 == GameAction.Paper)
            {
                return GameAction.Paper;
            }

            if (p1 == GameAction.Rock && p2 == GameAction.Scissors)
            {
                return GameAction.Rock;
            }
            return null;
        }
    }

    public class TournamentSummary
    {
        public string WinnerName { get; set; }
        public string MatchStatus { get; set; }
    }
}

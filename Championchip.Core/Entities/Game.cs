﻿namespace Championchip.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }
    }
}
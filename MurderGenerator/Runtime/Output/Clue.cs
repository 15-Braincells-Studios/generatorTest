using System.Collections.Generic;
using FifteenBraincells.MurderGenerator.Data;

namespace FifteenBraincells.MurderGenerator.Output
{
    /// <summary>Which table a clue came from.</summary>
    public enum ClueCategory
    {
        Method,
        Motive,
        Alibi,
        Contradiction,
        RedHerring,
        /// <summary>What the victim (or a decoy) does tonight. Not an alibi.</summary>
        Plan,
    }

    /// <summary>
    /// One clue in a level: a line a character says, or something lying in a room.
    /// The text is not finished yet: <see cref="Text.ClueTextRenderer"/> turns it into a string for a language.
    /// </summary>
    public sealed class Clue
    {
        internal Clue(ClueCategory category, string sourceId, ClueForm form, Character speaker, PlacedRoom room,
            string textKey, IReadOnlyDictionary<string, TextArg> args, ClueEffect effect, Character about = null,
            bool isFalseLead = false)
        {
            About = about;
            IsFalseLead = isFalseLead;
            Category = category;
            SourceId = sourceId;
            Form = form;
            Speaker = speaker;
            Room = room;
            TextKey = textKey;
            Args = args;
            Effect = effect;
        }

        public ClueCategory Category { get; }

        /// <summary>Id of the row in generator_data.json this clue was made from.</summary>
        public string SourceId { get; }

        public ClueForm Form { get; }

        /// <summary>Who says it. Null for objects.</summary>
        public Character Speaker { get; }

        /// <summary>Where it lies. Null for spoken lines.</summary>
        public PlacedRoom Room { get; }

        public string TextKey { get; }

        public IReadOnlyDictionary<string, TextArg> Args { get; }

        /// <summary>What the clue proves. Used by the solver; the player never sees this.</summary>
        public ClueEffect Effect { get; }

        public bool IsSpoken => Speaker != null;

        /// <summary>For alibis and plans: whose evening it is about.</summary>
        public Character About { get; }

        /// <summary>A line about the speaker's own evening (their alibi or plan). It comes first in a conversation.</summary>
        public bool IsAboutSpeaker => Speaker != null && Speaker == About;

        /// <summary>A loose contradiction about an alibi nobody has: it refutes nothing.</summary>
        public bool IsFalseLead { get; }

        public override string ToString() => $"{Category} {SourceId}";
    }
}

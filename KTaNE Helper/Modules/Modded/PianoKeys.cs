using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace KTaNE_Helper.Modules.Modded
{
    public class PianoKeys
    {
        
    }

    public static class NoteExtensions
    {
        public static IEnumerable<Note> Retrograde(this IEnumerable<Note> sequence)
        {
            return sequence.Reverse();
        }

        public static IEnumerable<Note> Inversion(this IEnumerable<Note> sequence)
        {
            int currentValue = -1;
            int lastSourceValue = -1;

            foreach (Note note in sequence)
            {
                int thisSourceValue = (int)note.Semitone;

                if (lastSourceValue != -1)
                {
                    int inversionDifference = lastSourceValue - thisSourceValue;
                    currentValue += inversionDifference;
                    while (currentValue < 0)
                    {
                        currentValue += 12;
                    }
                    currentValue %= 12;
                }
                else
                {
                    currentValue = thisSourceValue;
                }

                lastSourceValue = thisSourceValue;

                yield return new Note((Semitone)currentValue, note.Octave);
            }
        }

        public static IEnumerable<Note> Transpose(this IEnumerable<Note> sequence, int shift)
        {
            foreach (Note note in sequence)
            {
                int currentValue = ((int)note.Semitone) + shift;
                while (currentValue < 0)
                {
                    currentValue += 12;
                }
                currentValue %= 12;

                yield return new Note((Semitone)currentValue, note.Octave);
            }
        }
    }

    public class Note
    {
        public Note(Semitone semitone, int octave, bool useAlternate=false)
        {
            Semitone = semitone;
            Octave = octave;
            UseAlternate = useAlternate;
        }

        public Semitone Semitone;
        public int Octave;
        public bool UseAlternate;
    }

    public class Melody
    {
        public string Name;
        public Note[] Notes;

        public override string ToString()
        {
            return Name;
        }

        public Melody()
        {

        }

        public Melody(Melody input)
        {
            if (input == null)
                throw new NullReferenceException();
            if (input.Name != null)
                Name = string.Copy(input.Name);
            if (input.Notes != null)
                Notes = new List<Note>(input.Notes).ToArray();
        }
    }

    public static class MelodyDatabase
    {
        #region Regular Melodies
        public static readonly Melody FinalFantasyVictory = new Melody()
        {
            Name = "Final Fantasy Victory Fanfare",
            Notes = new[] { new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.FSharp, 3, true),
                new Note(Semitone.GSharp, 3, true),
                new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.GSharp, 3, true),
                new Note(Semitone.ASharp, 3, true)
            }
        };

        public static readonly Melody GuilesTheme = new Melody()
        {
            Name = "Guile's Theme",
            Notes = new[] { new Note(Semitone.DSharp, 3, true),
                new Note(Semitone.DSharp, 3, true),
                new Note(Semitone.D, 3),
                new Note(Semitone.D, 3),
                new Note(Semitone.DSharp, 3, true),
                new Note(Semitone.DSharp, 3, true),
                new Note(Semitone.D, 3),
                new Note(Semitone.DSharp, 3, true),
                new Note(Semitone.DSharp, 3, true),
                new Note(Semitone.D, 3),
                new Note(Semitone.D, 3),
                new Note(Semitone.DSharp, 3, true)
            }
        };

        public static readonly Melody JamesBond = new Melody()
        {
            Name = "James Bond Theme",
            Notes = new[] { new Note(Semitone.E, 3),
                new Note(Semitone.FSharp, 3),
                new Note(Semitone.FSharp, 3),
                new Note(Semitone.FSharp, 3),
                new Note(Semitone.FSharp, 3),
                new Note(Semitone.E, 3),
                new Note(Semitone.E, 3),
                new Note(Semitone.E, 3)
            }
        };

        public static readonly Melody JurrasicPark = new Melody()
        {
            Name = "Jurassic Park Theme",
            Notes = new[] { new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.A, 3),
                new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.F, 3),
                new Note(Semitone.DSharp, 3, true),
                new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.A, 3),
                new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.F, 3),
                new Note(Semitone.DSharp, 3, true)
            }
        };

        public static readonly Melody SuperMarioBros = new Melody()
        {
            Name = "Mario Bros. Overworld Theme",
            Notes = new[] { new Note(Semitone.E, 3),
                new Note(Semitone.E, 3),
                new Note(Semitone.E, 3),
                new Note(Semitone.C, 3),
                new Note(Semitone.E, 3),
                new Note(Semitone.G, 3),
                new Note(Semitone.G, 2)
            }
        };

        public static readonly Melody PinkPanther = new Melody()
        {
            Name = "The Pink Panther Theme",
            Notes = new[] { new Note(Semitone.CSharp, 3),
                new Note(Semitone.D, 3),
                new Note(Semitone.E, 3),
                new Note(Semitone.F, 3),
                new Note(Semitone.CSharp, 3),
                new Note(Semitone.D, 3),
                new Note(Semitone.E, 3),
                new Note(Semitone.F, 3),
                new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.A, 3)
            }
        };

        public static readonly Melody Superman = new Melody()
        {
            Name = "Superman Theme",
            Notes = new[] { new Note(Semitone.G, 3),
                new Note(Semitone.G, 3),
                new Note(Semitone.C, 3),
                new Note(Semitone.G, 3),
                new Note(Semitone.G, 3),
                new Note(Semitone.C, 4),
                new Note(Semitone.G, 3),
                new Note(Semitone.C, 3)
            }
        };

        public static readonly Melody TetrisA = new Melody()
        {
            Name = "Tetris Mode-A Theme",
            Notes = new[] { new Note(Semitone.A, 3),
                new Note(Semitone.E, 3),
                new Note(Semitone.F, 3),
                new Note(Semitone.G, 3),
                new Note(Semitone.F, 3),
                new Note(Semitone.E, 3),
                new Note(Semitone.D, 3),
                new Note(Semitone.D, 3),
                new Note(Semitone.F, 3),
                new Note(Semitone.A, 3)
            }
        };

        public static readonly Melody EmpireStrikesBack = new Melody()
        {
            Name = "The Empire Strikes Back Theme",
            Notes = new[] { new Note(Semitone.G, 3),
                new Note(Semitone.G, 3),
                new Note(Semitone.G, 3),
                new Note(Semitone.DSharp, 3, true),
                new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.G, 3),
                new Note(Semitone.DSharp, 3, true),
                new Note(Semitone.ASharp, 3, true),
                new Note(Semitone.G, 3)
            }
        };

        public static readonly Melody ZeldasLullaby = new Melody()
        {
            Name = "Zelda's Lullaby Theme",
            Notes = new[] { new Note(Semitone.B, 3),
                new Note(Semitone.D, 4),
                new Note(Semitone.A, 3),
                new Note(Semitone.G, 3),
                new Note(Semitone.A, 3),
                new Note(Semitone.B, 3),
                new Note(Semitone.D, 4),
                new Note(Semitone.A, 3)
            }
        };
        #endregion

        public static readonly Melody[] StandardMelodies =
        {
            FinalFantasyVictory, GuilesTheme, JamesBond, JurrasicPark,
            SuperMarioBros, PinkPanther, Superman, TetrisA,
            EmpireStrikesBack, ZeldasLullaby
        };

		#region Festive Melodies

		public static readonly Melody Rudolph = new Melody()
		{
			Name = "Rudolph The Red-Nosed Reindeer",
			Notes = new[] { new Note(Semitone.DSharp, 3, true),
								new Note(Semitone.F, 3),
								new Note(Semitone.DSharp, 3, true),
								new Note(Semitone.C, 3),
								new Note(Semitone.GSharp, 3, true),
								new Note(Semitone.F, 3),
								new Note(Semitone.DSharp, 3, true),
		}
		};

		public static readonly Melody WeThreeKings = new Melody()
		{
			Name = "We Three Kings Of Orient Are",
			Notes = new[] { new Note(Semitone.CSharp, 4),
								new Note(Semitone.B, 3),
								new Note(Semitone.A, 3),
								new Note(Semitone.FSharp, 3),
								new Note(Semitone.GSharp, 3),
								new Note(Semitone.A, 3),
								new Note(Semitone.GSharp, 3),
								new Note(Semitone.FSharp, 3),
		}
		};

		public static readonly Melody SilentNight = new Melody()
		{
			Name = "Silent Night",
			Notes = new[] { new Note(Semitone.G, 3),
								new Note(Semitone.A, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.E, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.A, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.E, 3),
		}
		};

		public static readonly Melody LastChristmas = new Melody()
		{
			Name = "Last Christmas",
			Notes = new[] { new Note(Semitone.DSharp, 3, true),
								new Note(Semitone.DSharp, 3, true),
								new Note(Semitone.CSharp, 3, true),
								new Note(Semitone.GSharp, 2, true),
								new Note(Semitone.DSharp, 3, true),
								new Note(Semitone.DSharp, 3, true),
								new Note(Semitone.F, 3),
								new Note(Semitone.CSharp, 3, true),
		}
		};

		public static readonly Melody AllIWantForChristmas = new Melody()
		{
			Name = "All I Want For Christmas Is You",
			Notes = new[] { new Note(Semitone.B, 3),
								new Note(Semitone.A, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.DSharp, 3, true),
								new Note(Semitone.D, 3),
								new Note(Semitone.A, 3),
								new Note(Semitone.B, 3),
								new Note(Semitone.A, 3),
								new Note(Semitone.G, 3),
		}
		};

		public static readonly Melody FrostyTheSnowman = new Melody()
		{
			Name = "Frosty The Snowman",
			Notes = new[] { new Note(Semitone.G, 3),
								new Note(Semitone.E, 3),
								new Note(Semitone.F, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.C, 4),
								new Note(Semitone.B, 3),
								new Note(Semitone.C, 4),
								new Note(Semitone.D, 4),
								new Note(Semitone.C, 4),
								new Note(Semitone.B, 3),
								new Note(Semitone.A, 3),
								new Note(Semitone.G, 3),
		}
		};

		public static readonly Melody MostWonderfulTime = new Melody()
		{
			Name = "It's The Most Wonderful Time Of The Year",
			Notes = new[] { new Note(Semitone.FSharp, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.A, 3),
								new Note(Semitone.A, 3),
								new Note(Semitone.D, 4),
								new Note(Semitone.B, 3),
								new Note(Semitone.A, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.E, 3),
								new Note(Semitone.D, 3),
		}
		};

		public static readonly Melody JingleBells = new Melody()
		{
			Name = "Jingle Bells",
			Notes = new[] { new Note(Semitone.G, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.G, 3),
								new Note(Semitone.ASharp, 3, true),
								new Note(Semitone.DSharp, 3, true),
								new Note(Semitone.F, 3),
								new Note(Semitone.G, 3),
		}
		};

		public static readonly Melody JingleBellRock = new Melody()
		{
			Name = "Jingle Bell Rock",
			Notes = new[] { new Note(Semitone.D, 4),
								new Note(Semitone.D, 4),
								new Note(Semitone.D, 4),
								new Note(Semitone.CSharp, 4),
								new Note(Semitone.CSharp, 4),
								new Note(Semitone.CSharp, 4),
								new Note(Semitone.B, 3),
								new Note(Semitone.CSharp, 4),
								new Note(Semitone.B, 3),
								new Note(Semitone.FSharp, 3),
		}
		};

		public static Melody GenerateCarolOfTheBells(int repetitions)
		{
			List<Note> fullNoteList = new List<Note>();
			Note[] oneRepeat = new[] { new Note(Semitone.ASharp, 3, true),
										new Note(Semitone.A, 3),
										new Note(Semitone.ASharp, 3, true),
										new Note(Semitone.G, 3),
		};

			for (int repetition = 0; repetition <= repetitions; ++repetition)
			{
				fullNoteList.AddRange(oneRepeat);
			}

			return new Melody()
			{
				Name = string.Format("Carol Of The Bells (x{0})", repetitions+1),
				Notes = fullNoteList.ToArray()
			};
		}
		#endregion

		public static readonly Melody[] FestiveMelodies =
	    {
			Rudolph, WeThreeKings, SilentNight,
			LastChristmas, AllIWantForChristmas, FrostyTheSnowman,
			MostWonderfulTime, JingleBells, JingleBellRock
	    };

		#region Serialism Melodies
		public static readonly Melody[] SerialismMelodies =
        {
            new Melody()
            {
                Name = "Serialism Tone Row 0",
                Notes = new[] { new Note(Semitone.F, 3),
                    new Note(Semitone.D, 3),
                    new Note(Semitone.FSharp, 3),
                    new Note(Semitone.GSharp, 3),
                    new Note(Semitone.C, 3),
                    new Note(Semitone.B, 3),
                    new Note(Semitone.ASharp, 3),
                    new Note(Semitone.CSharp, 3),
                    new Note(Semitone.G, 3),
                    new Note(Semitone.E, 3),
                    new Note(Semitone.DSharp, 3),
                    new Note(Semitone.A, 3)
                }
            },

            new Melody()
            {
                Name = "Serialism Tone Row 1",
                Notes = new[] { new Note(Semitone.ASharp, 3),
                    new Note(Semitone.A, 3),
                    new Note(Semitone.C, 3),
                    new Note(Semitone.E, 3),
                    new Note(Semitone.CSharp, 3),
                    new Note(Semitone.D, 3),
                    new Note(Semitone.DSharp, 3),
                    new Note(Semitone.G, 3),
                    new Note(Semitone.B, 3),
                    new Note(Semitone.FSharp, 3),
                    new Note(Semitone.GSharp, 3),
                    new Note(Semitone.F, 3)
                }
            },

            new Melody()
            {
                Name = "Serialism Tone Row 2",
                Notes = new[] { new Note(Semitone.FSharp, 3),
                    new Note(Semitone.B, 3),
                    new Note(Semitone.A, 3),
                    new Note(Semitone.GSharp, 3),
                    new Note(Semitone.D, 3),
                    new Note(Semitone.C, 3),
                    new Note(Semitone.G, 3),
                    new Note(Semitone.CSharp, 3),
                    new Note(Semitone.F, 3),
                    new Note(Semitone.DSharp, 3),
                    new Note(Semitone.E, 3),
                    new Note(Semitone.ASharp, 3)
                }
            },

            new Melody()
            {
                Name = "Serialism Tone Row 3",
                Notes = new[] { new Note(Semitone.E, 3),
                    new Note(Semitone.DSharp, 3),
                    new Note(Semitone.D, 3),
                    new Note(Semitone.FSharp, 3),
                    new Note(Semitone.F, 3),
                    new Note(Semitone.ASharp, 3),
                    new Note(Semitone.GSharp, 3),
                    new Note(Semitone.CSharp, 3),
                    new Note(Semitone.C, 3),
                    new Note(Semitone.B, 3),
                    new Note(Semitone.G, 3),
                    new Note(Semitone.A, 3)
                }
            },

            new Melody()
            {
                Name = "Serialism Tone Row 4",
                Notes = new[] { new Note(Semitone.D, 3),
                    new Note(Semitone.E, 3),
                    new Note(Semitone.A, 3),
                    new Note(Semitone.ASharp, 3),
                    new Note(Semitone.C, 3),
                    new Note(Semitone.B, 3),
                    new Note(Semitone.CSharp, 3),
                    new Note(Semitone.GSharp, 3),
                    new Note(Semitone.F, 3),
                    new Note(Semitone.FSharp, 3),
                    new Note(Semitone.DSharp, 3),
                    new Note(Semitone.G, 3)
                }
            },

            new Melody()
            {
                Name = "Serialism Tone Row 5",
                Notes = new[] { new Note(Semitone.C, 3),
                    new Note(Semitone.DSharp, 3),
                    new Note(Semitone.FSharp, 3),
                    new Note(Semitone.D, 3),
                    new Note(Semitone.F, 3),
                    new Note(Semitone.CSharp, 3),
                    new Note(Semitone.B, 3),
                    new Note(Semitone.A, 3),
                    new Note(Semitone.G, 3),
                    new Note(Semitone.ASharp, 3),
                    new Note(Semitone.E, 3),
                    new Note(Semitone.GSharp, 3)
                }
            },

            new Melody()
            {
                Name = "Serialism Tone Row 6",
                Notes = new[] { new Note(Semitone.GSharp, 3),
                    new Note(Semitone.C, 3),
                    new Note(Semitone.ASharp, 3),
                    new Note(Semitone.CSharp, 3),
                    new Note(Semitone.E, 3),
                    new Note(Semitone.G, 3),
                    new Note(Semitone.B, 3),
                    new Note(Semitone.DSharp, 3),
                    new Note(Semitone.A, 3),
                    new Note(Semitone.D, 3),
                    new Note(Semitone.F, 3),
                    new Note(Semitone.FSharp, 3)
                }
            },

            new Melody()
            {
                Name = "Serialism Tone Row 7",
                Notes = new[] { new Note(Semitone.E, 3),
                    new Note(Semitone.A, 3),
                    new Note(Semitone.CSharp, 3),
                    new Note(Semitone.B, 3),
                    new Note(Semitone.G, 3),
                    new Note(Semitone.GSharp, 3),
                    new Note(Semitone.ASharp, 3),
                    new Note(Semitone.DSharp, 3),
                    new Note(Semitone.FSharp, 3),
                    new Note(Semitone.F, 3),
                    new Note(Semitone.C, 3),
                    new Note(Semitone.D, 3)
                }
            },

            new Melody()
            {
                Name = "Serialism Tone Row 8",
                Notes = new[] { new Note(Semitone.GSharp, 3),
                    new Note(Semitone.DSharp, 3),
                    new Note(Semitone.D, 3),
                    new Note(Semitone.E, 3),
                    new Note(Semitone.ASharp, 3),
                    new Note(Semitone.CSharp, 3),
                    new Note(Semitone.FSharp, 3),
                    new Note(Semitone.G, 3),
                    new Note(Semitone.F, 3),
                    new Note(Semitone.A, 3),
                    new Note(Semitone.C, 3),
                    new Note(Semitone.B, 3)
                }
            },

            new Melody()
            {
                Name = "Serialism Tone Row 9",
                Notes = new[] { new Note(Semitone.DSharp, 3),
                    new Note(Semitone.GSharp, 3),
                    new Note(Semitone.C, 3),
                    new Note(Semitone.B, 3),
                    new Note(Semitone.D, 3),
                    new Note(Semitone.CSharp, 3),
                    new Note(Semitone.FSharp, 3),
                    new Note(Semitone.ASharp, 3),
                    new Note(Semitone.F, 3),
                    new Note(Semitone.G, 3),
                    new Note(Semitone.A, 3),
                    new Note(Semitone.E, 3)
                }
            }
        };
        #endregion
    }

    public enum Semitone
    {
        [Description("C")]
        C,
        [Description("C#/Db")]
        CSharp,
        [Description("D")]
        D,
        [Description("D#/Eb")]
        DSharp,
        [Description("E")]
        E,
        [Description("F")]
        F,
        [Description("F#/Gb")]
        FSharp,
        [Description("G")]
        G,
        [Description("G#/Ab")]
        GSharp,
        [Description("A")]
        A,
        [Description("A#/Bb")]
        ASharp,
        [Description("B")]
        B
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_form_01.Model
{
    public class GameScore
    {
        public string Name { get; set; }
        public TimeSpan Time { get; set; }

        public string TimeString
        {
            get
            {
                return Time.ToString(@"hh\:mm\:ss");
            }
        }

        public GameScore(string name, TimeSpan time)
        {
            this.Name = name;
            this.Time = time;
        }

        private static List<GameScore> scores;
        public static List<GameScore> Scores 
        {
            get
            {
                if (scores == null)
                {
                    scores = new List<GameScore>();
                }
                return scores;
            }
        }

        public static void AddScore(GameScore score)
        {
            Scores.Add(score);

            List<GameScore> sorted = Scores.OrderBy(o => o.Time).ToList();
            if (sorted.Count > 10)
            {
                sorted.RemoveAt(sorted.Count - 1);
            }

            scores = sorted;
        }

    }
}

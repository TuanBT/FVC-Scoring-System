using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Server;

namespace FSSServer.Data
{
    public static class TextDataUltil
    {
        private static string pathFighterText = "Fighter.txt";
        private static string pathMatchText = "Match.txt";

        public static List<Fighter> ReadFighterText()
        {
            try
            {
                Fighter fighter;
                var fighters = new List<Fighter>();
                string[] lines = File.ReadAllLines(pathFighterText);
                foreach (string line in lines)
                {
                    string[] cols = line.Split('\t');
                    fighter = new Fighter
                        {
                            FigId = cols[1],
                            FigName = cols[2],
                            FigClass = cols[3],
                            FigSex = cols[4],
                            FigWeight = cols[5]
                        };
                    fighters.Add(fighter);
                }
                return fighters;
            }
            catch (Exception)
            {
                MessageBox.Show("Có vấn đề với file " + pathFighterText + ". Kiểm tra xem file đã tồn tại hoặc có dữ liệu hay chưa?");
                return null;
            }
        }

        public static List<Match> ReadMatchText()
        {
            try
            {
                Match match;
                var matches = new List<Match>();
                string[] lines = File.ReadAllLines(pathMatchText);
                foreach (string line in lines)
                {
                    string[] cols = line.Split('\t');
                    match = new Match
                    {
                        MathId = cols[0],
                        FigIdRed = cols[1],
                        FigIdBlue = cols[2],
                        FigIdWin = cols[3]
                    };
                    matches.Add(match);
                }
                return matches;
            }
            catch (Exception)
            {
                MessageBox.Show("Có vấn đề với file " + pathMatchText + ". Kiểm tra xem file đã tồn tại hoặc có dữ liệu hay chưa?");
                return null;
            }
        }

        public static bool WriteMatchText()
        {
            try
            {
                //hàm sắp xếp Object MATCHES theo thứ tự tăng dần của MatId
                var matches = new List<Match>();
                for (int j = 1; j <= Variable.MATCHES.Count; j++)
                {
                    matches.Add(GetMatchFromMatchId(j.ToString()));
                }
                Variable.MATCHES = matches;

                var lines = new string[Variable.MATCHES.Count];
                int i = 0;
                foreach (var match in Variable.MATCHES)
                {
                    lines[i] = match.MathId + '\t' + match.FigIdRed + '\t' + match.FigIdBlue + '\t' + match.FigIdWin;
                    i++;
                }
                File.WriteAllLines(pathMatchText, lines);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        public static Match GetMatchFromMatchId(string matchId)
        {
            foreach (Match match in Variable.MATCHES)
            {
                if (match.MathId == matchId)
                {
                    return match;
                }
            }
            return null;
        }

        public static Fighter GetFighterFormFigId(string figId)
        {
            foreach (Fighter fighter in Variable.FIGHTERS)
            {
                if (fighter.FigId == figId)
                {
                    return fighter;
                }
            }
            return null;
        }

        public static bool InsertMatch(Match match)
        {
            try
            {
                Variable.MATCHES.Add(match);
                WriteMatchText();
                return true;
            }
            catch { }
            return false;
        }

        public static bool DeleteMatchByMatchId(string matchId)
        {
            try
            {
                Variable.MATCHES.Remove(GetMatchFromMatchId(matchId));
               // WriteMatchText();
                return true;
            }
            catch { }
            return false;
        }
    }
}

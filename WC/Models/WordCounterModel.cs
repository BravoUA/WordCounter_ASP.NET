using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using WordCounter.Models;

namespace WordCounter.Models
{
    public class WordCounterModel
    {
        private byte TopWordCount;
        private byte CountPosWord;
        private string Oll_WorldRegEx = @"\b\w+";
        private string Oll_2_WorldRegEx = @"\b(\w+)\s\b(\w+)+";
        private string Oll_3_WorldRegEx = @"(\b(\w+)\s\b(\w+))\s\b(\w+)";
        string[] Oll_World;
        string Text = "PDF document online is a web service that allows you to split your PDF document into separate pages. This simple application has several modes of operation, you can split your PDF document into separate pages, i.e. each page of the original document will be a separate PDF document, you can split your document into even and odd pages, this function will come in handy if you need to print a document in the form of a book, you can also specify page numbers in the settings and the Split PDF application will create separate PDF documents only with these pages and the fourth mode of operation allows you to create a new PDF document in which there will be only those pages that you specified";
        string Text2;
        bool Go = true;
        public List<WordModel> Resultat = new List<WordModel>();
       

        public WordCounterModel(byte TopW, byte CountPos,string Text) { TopWordCount = TopW; CountPosWord = CountPos; Text2 = Text; WorldCounter(); }

        void WorldCounter()
        {
            Regex regex = null;
            MatchCollection matches = null;
            WordModel[] WOR = null;
            if (TopWordCount == 1)
            {
                regex = new Regex(Oll_WorldRegEx);
                matches = regex.Matches(Text2.ToLowerInvariant());
                WOR = new WordModel[matches.Count];

                WordModel[] WORD = new WordModel[matches.Count];

                if (matches.Count > 0)
                {
                    int a = 0;
                    foreach (Match match in matches)
                    {
                        int counT = 0;
                        for (int i = 0; i < matches.Count; i++)
                        {
                            if (match.Value == matches[i].ToString())
                            {
                                counT++;
                            }
                        }

                        WORD[a] = new WordModel { Count = counT, World = match.Value };
                        a++;
                    }
                    a = 0;
                    List<string> WORLDS = new List<string>();
                    List<int> COUNT = new List<int>();

                    WORD = WORD.OrderByDescending(x => x.Count).ToArray();

                    for (int i = 0; i < WORD.Length; i++)
                    {


                        if (WORLDS.Contains(WORD[i].World)) { }
                        else
                        {
                            WORLDS.Add(WORD[i].World);
                            COUNT.Add(WORD[i].Count);

                            
                        }

                    }

                    for (int k = 0; k < CountPosWord; k++)
                    {
                        decimal rate = Rate(COUNT[k], matches.Count);

                        Resultat.Add(new WordModel() { World = WORLDS[k], Count = COUNT[k], WordRate = rate }); ;
                        Console.WriteLine((k + 1) + ". " + WORLDS[k].ToString() + " - " + COUNT[k].ToString() + " - " + rate + "%");
                    }


                }
                else
                    Console.WriteLine("Совпадений не найдено");
            }



            if (TopWordCount == 2)
            {
                regex = new Regex(Oll_WorldRegEx);

                matches = regex.Matches(Text2.ToLowerInvariant());
                WOR = new WordModel[matches.Count];



                List<string> CopleWords = new List<string>();

                if (matches.Count > 0)
                {


                    for (int i = 0; i < matches.Count; i++)
                    {
                        if (i + 1 >= matches.Count)
                        {

                        }
                        else
                        {

                            string copleword = "";
                            for (int j = 0; j < TopWordCount; j++)
                            {
                                copleword += matches[i].ToString();
                            }

                            CopleWords.Add(matches[i].ToString() + " " + matches[i + 1].ToString());

                        }
                        List<string> WORLDS = new List<string>();
                        List<int> COUNT = new List<int>();

                        var q = CopleWords.GroupBy(x => x)
                         .Select(g => new { Value = g.Key, Count = g.Count() })
                         .OrderByDescending(x => x.Count);

                        foreach (var x in q)
                        {
                            WORLDS.Add(x.Value);
                            COUNT.Add(x.Count);
                            
                        }
                        for (int k = 0; k < CountPosWord; k++)
                        {
                            decimal rate = Rate(COUNT[k], matches.Count);

                            Resultat.Add(new WordModel() { World = WORLDS[k], Count = COUNT[k] });

                            Console.WriteLine((k + 1) + ". " + WORLDS[k].ToString() + " - " + COUNT[k].ToString() + " - " + rate + "%");
                        }



                    }
                }
                else
                    Console.WriteLine("Совпадений не найдено");
            }





        }













        public decimal Rate(int a, int b)
        {
            int N = 1;
            decimal res = Convert.ToDecimal(a * 100) / Convert.ToDecimal(b);
            decimal X = decimal.Round(res, N, MidpointRounding.ToEven);

            return X;
        }

    }
}

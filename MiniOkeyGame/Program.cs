using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOkeyGame
{
    internal class Program
    {

        public class Player
        {
            public int id { get; set; }
            public string name { get; set; }

            public int winCount { get; set; }

            public bool isWinner = false;

            public List<int> pdeck = new List<int>();
        }

        static void Main(string[] args)
        {

            Random random = new Random();
            
            //int randomNumber = random.Next(0, 53);

            List<int> nonRandDeck = new List<int>();

            //desteyi yükle

            for (int j = 0; j <= 1; j++)
            {
                for (int i = 0; i <= 52; i++)
                {
                    nonRandDeck.Add(i);
                }
            }

            //desteyi karıştır
            List<int> deck = nonRandDeck.OrderBy(x => random.Next()).ToList();

            //gösterge seç
            int needle = deck[random.Next(0, deck.Count())];


            //çekilen taş gösterge olursa  tekrar çek
            while(needle % 52 != 0)
            {
                needle = deck[random.Next(0, deck.Count())];
            }

            //çekilen taş 13 ise okey 1 olur
            if(needle % 12  == 0)
            {
                needle = needle - 12;
            }
            else //çekilen taş yukarılardakinlerden başka herhangi bir sayı ise 1 ekle
            {
                needle += 1;
            }

            //kullanıcıları oluştur
            List<Player> playerList = new List<Player>()
            {
               new Player()
               {
                   id = 0,
                   name = "Player 1",
                   winCount = 0,
               },
               new Player()
               {
                   id = 1,
                   name = "Player 2",
                   winCount = 0,
               },
               new Player()
               {
                   id = 2,
                   name = "Player 3",
                   winCount = 0,
               },
               new Player()
               {
                   id = 3,
                   name = "Player 4",
                   winCount = 0,
               },
            };

            //kullanıcılara taşları dağıt

            //kimin başlayacağını seç
            int randPlayerIndex = random.Next(0,4);


            foreach(Player player in playerList)
            {
                //taşları playerlara dağıtır
                for (int l = 0; l < 14; l++)
                {
                    player.pdeck.Add(deck[0]);
                    deck.RemoveAt(0);
                }

                //eğer player seçilmiş kişi ise 1 taş fazla alur
                if(player.id == randPlayerIndex)
                {
                    player.pdeck.Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }

            //playerların ellerini kontrol et


            foreach (Player player in playerList)
            {

                player.pdeck.Sort();

                List<int> tmpdeck = player.pdeck;

                int count = 0;

                foreach(int tile in player.pdeck)
                {
                    foreach(int tile2 in tmpdeck) 
                    {
                        if((tile%12) == (tile2 % 12))
                        {
                            count++;
                        }
                    }
                }

                tmpdeck = player.pdeck.ToList();

                for(int i = 0; i < tmpdeck.Count - 2; i++)
                {
                    if((tmpdeck[i] == tmpdeck[i+1] -1) && (tmpdeck[i+1] == tmpdeck[i + 2] - 2))
                    {
                        count++;
                    }
                }


                player.winCount = count;
            }

            //kazanan belirleme
            int winId = 0;
            int winCount = 0;

            foreach(Player player in playerList)
            {
                if(player.winCount > winCount)
                {
                   winCount = player.winCount;
                   winId = player.id;
                }
            }

            playerList[winId].isWinner = true;

            //sonuç gösterme



            Console.WriteLine("Sonuç");
            Console.WriteLine("---------------");

            foreach (Player p in playerList)
            {
                Console.WriteLine("Oyuncu: "+p.name);
                Console.WriteLine("Istaka: ");
                foreach(int t in p.pdeck){
                    Console.Write(t.ToString()+" ");
                }
                Console.WriteLine("");

                string isWinner = "";

                if (p.isWinner)
                {
                    isWinner = "KAZANDI";
                }else
                {
                    isWinner = "KAZANAMADI";
                }
                

                Console.WriteLine("Kazandı mı :" + isWinner);

                Console.WriteLine("---------------");

            }

            Console.ReadLine();


        }
    }
}

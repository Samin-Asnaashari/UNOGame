using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Diagnostics;

namespace UNOService.Game
{
    public class Game
    {
        public int GameID { get; set; }
        public List<Player> Players { get; set; }
        public Stack<Card> Deck { get; set; }
        public Stack<Card> PlayedCards { get; set; }
        public Direction Direction { get; set; }
        private DatabaseHandler databaseHandler;
         
        public List<Card> databaseDeck { get; set; }
        public List<Move> moves { get; set; }

        private int nextPlayerTurn;
        public Player CurrentPlayer { get; private set; }
        private Player PreviousPlayer;
        private bool UnoSaidAlready;
        public int cardPickQueue;

        public Game(int gameID, List<Player> players , DatabaseHandler database)
        {
            //rand = new Random(GameID);
            this.GameID = gameID;
            this.Players = players;
            this.Deck = new Stack<Card>();
            this.PlayedCards = new Stack<Card>();
            this.Direction = Direction.clockwise;
            CurrentPlayer = Players[0];
            this.databaseHandler = database;
            createDeck();
            cardPickQueue = 0;

            moves = new List<Move>();
            databaseDeck = new List<Card>();
        }

        public void SendMessage(Player player, string message)
        {
            sendMessage(player.UserName, message);

            if (message.Length == 3 && message.ToLower().Contains("uno"))
            {
                if (!UnoSaidAlready)//prevent multiple unos and duplicate punishment
                {
                    UnoSaidAlready = true;
                    Uno(player);
                }
            }
        }

        private void sendMessage(string message)
        {
            foreach (Player player in Players)
            {
                player.IGameCallback.SendMessageGameCallback(message);
            }
        }

        private void sendMessage(string userNameSender, string message)
        {
            sendMessage($"{userNameSender}: {message}");
        }

        public void MakePreviousPlayerUnoSafe()
        {
            // Player is only safe when (next)player has played his turn.
            PreviousPlayer.UnoSaid = true;
        }

        private Player PlayerToBePunished()//Calculating player of a game based on the calling player parameter and depending on direction of the game
        {
            for (int i = 0; i < this.Players.Count; i++)
            {
                if (!this.Players[i].UnoSaid && this.Players[i].Hand.Count == 1)//assume that there is only one cause in the play card if no one says uno then the player that should have said should have his attribute unosaid set to true
                {
                    if (this.Direction == Direction.clockwise)
                    {
                        if (Players[(i + 1) % this.Players.Count].UserName == this.CurrentPlayer.UserName)
                            return this.Players[i];
                    }
                    else
                    {
                        if (this.Players[(i - 1 + Players.Count) % this.Players.Count].UserName == this.CurrentPlayer.UserName)
                            return this.Players[i];
                    }
                }
            }
            return null;
        }

        public void Uno(Player playerWhoCalledUno)
        {
            if (playerWhoCalledUno == PreviousPlayer && playerWhoCalledUno.Hand.Count == 1 && playerWhoCalledUno.UnoSaid == false) // Player called Uno on himself
            {
                playerWhoCalledUno.UnoSaid = true;
                sendMessage($"{playerWhoCalledUno.UserName} said Uno on themself");
            }
            else // Call Uno on other players
            {
                Player playerToBePunished = PlayerToBePunished();
                if (playerToBePunished != null)
                {
                    sendMessage($"{playerWhoCalledUno.UserName} said Uno on {playerToBePunished.UserName}");
                    giveCardsToPlayer(playerToBePunished, 2,Move.Types.PunishedCard);
                }
            }
            UnoSaidAlready = false;
        }

        public void ChooseNotToPlayCard(Player player)
        {
            // Make sure this only happen if the player has picked a card
            if (player.AlreadyPickedCards)
            {
                moves.Add(new Move(player.UserName, player.Game.GameID, null, Move.Types.Keep));
                if (PreviousPlayer != null)
                    if (PreviousPlayer.Hand.Count == 1 && !PreviousPlayer.UnoSaid)
                        MakePreviousPlayerUnoSafe();
                EndTurn();
            }
            else
            {
                Debug.WriteLine($"{player.UserName} tried to ChooseNotToPlayCard() but has not picked a card");
            }
        }

        public void InsertDeckIntoDatabase()
        {
            foreach (var item in this.databaseDeck)
            {
                databaseHandler.InsertCard(item,this.GameID);
            }
        }

        private bool isValidCard(Player player, Card cardtoplay)
        {
            Card tableCard = PlayedCards.Peek();
            // Only current player may play a card
            if (CurrentPlayer != player)
            {
                Debug.WriteLine($"{player.UserName} tried to play a card, but it was not their turn");
                return false;
            }

            if (!player.HasCard(cardtoplay))
            {
                Debug.WriteLine($"{player.UserName} tried to play a card, but it was not in their hand");
                return false;
            }

            // If there is a card draw queue, only 'attack' cards may be played
            if (cardPickQueue != 0)
            {
                if (cardtoplay.Type == CardType.draw4Wild || cardtoplay.Type == CardType.draw2)
                {
                    return true;
                }
                else
                {
                    Debug.WriteLine($"{player.UserName} tried to play a card, but it was not an attack card. Picking queue: {cardPickQueue}");
                    return false;
                }
            }

            // Wild cards can be played on any color
            if (cardtoplay.Type == CardType.draw4Wild || cardtoplay.Type == CardType.wild)
            {
                return true;
            }
            // Any matching color can be played, or matching numbers for normal cards
            else if (cardtoplay.Color == tableCard.Color || (cardtoplay.Type == CardType.normal && cardtoplay.Number == tableCard.Number))
            {
                return true;
            }
            // Any matching types can also be played
            else if (cardtoplay.Type == tableCard.Type && cardtoplay.Type != CardType.normal)
            {
                return true;
            }

            Debug.WriteLine($"{player.UserName} tried to play a card, but it was not a valid card for unknown reasons");
            return false;
        }

        public bool TryPlayCard(Player playerWhoPerformedAction, Card card)
        {
            if (isValidCard(playerWhoPerformedAction, card))
            {
                if(PreviousPlayer != null)
                    if(PreviousPlayer.Hand.Count == 1 && !PreviousPlayer.UnoSaid)
                        MakePreviousPlayerUnoSafe();

                cardAction(card);

                moves.Add(new Move(playerWhoPerformedAction.UserName,playerWhoPerformedAction.Game.GameID,card,Move.Types.Play));

                PlayedCards.Push(card);
                playerWhoPerformedAction.Remove(card);

                // Tell other users a card was played
                foreach (Player p in Players)
                {
                    if (p.UserName != playerWhoPerformedAction.UserName) //Prevent deadlock
                        p.IGameCallback.CardPlayed(card, playerWhoPerformedAction.UserName);
                }

                // Check end game condition
                if (playerWhoPerformedAction.Hand.Count() == 0)
                {
                    //game.End();
                    foreach (Player player in Players)
                    {
                        if (player != playerWhoPerformedAction) //Prevent deadlock
                            player.IGameCallback.EndOfTheGame(playerWhoPerformedAction.UserName);
                    }
                }

                EndTurn();

                return true;
            }

            return false;
        }

        private void cardAction(Card card)
        {
            switch (card.Type)
            {
                case CardType.draw2:
                    cardPickQueue += 2;
                    break;
                case CardType.draw4Wild:
                    cardPickQueue += 4;
                    break;
                case CardType.skip:
                    skip();
                    break;
                case CardType.reverse:
                    switchDirection();
                    break;
            }
        }

        public void GiveCardsToPlayer(Player player)
        {
            if (!player.AlreadyPickedCards)
            {
                if (cardPickQueue == 0)
                {
                    player.AlreadyPickedCards = true;
                    giveCardsToPlayer(player, 1, Move.Types.Take);
                }
                else
                {
                    giveCardsToPlayer(player, cardPickQueue, Move.Types.PunishedCard);
                    cardPickQueue = 0;
                    if(PreviousPlayer.Hand.Count == 1 & !PreviousPlayer.UnoSaid)
                        MakePreviousPlayerUnoSafe();
                    EndTurn();
                }
            }
            else
            {
                Debug.WriteLine($"{player.UserName} tried to pick a card but has already picked");
            }
        }

        private void giveCardsToPlayer(Player player, int amountOfCards, Move.Types moveType)
        {
            List<Card> cards = getCardsFromDeck(amountOfCards);
            player.Hand.AddRange(cards);
            player.IGameCallback.AssignCards(cards);

            foreach (var item in cards)
            {
                moves.Add(new Move(player.UserName, player.Game.GameID, item,moveType));
            }

            foreach (Player otherPlayer in Players)
            {
                if (otherPlayer != player)
                {
                    otherPlayer.IGameCallback.NotifyPlayersNumberOfCardsTaken(amountOfCards, player.UserName);
                }
            }

            // If player picks cards, uno is reset because they have more then one card.
            CurrentPlayer.UnoSaid = false;
        }

        private List<Card> getCardsFromDeck(int numberOfCardsToPick)
        {
            List<Card> pickedCards = new List<Card>();
            for (int i = 0; i < numberOfCardsToPick; i++)
            {
                if (Deck.Count == 0)
                {
                    refillDeck();
                }

                pickedCards.Add(Deck.Pop());
            }

            return pickedCards;
        }

        private Stack<Card> shuffle(Stack<Card> deckToShuffle)
        {
            List<Card> cards = deckToShuffle.ToList();

            Random r = new Random();

            for (int n = (cards.Count - 1); n > 0; --n)
            {
                int k = r.Next(n + 1);
                Card temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }

            return new Stack<Card>(cards);
        }

        private void createDeck()
        {
            List<CardColor> cardColors = new List<CardColor>();
            cardColors.Add(CardColor.Blue);
            cardColors.Add(CardColor.Green);
            cardColors.Add(CardColor.Red);
            cardColors.Add(CardColor.Yellow);

            List<CardType> cardTypes = new List<CardType>();
            cardTypes.Add(CardType.draw2);
            cardTypes.Add(CardType.reverse);
            cardTypes.Add(CardType.skip);

            for (int i = 0; i < 4; i++)//adding cards with only 4 ocurrences
            {
                Deck.Push(new Card(CardType.draw4Wild, CardColor.None, -1));
                Deck.Push(new Card(CardType.wild, CardColor.None, -1));
            }

            while (cardTypes.Count != 0)// adding the remaining 3 types
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int i2 = 0; i2 < 2; i2++)
                    {
                        Deck.Push(new Card(cardTypes[cardTypes.Count - 1], cardColors[i], -1));
                    }
                }
                cardTypes.RemoveAt(cardTypes.Count - 1);
            }

            for (int i = 0; i < 4; i++)//adding normal cards 0
            {
                Deck.Push(new Card(CardType.normal, cardColors[i], 0));
            }

            while (cardColors.Count != 0)//adding normal cards 1 to 9
            {
                for (int i = 1; i < 10; i++)
                {
                    Deck.Push(new Card(CardType.normal, cardColors[cardColors.Count - 1], i));
                    Deck.Push(new Card(CardType.normal, cardColors[cardColors.Count - 1], i));
                }

                cardColors.RemoveAt(cardColors.Count - 1);
            }

            this.Deck = shuffle(this.Deck);
            //foreach (var deckitem in this.Deck)
            //{
            //    databaseDeck.Add(deckitem);
            //}
        }

        public void EndTurn()
        {
            PreviousPlayer = CurrentPlayer;

            if (Direction == Direction.clockwise)
            {
                nextPlayerTurn = (nextPlayerTurn + 1) % Players.Count();
            }
            else
            {
                nextPlayerTurn = (nextPlayerTurn - 1 + Players.Count) % Players.Count();
            }

            CurrentPlayer = Players[nextPlayerTurn];
            CurrentPlayer.AlreadyPickedCards = false;

            // If player has one card at beginning of turn, he is safe from Uno
            //CurrentPlayer.UnoSaid = (CurrentPlayer.Hand.Count == 1);

            foreach (Player p in Players)
            {
                if (p.UserName != PreviousPlayer.UserName) // Prevent deadlock when skipping turn with two players.
                    p.IGameCallback.TurnChanged(CurrentPlayer);
            }
        }

        private void switchDirection()
        {
            if (Direction == Direction.clockwise)
            {
                Direction = Direction.counterClockwise;
            }
            else
            {
                Direction = Direction.clockwise;
            }
        }

        private void refillDeck()
        {
            Card lastCard = PlayedCards.Pop();
            PlayedCards = shuffle(PlayedCards);
            this.Deck = this.PlayedCards;
            this.PlayedCards = new Stack<Card>();
            PlayedCards.Push(lastCard);
        }

        private void skip()
        {
            if (Direction == Direction.clockwise)
            {
                nextPlayerTurn = (nextPlayerTurn + 1) % Players.Count();
            }
            else
            {
                nextPlayerTurn = (nextPlayerTurn - 1 + Players.Count) % Players.Count();
            }
        }

        public void StartGameReplay(Player player)
        {
            //List<string> playersUserNames = Players.Select(x => x.UserName).ToList();
            //moves = databaseHandler.GettMoves(player.Game.GameID); //GameID: Chosen Game to play  
        }

        public Move NextMove(int GameID)
        {
            Move m = moves[0];
            moves.Remove(m);
            return m;

        }
        //private void giveCardsToReplayPlayer(Player player, int amountOfCards)
        //{
        //    List<Card> cards = getCardsFromDeck(amountOfCards);
        //    player.Hand.AddRange(cards);

        //    //foreach (var item in cards)
        //    //{
        //    //    moves.Add(new Move(player.UserName, player.Game.GameID, item, moveType));
        //    //}

        //    foreach (Player selfPlayer in Players)
        //    {
        //        if (selfPlayer == player)
        //        {
        //            player.IGameCallback.AssignCards(cards);
        //            selfPlayer.IGameCallback.NotifyPlayersNumberOfCardsTaken(amountOfCards, player.UserName);
        //        }
        //    }

            // If player picks cards, uno is reset because they have more then one card.
           //CurrentPlayer.UnoSaid = false;
        //}

        public void StartGame()
        {
            List<string> playersUserNames = Players.Select(x => x.UserName).ToList();

            foreach (Player player in Players)
            {
                // TODO Make sure this value is 7, I keep changing it to test UNO
                player.IGameCallback.InitializeGame(playersUserNames);
            }

            foreach (Player player in Players)
            {
                giveCardsToPlayer(player, 7,Move.Types.Assigned);
            }

            do
            {
                PlayedCards.Push(Deck.Pop());
            }
            while (PlayedCards.Peek().Type != CardType.normal);


            foreach (Player player in Players)
            {
                player.IGameCallback.CardPlayed(PlayedCards.Peek(), "FirstAtStart");
                player.IGameCallback.TurnChanged(CurrentPlayer);
            }
        }
    }
}

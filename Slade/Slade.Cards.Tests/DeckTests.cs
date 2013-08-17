using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Slade.Cards.Tests
{
	[TestClass]
	public class DeckTests
	{
		[TestMethod]
		public void AddCard_SingleCard_DeckCardReferences()
		{
			var deck = new Deck();
			var card = new Card();

			deck.AddCard(card);

			Assert.IsNotNull(card.Deck);
			Assert.AreEqual(deck, card.Deck);
			Assert.AreEqual(1, deck.Cards.Count());
		}

		[TestMethod]
		public void AddCard_SingleCardAssignedToDeckA_CardMovedToDeckB()
		{
			var deckA = new Deck();
			var deckB = new Deck();
			var card = new Card();

			deckA.AddCard(card);
			deckB.AddCard(card);

			Assert.IsNotNull(card.Deck);
			Assert.AreEqual(deckB, card.Deck);
			Assert.AreEqual(0, deckA.Cards.Count());
			Assert.AreEqual(1, deckB.Cards.Count());
		}

		[TestMethod]
		public void RemoveCard_SingleCardAssignedToDeck_CardRemovedFromDeck()
		{
			var deck = new Deck();
			var card = new Card();

			deck.AddCard(card);
			bool result = deck.RemoveCard(card);
		}
	}
}
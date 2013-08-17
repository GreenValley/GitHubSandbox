using System;
using System.Collections.Generic;

namespace Slade.Cards
{
	/// <summary>
	/// Represents a deck of playing cards as represented by instances of the <see cref="Card"/> class.
	/// </summary>
	public class Deck
	{
		private readonly Dictionary<Guid, Card> mCardsById = new Dictionary<Guid, Card>();

		/// <summary>
		/// Provides access to the collection of cards contained within the deck.
		/// </summary>
		public IEnumerable<Card> Cards
		{
			get { return mCardsById.Values; }
		}

		/// <summary>
		/// Adds the given card to the collection of cards contained within the deck.
		/// </summary>
		/// <param name="card">The card to be added to the deck.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given card is null.</exception>
		/// <remarks>
		/// <para>
		/// If the given card is already located within another deck, it will be removed from that
		/// deck before being added to this one.
		/// </para>
		/// </remarks>
		public void AddCard(Card card)
		{
			VerificationProvider.VerifyNotNull(card, "card");

			if (card.Deck != null)
			{
				// The card is already associated with another deck, so remove it from that deck
				card.Deck.RemoveCard(card);
			}

			// Add the given card to the collection
			mCardsById.Add(card.CardId, card);

			// Re-assign the this deck to the given card
			card.Deck = this;
		}

		/// <summary>
		/// Removes the given card from the collection of cards contained within the deck.
		/// </summary>
		/// <param name="card">The card to be removed from the deck.</param>
		/// <returns>True if the given card was successfully removed from the deck; false otherwise.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the given card is null.</exception>
		public bool RemoveCard(Card card)
		{
			VerificationProvider.VerifyNotNull(card, "card");

			// Try to remove the card from the collection of cards
			if (mCardsById.Remove(card.CardId))
			{
				// Clear the deck instance on the card
				card.Deck = null;

				return true;
			}

			// If we got here, the card does not exist within this deck
			return false;
		}
	}
}
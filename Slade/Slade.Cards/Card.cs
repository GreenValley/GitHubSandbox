using System;

namespace Slade.Cards
{
	/// <summary>
	/// Represents a playing card from within a card game and deck.
	/// </summary>
	public class Card
	{
		private readonly Guid mCardId;

		/// <summary>
		/// Initializes a new instance of the <see cref="Card"/> class.
		/// </summary>
		public Card()
		{
			mCardId = Guid.NewGuid();
		}

		/// <summary>
		/// Gets the unique Id of the card instance.
		/// </summary>
		public Guid CardId
		{
			get { return mCardId; }
		}

		/// <summary>
		/// Gets or sets the deck in which the card can be found.
		/// </summary>
		public Deck Deck { get; set; }
	}
}
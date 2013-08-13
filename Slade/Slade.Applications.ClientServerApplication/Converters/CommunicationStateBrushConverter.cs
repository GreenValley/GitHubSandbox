using Slade.Applications.Converters;
using System.ServiceModel;
using System.Windows.Media;

namespace Slade.Applications.ClientServerApplication.Converters
{
    /// <summary>
    /// Converts a <see cref="CommunicationState"/> value into a pertinent <see cref="Brush"/> to represent the state.
    /// </summary>
    public class CommunicationStateBrushConverter : ValueConverterBase
    {
        private static readonly Color OpenColor = Colors.LightGreen;
        private static readonly Color ClosedColor = Colors.LightGray;
        private static readonly Color FaultedColor = Colors.LightPink;

        private static CommunicationStateBrushConverter sInstance;

        /// <summary>
        /// Provides access to the shared instance of the <see cref="CommunicationStateBrushConverter"/> class.
        /// </summary>
        public static CommunicationStateBrushConverter Instance
        {
            get { return sInstance ?? (sInstance = new CommunicationStateBrushConverter()); }
        }

        /// <summary>
        /// Converts the given <see cref="CommunicationState"/> value into a <see cref="Brush"/> pertinent to the state.
        /// </summary>
        /// <param name="value">The value of the communication state.</param>
        /// <param name="parameter">An optional parameter that is not supported for this type of conversion.</param>
        /// <returns>A colored brush object that represents the given communication state value.</returns>
        protected override object Convert(object value, object parameter)
        {
            return new SolidColorBrush(GetCommunicationStateColor((CommunicationState)value));
        }

        private static Color GetCommunicationStateColor(CommunicationState state)
        {
            switch (state)
            {
                case CommunicationState.Created:
                case CommunicationState.Opening:
                case CommunicationState.Opened:
                    return OpenColor;

                case CommunicationState.Closing:
                case CommunicationState.Closed:
                    return ClosedColor;

                case CommunicationState.Faulted:
                    return FaultedColor;

                default:
                    return Colors.Transparent;
            }
        }
    }
}
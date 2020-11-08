namespace RegExRazorReplace.Events
{
  using Prism.Events;

  using RegExRazorReplace.Data;

  /// <summary>The event which occurs when the parsing is completed.</summary>
  /// <seealso cref="Prism.Events.PubSubEvent{RegExRazorReplace.Data.ParseResult}" />
  internal class ParseCompleted : PubSubEvent<ParseResult>
  {
  }
}
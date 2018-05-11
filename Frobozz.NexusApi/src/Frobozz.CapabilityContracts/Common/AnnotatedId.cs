namespace Frobozz.Contracts.Common
{
    /// <summary>
    /// This type can be used when returning references to many objects that should be displayed in a list. The <see cref="Title"/>
    /// can be used as a user friendly way to represent the object and the <see cref="Id"/> can be used for further reference.
    /// </summary>
    public class AnnotatedId
    {
        /// <summary>
        /// The id that we have annotated.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The title for the item with the specified <see cref="Id"/>.
        /// </summary>
        public string Title { get; set; }
    }
}

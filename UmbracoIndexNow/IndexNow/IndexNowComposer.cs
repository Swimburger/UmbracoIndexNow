using Umbraco.Core;
using Umbraco.Core.Composing;

namespace UmbracoIndexNow.IndexNow
{
    public class IndexNowComposer : IUserComposer
    {
        public void Compose(Composition composition) => composition.Components().Append<IndexNowComponent>();
    }
}

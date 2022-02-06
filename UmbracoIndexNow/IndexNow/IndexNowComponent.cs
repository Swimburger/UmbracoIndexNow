using Umbraco.Core.Composing;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Trees;

namespace UmbracoIndexNow.IndexNow
{
    public class IndexNowComponent : IComponent
    {
        public void Initialize() => TreeControllerBase.MenuRendering += OnMenuRendering;

        public void Terminate() => TreeControllerBase.MenuRendering -= OnMenuRendering;

        private void OnMenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
        {
            if (sender.TreeAlias != "content")
            {
                return;
            }

            var content = sender.Umbraco.Content(int.Parse(e.NodeId));
            if (content == null)
            {
                return;
            }

            var contentType = content.ContentType;
            var compositions = contentType.CompositionAliases;
            if (compositions.Contains("contentBase") ||
                compositions.Contains("navigationBase") ||
                contentType.Alias == "home")
            {
                var i = new MenuItem("submitToIndexNow", "Submit To IndexNow");
                i.ExecuteJsMethod("submitToIndexNow(node)");
                i.Icon = "share-alt";
                e.Menu.Items.Add(i);
            }
        }
    }
}
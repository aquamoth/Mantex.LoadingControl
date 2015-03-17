using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

public static class ScriptsHtmlHelpers
{
    public static string RequireScript(this HtmlHelper html, string path, int priority = 1)
    {
        var requiredScripts = HttpContext.Current.Items["RequiredScripts"] as List<ResourceInclude>;
        if (requiredScripts == null) HttpContext.Current.Items["RequiredScripts"] = requiredScripts = new List<ResourceInclude>();
        if (!requiredScripts.Any(i => i.Path == path)) requiredScripts.Add(new ResourceInclude() { Path = path, Priority = priority });
        return null;
    }

    public static HtmlString EmitRequiredScripts(this HtmlHelper html)
    {
        var requiredScripts = HttpContext.Current.Items["RequiredScripts"] as List<ResourceInclude>;
        if (requiredScripts == null) return null;
        StringBuilder sb = new StringBuilder();
        foreach (var item in requiredScripts.OrderByDescending(i => i.Priority))
        {
            var bundle = System.Web.Optimization.BundleTable.Bundles.GetBundleFor(item.Path);
            if (bundle != null)
            {
                sb.AppendLine(System.Web.Optimization.Scripts.Render(item.Path).ToHtmlString());
            }
            else
            {
                sb.AppendFormat("<script src=\"{0}\" type=\"text/javascript\"></script>\n", VirtualPathUtility.ToAbsolute(item.Path));
            }
        }
        return new HtmlString(sb.ToString());
    }

    public static string RequireStyle(this HtmlHelper html, string path, int priority = 1)
    {
        var requiredStyles = HttpContext.Current.Items["RequiredStyles"] as List<ResourceInclude>;
        if (requiredStyles == null) HttpContext.Current.Items["RequiredStyles"] = requiredStyles = new List<ResourceInclude>();
        if (!requiredStyles.Any(i => i.Path == path)) requiredStyles.Add(new ResourceInclude() { Path = path, Priority = priority });
        return null;
    }

    public static HtmlString EmitRequiredStyles(this HtmlHelper html)
    {
        var requiredStyles = HttpContext.Current.Items["RequiredStyles"] as List<ResourceInclude>;
        if (requiredStyles == null) return null;
        StringBuilder sb = new StringBuilder();
        foreach (var item in requiredStyles.OrderByDescending(i => i.Priority))
        {
            var bundle = System.Web.Optimization.BundleTable.Bundles.GetBundleFor(item.Path);
            if (bundle != null)
            {
                sb.AppendLine(System.Web.Optimization.Styles.Render(item.Path).ToHtmlString());
            }
            else
            {
                sb.AppendFormat("<link rel=\"stylesheet\" href=\"{0}\">\n", VirtualPathUtility.ToAbsolute(item.Path));
            }
        }
        return new HtmlString(sb.ToString());
    }

    public class ResourceInclude
    {
        public string Path { get; set; }
        public int Priority { get; set; }
    }




    //public static IHtmlString HiddenInputFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes = null)
    //{
    //    ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
    //    var memberExpression = (MemberExpression)expression.Body;
    //    string fullID = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(memberExpression.Member.Name);
    //    var builder = new TagBuilder("input");
    //    builder.MergeAttribute("type", "hidden");
    //    var value = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
    //    builder.MergeAttribute("value", value.ToString());
    //    string fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
    //    builder.MergeAttribute("name", fullName);
    //    builder.GenerateId(fullID);
    //    var tag = builder.ToString(TagRenderMode.SelfClosing);
    //    return new HtmlString(tag);
    //}
}

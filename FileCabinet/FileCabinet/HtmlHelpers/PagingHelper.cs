using FileCabinet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace FileCabinet.HtmlHelpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
                                             PagingInfo pagingInfo,
                                             Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString RatingStars(this HtmlHelper html,
                                             Article article)
        {
            if (WebSecurity.CurrentUserId == -1)
            {
                TagBuilder tag = new TagBuilder("span");
                tag.SetInnerText("Зарегистрируйся, чтобы поставить оценку");
                return MvcHtmlString.Create(tag.ToString());
            }
            StringBuilder result = new StringBuilder();
            if(article.Marks.FirstOrDefault(x => x.UserProfileId == WebSecurity.CurrentUserId) == null)
                for (int i = 1; i <= 5; i++)
                {
                    TagBuilder tag = new TagBuilder("span");
                    tag.SetInnerText("\u2606");
                    result.Append(tag.ToString());
                }
            else
            {
                //int value = 6 - article.Marks.FirstOrDefault(x => x.UserProfileId == WebSecurity.CurrentUserId).Value;
                double value = 6 - article.Marks.Average(x => x.Value);
                value = value - (int)value >= 0.5 ? value - 1 : value;
                for (int i = 1; i < value; i++)
                {
                    TagBuilder tag = new TagBuilder("span");
                    tag.SetInnerText("\u2606");
                    result.Append(tag.ToString());
                    
                }
                for(double i = value; i <= 5; i++)
                {
                    TagBuilder tag = new TagBuilder("span");
                    tag.SetInnerText("\u2605");
                    tag.Attributes["style"] = "color:gold";                    
                    result.Append(tag.ToString());
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }
        public static MvcHtmlString AverageRating(this HtmlHelper html,
                                        Article article )
        {
            TagBuilder tag = new TagBuilder("div");
            try
            {
                tag.SetInnerText(String.Format("|{0}|", article.Marks.Average(x => x.Value)));
            }
            catch
            {
                tag.SetInnerText("|0|");
            }
            tag.AddCssClass("pull-right");
            tag.Attributes["style"] = "color:red;margin-left:20px;";
            return MvcHtmlString.Create(tag.ToString());
        }
    }
}
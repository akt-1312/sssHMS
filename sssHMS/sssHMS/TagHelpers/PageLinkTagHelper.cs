using sssHMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; }
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public string PageFirst { get; set; }
        public string PageLast { get; set; }
        public string PageNext { get; set; }
        public string PagePrev { get; set; }
        public string DisabledPage { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            string url = "";

            TagBuilder tagFastPage = new TagBuilder("a");
            TagBuilder tagFirst = new TagBuilder("i");

            url = PageModel.urlParam.Replace(":", 1.ToString());
            tagFastPage.Attributes["href"] = url;
            if (PageClassesEnabled)
            {
                tagFirst.AddCssClass(PageFirst);
                tagFastPage.AddCssClass(PageClass);
                tagFastPage.AddCssClass(PageClassNormal);
                tagFastPage.InnerHtml.AppendHtml(tagFirst);

                result.InnerHtml.AppendHtml(tagFastPage);
            }


            TagBuilder tagPrev = new TagBuilder("a");
            TagBuilder tagBackWard = new TagBuilder("i");

            int prevData = PageModel.CurrentPage - 1;
            if (PageModel.CurrentPage - 1 < 1)
            {
                prevData = 1;
            }
            url = PageModel.urlParam.Replace(":", prevData.ToString());
            tagPrev.Attributes["href"] = url;
            if (PageClassesEnabled)
            {
                tagBackWard.AddCssClass(PagePrev);
                tagPrev.AddCssClass(PageClass);
                tagPrev.AddCssClass(PageClassNormal);
                tagPrev.InnerHtml.AppendHtml(tagBackWard);
                
                result.InnerHtml.AppendHtml(tagPrev);
            }
            if (PageModel.PagerSize > PageModel.totalPage)
            {
                for (int i = 1; i <= PageModel.totalPage; i++)
                {
                    TagBuilder tag = new TagBuilder("a");

                    url = PageModel.urlParam.Replace(":", i.ToString());
                    tag.Attributes["href"] = url;
                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }
                    tag.InnerHtml.Append(i.ToString());
                    result.InnerHtml.AppendHtml(tag);
                }
            }
            else
            {

                if (PageModel.CurrentPage < PageModel.PagerSize-1)
                {
                    for (int i = 1; i <= PageModel.PagerSize; i++)
                    {
                        TagBuilder tag = new TagBuilder("a");

                        url = PageModel.urlParam.Replace(":", i.ToString());
                        tag.Attributes["href"] = url;
                        if (PageClassesEnabled)
                        {
                            tag.AddCssClass(PageClass);
                            tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                        }
                        tag.InnerHtml.Append(i.ToString());
                        result.InnerHtml.AppendHtml(tag);
                    }
                }



                else if (PageModel.CurrentPage >= PageModel.PagerSize-1 && PageModel.CurrentPage < PageModel.totalPage - 1)
                {
                    int lr = (int)Math.Ceiling((decimal)PageModel.PagerSize / 2);
                    for (int i = PageModel.CurrentPage - (lr - 1); i <= PageModel.CurrentPage + (lr - 1); i++)
                    {
                        TagBuilder tag = new TagBuilder("a");

                        url = PageModel.urlParam.Replace(":", i.ToString());
                        tag.Attributes["href"] = url;
                        if (PageClassesEnabled)
                        {
                            tag.AddCssClass(PageClass);
                            tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                        }
                        tag.InnerHtml.Append(i.ToString());
                        result.InnerHtml.AppendHtml(tag);

                    }

                }

                else if (PageModel.CurrentPage >= PageModel.totalPage - 1)
                {
                    for (int i = PageModel.totalPage - (PageModel.PagerSize - 1); i <= PageModel.totalPage; i++)
                    {
                        TagBuilder tag = new TagBuilder("a");

                        url = PageModel.urlParam.Replace(":", i.ToString());
                        tag.Attributes["href"] = url;
                        if (PageClassesEnabled)
                        {
                            tag.AddCssClass(PageClass);
                            tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                        }
                        tag.InnerHtml.Append(i.ToString());
                        result.InnerHtml.AppendHtml(tag);

                    }
                }

            }



            TagBuilder tagNext = new TagBuilder("a");
            TagBuilder tagForWard = new TagBuilder("i");

            int nextData = PageModel.CurrentPage + 1;
            if (PageModel.CurrentPage + 1 > PageModel.totalPage)
            {
                nextData = PageModel.totalPage;
            }

            url = PageModel.urlParam.Replace(":", nextData.ToString());
            tagNext.Attributes["href"] = url;
            if (PageClassesEnabled /*&& PageModel.CurrentPage != PageModel.totalPage*/)
            {
                tagForWard.AddCssClass(PageNext);
                tagNext.AddCssClass(PageClass);
                tagNext.AddCssClass(PageClassNormal);
                tagNext.InnerHtml.AppendHtml(tagForWard);
                result.InnerHtml.AppendHtml(tagNext);
            }


            TagBuilder tagLastPage = new TagBuilder("a");
            TagBuilder tageLast = new TagBuilder("i");
            url = PageModel.urlParam.Replace(":", PageModel.LastPage.ToString());
            tagLastPage.Attributes["href"] = url;
            if (PageClassesEnabled /*&& PageModel.CurrentPage!=PageModel.totalPage*/)
            {
                tageLast.AddCssClass(PageLast);
                tagLastPage.AddCssClass(PageClass);
                tagLastPage.AddCssClass(PageClassNormal);
                tagLastPage.InnerHtml.AppendHtml(tageLast);
                result.InnerHtml.AppendHtml(tagLastPage);
            }
            if (PageModel.CurrentPage == 1)
            {

                tagFastPage.AddCssClass(DisabledPage);
                tagPrev.AddCssClass(DisabledPage);
            }
            if (PageModel.CurrentPage == 0)
            {

                tagFastPage.AddCssClass(DisabledPage);
                tagPrev.AddCssClass(DisabledPage);
                tagLastPage.AddCssClass(DisabledPage);
                tagNext.AddCssClass(DisabledPage);
            }

            if (PageModel.CurrentPage == PageModel.totalPage)
            {

                tagLastPage.AddCssClass(DisabledPage);
                tagNext.AddCssClass(DisabledPage);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}

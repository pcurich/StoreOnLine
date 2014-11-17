//using System;
//using System.Globalization;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Mvc.Html;
//using StoreOnLine.DataBase.CMS;
//using StoreOnLine.DataBase.Entities;
//using StoreOnLine.Service.Business;
//using StoreOnLine.Service.Security;
//using StoreOnLine.Service.Services;

//namespace StoreOnLine.Service.Helpers
//{

//    /// <summary>
//    /// Esta clase hace varias cosas tales como obtener el HomePage Id, 
//    /// genera los menus, submenus, obtener breakcumb links 
//    /// botonoes sociales, etc, Aca se puede encontrar los menus con las clases de menu 
//    /// </summary>
//    public static class HtmlHelpers
//    {
//        private static StoreOnLineContext db = new StoreOnLineContext();
//        private static PageService pageservice = new PageService();
//        private static LanguageService languageservice = new LanguageService();
//        private static CacheMemory cache = new CacheMemory();

//        public static string GetHomePage()
//        {
//            var homeId = -1;
//            var f = from p in db.Pages.Where(p => p.ParentId == -1 && p.IncludeInMenu)
//                    orderby p.PageOrder
//                    ascending
//                    select new { p.Id, p.Url };

//            if (f.Any())
//                homeId = f.First().Id;

//            if (homeId == -1)
//                return "/";

//            return string.IsNullOrEmpty(f.First().Url) ?
//                string.Format("/{0}", SeoUrl.GetSeoUrl(homeId)) :
//                f.First().Url;
//        }

//        public static int GetFirstId()
//        {
//            var f = from p in db.Pages.Where(p => p.ParentId == -1 && p.IncludeInMenu)
//                    orderby p.PageOrder
//                    ascending
//                    select new { p.Id };

//            return f.Any() ? f.First().Id : 0;
//        }

//        public static HtmlString GetrefererUrl(this HtmlHelper helper)
//        {
//            var redirectUrl = HttpContext.Current.Request.UrlReferrer != null ?
//                              HttpContext.Current.Request.UrlReferrer.PathAndQuery : "#";

//            return new HtmlString(redirectUrl);
//        }

//        public static HtmlString Help(this HtmlHelper helper, int id, bool mouseOver)
//        {
//            string sid = (HttpContext.Current.Request.RawUrl + "-" + id).Substring(1);
//            var builder = new TagBuilder("img");
//            builder.GenerateId(sid);
//            builder.AddCssClass("pointer");
//            builder.MergeAttribute(mouseOver ? "mouseover" : "onclick", "showhelp('" + sid + "')");
//            builder.MergeAttribute("src", "/Content/images/site/help.gif");
//            builder.MergeAttribute("title", "Click for help");
//            builder.MergeAttribute("alt", "help");
//            return new HtmlString(builder.ToString(TagRenderMode.SelfClosing));
//        }

//        public static HtmlString GetBackLink(this HtmlHelper helper)
//        {
//            string redirectUrl;
//            if (HttpContext.Current.Request.UrlReferrer != null)
//                redirectUrl = HttpContext.Current.Request.UrlReferrer.PathAndQuery;
//            else
//                redirectUrl = "#";
//            return new HtmlString("<a href='" + redirectUrl + "' style='text-decoration:none'>Return</a>");
//        }

//        public static HtmlString GetSettings(this HtmlHelper helper, string settingKey)
//        {
//            return new HtmlString(SiteSettingString.GetString(settingKey));
//        }

//        public static HtmlString GetProperties(this HtmlHelper helper, string resourceType, string resourceKey)
//        {
//            return new HtmlString(PropertyString.GetString(resourceType, resourceKey));
//        }

//        public static HtmlString GetResources(this HtmlHelper helper, string resourceType, string resourceKey)
//        {
//            return new HtmlString(ResourceString.GetString(resourceType, resourceKey));
//        }

//        public static HtmlString HeadMenu(this HtmlHelper helper, int parentId, bool isGenerateSubMenu)
//        {
//            //var hm = from p in db.pages where p.SiteId == SiteId && p.ParentId == ParentId && p.IncludeInMenu == true orderby p.PageOrder ascending select p;
//            var hm = pageservice.GetPageSiteListParent(parentId);
//            string ul = "<ul class=\"art-menu\">\n\t";
//            foreach (var r in hm)
//            {
//                if (Role.IsViewRole(r.ViewRoles))
//                {
//                    //if(SiteSettingString.GetBoolean("NotViewRoleHideMenu")
//                    if (string.IsNullOrEmpty(r.Url))
//                        ul += "<li>" + string.Format("<a href='/{0}'><span class='l'></span><span class='r'></span><span id='{1}' class='t edit-page'>{2}</span></a>", SeoUrl.GetSeoUrl(r.Id), r.Id, PropertyString.GetString("Page", r.Id.ToString()));
//                    else
//                        ul += "<li>" + string.Format("<a href='{0}'><span class='l'></span><span class='r'></span><span id='{1}' class='t edit-page'>{2}</span></a>", r.Url, r.Id, PropertyString.GetString("Page", r.Id.ToString()));
//                    if (isGenerateSubMenu)
//                        ul += SubHeadMenu(r.Id);
//                    ul += "</li>\n\t";
//                }
//            }
//            ul += "</ul>";
//            return new HtmlString(ul);
//        }

//        //render sub Header Menu if need
//        private static string SubHeadMenu(int parentId)
//        {
//            string li = "";
//            bool ul = false;
//            //var shm = from p in db.pages where p.ParentId == ParentId && p.IncludeInMenu == true orderby p.PageOrder ascending select p;
//            var shm = pageservice.GetPageSiteListChild(parentId);
//            foreach (var r in shm)
//            {
//                if (Role.IsViewRole(r.ViewRoles))
//                {
//                    ul = true;
//                    if (string.IsNullOrEmpty(r.Url))
//                        li += "<li>" + string.Format("<a href='/{0}' id='{1}' class='edit-page'>{2}</a>", SeoUrl.GetSeoUrl(r.Id), r.Id, PropertyString.GetString("Page", r.Id.ToString()));
//                    else
//                        li += "<li>" + string.Format("<a href='{0}' id='{1}' class='edit-page'>{2}</a>", r.Url, r.Id, PropertyString.GetString("Page", r.Id.ToString()));
//                    li += SubHeadMenu(r.Id);
//                    li += "</li>\n\t";
//                }
//            }
//            if (ul)
//                li = "\n\t<ul>\n\t" + li + "</ul>";
//            return li;
//        }

//        //render Side Menu
//        public static HtmlString SideMenu(this HtmlHelper helper, int parentId, string menuHeader)
//        {
//            var sm = pageservice.GetPageSiteListChild(parentId);

//            if (!sm.Any())
//                return null;

//            string ul = "";
//            if (!string.IsNullOrEmpty(menuHeader))
//            {
//                ul = "<div class='art-vmenublock'>\n" +
//                "<div class='art-vmenublock-tl'></div>\n" +
//                "<div class='art-vmenublock-tr'></div>\n" +
//                "<div class='art-vmenublock-bl'></div>\n" +
//                "<div class='art-vmenublock-br'></div>\n" +
//                "<div class='art-vmenublock-tc'></div>\n" +
//                "<div class='art-vmenublock-bc'></div>\n" +
//                "<div class='art-vmenublock-cl'></div>\n" +
//                "<div class='art-vmenublock-cr'></div>\n" +
//                "<div class='art-vmenublock-cc'></div>\n" +
//                "<div class='art-vmenublock-body'>\n" +
//                "<div class='art-vmenublockheader'>\n" +
//                "<div class='l'></div>\n" +
//                "<div class='r'></div>\n" +
//                "<h3 class='t'>" + menuHeader + "</h3>\n" +
//                "</div>\n" +
//                "<div class='art-vmenublockcontent'><div class='art-vmenublockcontent-body'><ul class=\"art-vmenu\">\n\t";
//                foreach (var r in sm)
//                {
//                    if (Role.IsViewRole(r.ViewRoles))
//                    {
//                        if (string.IsNullOrEmpty(r.Url))
//                            ul += "<li>" + string.Format("<a href='/{0}'><span class='l'></span><span class='r'></span><span id='{1}' class='t edit-page'>{2}</span></a>", SeoUrl.GetSeoUrl(r.Id), r.Id, PropertyString.GetString("Page", r.Id.ToString()));
//                        else
//                            ul += "<li>" + string.Format("<a href='{0}'><span class='l'></span><span class='r'></span><span id='{1}' class='t edit-page'>{2}</span></a>", r.Url, r.Id, PropertyString.GetString("Page", r.Id.ToString()));
//                        ul += subSideMenu(r.Id, parentId);
//                        ul += "</li>\n\t";
//                    }
//                }
//                ul += "</ul><div class='cleared'></div></div></div><div class='cleared'></div></div></div>";
//            }
//            else
//            {
//                ul = "<div class='art-vmenublock'>\n" +
//                "<div class='art-vmenublock-tl'></div>\n" +
//                "<div class='art-vmenublock-tr'></div>\n" +
//                "<div class='art-vmenublock-bl'></div>\n" +
//                "<div class='art-vmenublock-br'></div>\n" +
//                "<div class='art-vmenublock-tc'></div>\n" +
//                "<div class='art-vmenublock-bc'></div>\n" +
//                "<div class='art-vmenublock-cl'></div>\n" +
//                "<div class='art-vmenublock-cr'></div>\n" +
//                "<div class='art-vmenublock-cc'></div>\n" +
//                "<div class='art-vmenublock-body'>\n" +
//                "<div class='art-vmenublockcontent'><div class='art-vmenublockcontent-body'><ul class=\"art-vmenu\">\n\t";
//                //else
//                //    ul = "<div class='art-vmenublock'><div class='art-vmenublock-body'><div class='art-vmenublockcontent'><div class='art-vmenublockcontent-body'><ul class=\"art-vmenu\">\n\t";
//                foreach (var r in sm)
//                {
//                    if (Role.IsViewRole(r.ViewRoles))
//                    {
//                        if (string.IsNullOrEmpty(r.Url))
//                            ul += "<li>" + string.Format("<a href='/{0}'><span class='l'></span><span class='r'></span><span id='{1}' class='t edit-page'>{2}</span></a>", SeoUrl.GetSeoUrl(r.Id), r.Id, PropertyString.GetString("Page", r.Id.ToString()));
//                        else
//                            ul += "<li>" + string.Format("<a href='{0}'><span class='l'></span><span class='r'></span><span id='{1}' class='t edit-page'>{2}</span></a>", r.Url, r.Id, PropertyString.GetString("Page", r.Id.ToString()));
//                        ul += subSideMenu(r.Id, parentId);
//                        ul += "</li>\n\t";
//                    }
//                }
//                ul += "</ul><div class='cleared'></div></div></div><div class='cleared'></div></div></div>";
//            }
//            return new HtmlString(ul);
//        }

//        //Render sub Side Menu
//        private static string subSideMenu(int ParentId, int Id)
//        {
//            string li = "";
//            bool ul = false;
//            //var ssm = from p in db.pages where p.ParentId == ParentId && p.IncludeInMenu == true orderby p.PageOrder ascending select p;
//            var ssm = pageservice.GetPageSiteListChild(ParentId);
//            foreach (var r in ssm)
//            {
//                if (Role.IsViewRole(r.ViewRoles))
//                {
//                    ul = true;
//                    if (string.IsNullOrEmpty(r.Url))
//                        li += "<li>" + string.Format("<a href='/{0}' id='{1}' class='edit-page'>{2}</a>", SeoUrl.GetSeoUrl(r.Id), r.Id, PropertyString.GetString("Page", r.Id.ToString()));
//                    else
//                        li += "<li>" + string.Format("<a href='{0}' id='{1}' class='edit-page'>{2}</a>", r.Url, r.Id, PropertyString.GetString("Page", r.Id.ToString()));
//                    li += subSideMenu(r.Id, Id);
//                    li += "</li>\n\t";
//                }
//            }
//            if (ul)
//                li = "\n\t<ul>\n\t" + li + "</ul>";
//            return li;
//        }

//        public static HtmlString GetPageList(this HtmlHelper helper, int parentId)
//        {
//            return new HtmlString(PageList(parentId));
//        }
//        public static HtmlString GetRoleList(this HtmlHelper helper)
//        {
//            //string roleList = "";
//            //foreach (var r in db.Roles)
//            //{
//            //    roleList += "<span class='node pointer' rel='" + r.RoleName + "'>" + r.RoleName + "</span><br/>";
//            //}
//            string roleList = Enumerable.Aggregate(db.Roles, "", (current, r) => current + ("<span class='node pointer' rel='" + r.RoleName + "'>" + r.RoleName + "</span><br/>"));
//            return new HtmlString(roleList);
//        }
//        public static HtmlString getUserList(this HtmlHelper helper, string UserName)
//        {
//            string RoleList = "";
//            if (string.IsNullOrEmpty(UserName))
//            {
//                var u = db.UsersCms;
//                RoleList = Enumerable.Aggregate(u, RoleList, (current, r) => current + ("<span class='node pointer' rel='" + r.Email + "'>" + r.UserName + "</span><br/>"));
//            }
//            else
//            {
//                var u = db.UsersCms.Where(c => c.UserName.Contains(UserName));
//                foreach (var r in u)
//                {
//                    RoleList += "<span class='node' rel='" + r.Email + "'>" + r.UserName + "</span><br/>";
//                }
//            }

//            return new HtmlString(RoleList);
//        }
//        private static string PageList(int parentId)
//        {
//            var li = "";
//            var ul = false;
//            var shm = from p in db.Pages
//                      where p.ParentId == parentId && p.IncludeInMenu
//                      orderby p.PageOrder
//                      ascending
//                      select p;

//            foreach (var r in shm)
//            {
//                ul = true;
//                li += "<li>" + string.Format("<a class='pageNode' href=\"/{0}\" rel='{1}'>{2}</a>",
//                                                SeoUrl.GetSeoUrl(r.Id), r.Id,
//                                                PropertyString.GetString("Page", r.Id.ToString(CultureInfo.InvariantCulture)));
//                li += PageList(r.Id);
//                li += "</li>\n\t";
//            }
//            if (ul)
//            {
//                li = "\n\t<ul>\n\t" + li + "</ul>";
//            }
//            return li;
//        }

//        public static HtmlString BreakCumb(this HtmlHelper helper, int Id)
//        {
//            return new HtmlString(Gbreakcumb(Id));
//        }

//        private static string Gbreakcumb(int Id)
//        {
//            Page p = pageservice.GetPageById(Id);
//            bool ShowBreakCum = p.EnableBreakCumb;
//            string _breakcumb = String.Format("<span>{0}</a>", PropertyString.GetString("page", Id.ToString()));
//            int ParentId = 0;
//            do
//            {
//                var page = pageservice.GetPageById(Id);
//                if (page != null)
//                {
//                    if (page.ParentId == -1)
//                    {
//                        if (ShowBreakCum)//store root Id in string and split it on pageview
//                            _breakcumb = page.Id + "_" + _breakcumb;
//                        else
//                            _breakcumb = page.Id + "_";
//                        ParentId = page.ParentId;
//                        break;
//                    }
//                    else
//                    {
//                        _breakcumb = String.Format("<a href='/{0}' class='breakcumb'>{1}</a>", SeoUrl.GetSeoUrl(page.ParentId), PropertyString.GetString("page", page.ParentId.ToString())) + " » " + _breakcumb;
//                        Id = page.ParentId;
//                    }
//                }
//                else
//                {
//                    _breakcumb = page.Id.ToString();
//                    break;
//                }
//            } while (ParentId != -1);
//            return _breakcumb;
//        }

//        public static HtmlString GetLanguageSelector(this HtmlHelper helper, bool displayname = false)
//        {
//            string currentselected;
//            var language = languageservice.GetLanguagePublic();
//            string html = string.Empty;
//            if (displayname)
//                foreach (var item in language)
//                {
//                    currentselected = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == item.TwoLetterIsoLanguageName ? "languageselector languageselected" : "languageselector";
//                    html += " <a href='/common/setculture/" + item.Id + "'><span class='" + currentselected + "'><img src='../../Content/Images/flags/" + item.LanguageImageName + "' title='" + item.LanguageName + "' />" + item.LanguageName + "</span></a>";
//                }
//            else
//                foreach (var item in language)
//                {
//                    currentselected = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == item.TwoLetterIsoLanguageName ? "languageselector languageselected" : "languageselector";
//                    html += " <a href='/common/setculture/" + item.TwoLetterIsoLanguageName + "'><span class='" + currentselected + "'><img src='../../Content/Images/flags/" + item.LanguageImageName + "' title='" + item.LanguageName + "' /></span></a>";
//                }
//            return new HtmlString(html);
//        }

//        public static HtmlString GetSettingEditIcon(this HtmlHelper helper, int moduleId, string editContentUrl)
//        {
//            string html = "<a href='/pagemodule/edit/" + moduleId + "'><img src='../../Content/Images/site/setting.png' alt='Module setting' /></a>";
//            if (!string.IsNullOrEmpty(editContentUrl))
//                html += "<a href='" + editContentUrl + moduleId + "'><img src='../../Content/Images/site/editcontent.png' alt='Edit content' /></a>";
//            return new HtmlString(html);
//        }
//        public static MvcHtmlString AutocompleteFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string actionName, string controllerName)
//        {
//            var autocompleteUrl = UrlHelper.GenerateUrl(null, actionName, controllerName,
//                                                           null,
//                                                           html.RouteCollection,
//                                                           html.ViewContext.RequestContext, true);

//            return html.TextBoxFor(expression, new { data_autocomplete_url = autocompleteUrl });
//        }

//        public static HtmlString Social_Twitter(this HtmlHelper htmlHelper, string title, string url = "")
//        {
//            var socialLink = new TagBuilder("a");
//            socialLink.Attributes.Add("href", "https://twitter.com/share");
//            socialLink.Attributes.Add("class", "twitter-share-button");
//            socialLink.Attributes.Add("data-via", "MY-TWITTER-HANDLE");
//            socialLink.Attributes.Add("data-count", "horizontal");
//            socialLink.Attributes.Add("data-text", title);
//            socialLink.SetInnerText("Tweet");
//            if (!string.IsNullOrEmpty(url))
//            {
//                socialLink.Attributes.Add("data-url", url);
//            }
//            return new HtmlString(socialLink.ToString(TagRenderMode.Normal));
//        }

//        public static HtmlString Social_Facebook(this HtmlHelper htmlHelper, string title, string url = "")
//        {
//            var str = new StringBuilder();
//            var socialLink = new TagBuilder("div");
//            socialLink.Attributes.Add("class", "fb-like");
//            socialLink.Attributes.Add("data-send", "false");
//            socialLink.Attributes.Add("data-layout", "button_count");
//            socialLink.Attributes.Add("data-show-faces", "false");
//            socialLink.Attributes.Add("data-font", "arial");
//            if (!string.IsNullOrEmpty(url))
//            {
//                socialLink.Attributes.Add("data-href", url);
//            }
//            str.Append(socialLink.ToString(TagRenderMode.Normal));
//            return new HtmlString(str.ToString());
//        }

//        public static HtmlString Social_GooglePlusOne(this HtmlHelper htmlHelper, string title, string url = "")
//        {
//            var socialLink = new TagBuilder("div");
//            socialLink.Attributes.Add("class", "g-plusone");
//            socialLink.Attributes.Add("data-size", "medium");
//            if (!string.IsNullOrEmpty(url))
//            {
//                socialLink.Attributes.Add("data-href", url);
//            }
//            return new HtmlString(socialLink.ToString(TagRenderMode.Normal));
//        }

//        public static HtmlString Social_AllButtons(this HtmlHelper htmlHelper, string title, string url)
//        {
//            var str = new StringBuilder();
//            var ul = new TagBuilder("ul");
//            ul.AddCssClass("social");

//            //Google
//            var li3 = new TagBuilder("li") { InnerHtml = htmlHelper.Social_GooglePlusOne(title, url).ToHtmlString() };
//            li3.AddCssClass("social-google");
//            ul.InnerHtml += li3.ToString();

//            //Twitter
//            var li2 = new TagBuilder("li") { InnerHtml = htmlHelper.Social_Twitter(title, url).ToHtmlString() };
//            li2.AddCssClass("social-twitter");
//            ul.InnerHtml += li2.ToString();

//            //facebook
//            var li1 = new TagBuilder("li") { InnerHtml = htmlHelper.Social_Facebook(title, url).ToHtmlString() };
//            li1.AddCssClass("social-facebook");
//            ul.InnerHtml += li1.ToString();
//            str.Append(ul.ToString(TagRenderMode.Normal));

//            return new HtmlString(str.ToString());
//        }
//    }

//}

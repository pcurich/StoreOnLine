using System;
using System.Collections.Generic;
using System.Linq;
using StoreOnLine.DataBase.Model.CmsEmploye;
using StoreOnLine.DataBase.Model.CmsGroup;
using StoreOnLine.DataBase.Model.CmsLanguage;
using StoreOnLine.DataBase.Model.CmsShop;
using StoreOnLine.Util.Xml;
using Rol = StoreOnLine.DataBase.Model.CmsRol.Rol;

namespace StoreOnLine.DataBase.Configuration
{
    public class DataInitial
    {
        public static void LoadLanguage(StoreOnLineContext context, String str)
        {
            var elemt = XmlSerialization<List<Language>>.Deserialize(str);
            foreach (var e in elemt)
            {
                e.Active = true;
                context.Languages.Add(e);
            }
            context.SaveChanges();
        }

        public static void LoadShop(StoreOnLineContext context, String str)
        {
            var elemt = XmlSerialization<List<Shop>>.Deserialize(str);
            foreach (var e in elemt)
            {
                e.Active = true;
                context.Shops.Add(e);
            }
            context.SaveChanges();
        }

        public static void LoadRol(StoreOnLineContext context, String str)
        {
            var elemt = XmlSerialization<List<Rol>>.Deserialize(str);
            foreach (var e in elemt)
            {
                e.Active = true;
                e.LanguageId = context.Languages.FirstOrDefault(o => o.Cultura == "es-PE").Id;
                context.Rols.Add(e);
            }
            context.SaveChanges();
        }

        public static void LoadEmployer(StoreOnLineContext context, string str)
        {
            var elemt = XmlSerialization<List<Employer>>.Deserialize(str);
            foreach (var e in elemt)
            {
                e.Active = true;
                e.LastPassWordGenered = DateTime.Now;
                context.Employers.Add(e);
            }
            context.SaveChanges();
        }

        internal static void LoadEmployerShop(StoreOnLineContext context, string str)
        {
            var elemt = XmlSerialization<List<EmployerShop>>.Deserialize(str);
            foreach (var e in elemt)
            {
                e.Active = true;
                e.EmployeId = (context.Employers.First(o => o.UserName == "pcurich")).Id;
                e.ShopId = (context.Shops.First(o => o.Name == "StoreOnLine")).Id;
                context.EmployerShops.Add(e);
            }
            context.SaveChanges();
        }

        #region Group
        internal static void LoadGroup(StoreOnLineContext context, string str)
        {
            var elemt = XmlSerialization<List<Group>>.Deserialize(str);
            foreach (var e in elemt)
            {
                e.Active = true;
                context.Groups.Add(e);
            }
            context.SaveChanges();
        }

        internal static void LoadGroupLang(StoreOnLineContext context, string str)
        {
            var elemt = XmlSerialization<List<GroupLang>>.Deserialize(str);
            foreach (var e in elemt)
            {
                e.Active = true;
                e.LanguageId = context.Languages.First(o => o.Cultura.Equals("es-PE")).Id;
                context.GroupLangs.Add(e);
            }
            context.SaveChanges();
        }
        #endregion
    }
}

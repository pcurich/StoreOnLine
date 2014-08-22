using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Entities
{
    public partial class StoreOnLineContext
    {
        public class StoreOnLineInitializerDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<StoreOnLineContext>
        {
            protected override void Seed(StoreOnLineContext context)
            {
                // base.Seed(context);
            }

            //public override void InitializeDatabase(StoreOnLineContext context)
            //{
            //    //if (context.Database.Exists())
            //    //{
            //    //    context.Database.ExecuteSqlCommand("");
            //    //}
            //}
        }

        public class StoreOnLineInitializerDropCreateDatabaseAlways : DropCreateDatabaseAlways<StoreOnLineContext>
        {
            protected override void Seed(StoreOnLineContext context)
            {
                LoadCategory();
                var cat = new Category { CategoryName = "Category 1" };
                context.Categories.Add(cat);
                context.SaveChanges();

                var cam = new Campaign { CampaignName = "Campaign 1" };
                context.Campaigns.Add(cam);
                context.SaveChanges();

                var uni = new Unit { UnitName = "Unit 1" };
                context.Units.Add(uni);
                context.SaveChanges();

                var pbs = new List<ProductBase>
                {
                    new ProductBase {ProductName = "Football", ProductBasePrice = 25M, ProductCampaignId = 1, ProductCategoryId = 1, ProductUnitId = 1},
                    new ProductBase {ProductName = "Surf board", ProductBasePrice = 179M, ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1},
                    new ProductBase {ProductName = "Running shoes", ProductBasePrice = 95M, ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1},
                    new ProductBase {ProductName = "Tshirt", ProductBasePrice = 95M, ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1},
                    new ProductBase {ProductName = "Shorts ", ProductBasePrice = 95M, ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1}
                };

                foreach (var pb in pbs)
                {
                    context.ProductBases.Add(pb);
                }
            }

            private static void LoadCategory()
            {
                string[] rootx1 = System.IO.Directory.GetDirectories(System.AppDomain.CurrentDomain.BaseDirectory);

                List<KeyValuePair<string, string>> xmlPairs = new List<KeyValuePair<string, string>>();

                XDocument xml = Util.Xml.XmlFile.Load("C:/Users/Pedro/Documents/GitHub/StoreOnLine/StoreOnLine.DataBase/Files/Category.xml");
                var categories = (from categ in xml.Descendants("Category") select categ).ToList();

                foreach (XElement category in categories)
                {
                    XAttribute xElement = category.Attribute("CategoryName");
                    if (xElement != null)
                        Console.WriteLine(xElement.Value);
                }

                //if (xml.Root != null)
                //{
                //    string root = xml.Root.Name.ToString();
                //    IEnumerable<XElement> xElements = Util.Xml.XmlFile.Elements(xml, root);
                    //xmlPairs.AddRange(
                    //   from xElement in xElements
                    //   from att in xElement.Attributes(xElement.Name)
                    //   select new KeyValuePair<string, string>(att.Name.LocalName, att.Value)
                    //   );
                    //foreach (var xElement in xElements)
                    //{
                    //    IEnumerable<XAttribute> attributes = xElement.Attributes(xElement.Name);
                    //    foreach (var att in attributes)
                    //    {
                    //        xmlPairs.Add(new KeyValuePair<string, string>(att.Name.LocalName, att.Value));
                    //    }
                    //}

                    //foreach (var xmlPair in xmlPairs)
                    //{
                    //    Console.WriteLine(xmlPair.Value + " - " + xmlPair.Value);
                    //}
                    
             //   }

            }

            //public override void InitializeDatabase(StoreOnLineContext context)
            //{
            //    //if (context.Database.Exists())
            //    //{
            //    //    context.Database.ExecuteSqlCommand("");
            //    //}
            //}
        }

        public class StoreOnLineInitializerCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<StoreOnLineContext>
        {
            protected override void Seed(StoreOnLineContext context)
            {
                var pbs = new List<ProductBase>
                {
                    new ProductBase {ProductName = "Football", ProductBasePrice = 25M},
                    new ProductBase {ProductName = "Surf board", ProductBasePrice = 179M},
                    new ProductBase {ProductName = "Running shoes", ProductBasePrice = 95M}
                };

                foreach (var pb in pbs)
                {
                    context.ProductBases.Add(pb);
                }

            }

            public override void InitializeDatabase(StoreOnLineContext context)
            {
                if (context.Database.Exists())
                {
                    //context.Database.ExecuteSqlCommand("");
                }
            }
        }
    }
}

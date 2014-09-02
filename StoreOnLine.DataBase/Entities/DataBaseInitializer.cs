using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Resources;
using StoreOnLine.Util.Xml;

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

        }

        public class StoreOnLineInitializerDropCreateDatabaseAlways : DropCreateDatabaseAlways<StoreOnLineContext>
        {
            protected override void Seed(StoreOnLineContext context)
            {
                String pathFile = @"C:\Users\gmc\Documents\GitHub\StoreOnLine\StoreOnLine.DataBase\Files\";
                String pathImg = @"C:\Users\gmc\Documents\GitHub\StoreOnLine\StoreOnLine.DataBase\Img\";
                LoadImagen(context, pathImg);
                LoadCategory(context, pathFile + "Category.xml");
                LoadCampaign(context, pathFile + "Campaign.xml");
                LoadUnit(context, pathFile + "Unit.xml");
                //Export(@"C:\Users\gmc\Documents\GitHub\StoreOnLine\StoreOnLine.DataBase\Files\Unit.xml");

                var pbs = new List<ProductBase>
                {
                    new ProductBase {ProductName = "Football", ProductDescription = "", ProductBasePrice = 25M, ProductSalePrice =2.5M, ProductCampaignId = 1, ProductCategoryId = 1, ProductUnitId = 1 },
                    new ProductBase {ProductName = "Surf board", ProductDescription = "", ProductBasePrice = 179M, ProductSalePrice =2.5M,ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1 },
                    new ProductBase {ProductName = "Running shoes", ProductDescription = "", ProductBasePrice = 95M,ProductSalePrice =2.5M, ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1 },
                    new ProductBase {ProductName = "Tshirt", ProductDescription = "", ProductBasePrice = 95M, ProductSalePrice =2.5M,ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1 },
                    new ProductBase {ProductName = "Shorts ", ProductDescription = "",ProductBasePrice = 95M,ProductSalePrice =2.5M, ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1 }
                };

                foreach (var pb in pbs)
                {
                    context.ProductBases.Add(pb);
                }
                context.SaveChanges();
            }
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

        #region LoadMethods

        private static void LoadCategory(StoreOnLineContext context, String str)
        {
            string[] rootx1 = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory);

            var elemt = XmlSerialization<List<Category>>.Deserialize(str);
            foreach (var e in elemt)
            {
                context.Categories.Add(e);
            }
            context.SaveChanges();
        }

        private static void LoadCampaign(StoreOnLineContext context, String str)
        {
            string[] rootx1 = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory);

            var elemt = XmlSerialization<List<Campaign>>.Deserialize(str);
            foreach (var e in elemt)
            {
                context.Campaigns.Add(e);
            }
            context.SaveChanges();
        }

        private static void LoadUnit(StoreOnLineContext context, String str)
        {
            string[] rootx1 = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory);

            var elemt = XmlSerialization<List<Unit>>.Deserialize(str);

            foreach (var e in elemt)
            {
                context.Units.Add(e);
            }
            context.SaveChanges();
        }

        private static void Export(String str)
        {
            var pbs = new List<Unit>
                {
                    new Unit {UnitCode = "X", UnitDescription = "X", UnitName ="X" }
                };
            XmlSerialization<List<Unit>>.Serialize(pbs, str);
        }

        private static void LoadImagen(StoreOnLineContext context, String str)
        {
            var directory = new DirectoryInfo(str);
            foreach (var dir in directory.GetFiles())
            {
                var map = new Bitmap(Image.FromFile(dir.FullName), new Size(150, 150));

                var imagen = new Imagen();
                imagen.ImageData = Util.Img.ImgTransform.ConvertBitMapToByteArray(map);
                imagen.ImageMimeType = ImageFormat.Jpeg.ToString();
                imagen.ObjectId = 0;

                imagen.ObjectName = dir.Name.Contains("Campaign") ? Const.ObjectName.CampaignName :
                    dir.Name.Contains("Category") ? Const.ObjectName.CategoryName :
                    dir.Name.Contains("Unit") ? Const.ObjectName.UnitName :
                    dir.Name.Contains("Feature") ? Const.ObjectName.FeatureName :
                    dir.Name.Contains("ProductBase") ? Const.ObjectName.ProductBaseName :
                    dir.Name.Contains("ProductConcrete") ? Const.ObjectName.ProductCompositeName : Const.ObjectName.Default;

                imagen.IsPrincipal = true;
                context.Imagens.Add(imagen);
            }
            context.SaveChanges();
        }

        #endregion
    }
}

using StoreOnLine.DataBase.Model.Companies;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Providers;
using StoreOnLine.DataBase.Model.Resources;
using StoreOnLine.DataBase.Model.Security;
using StoreOnLine.Util.Xml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

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
                var path = "pedro";//gmc
                String pathFile = @"C:\Users\" + path + @"\Documents\GitHub\StoreOnLine\StoreOnLine.DataBase\Files\";
                String pathImg = @"C:\Users\" + path + @"\Documents\GitHub\StoreOnLine\StoreOnLine.DataBase\Img\";
                LoadImagen(context, pathImg);
                LoadCategory(context, pathFile + "Category.xml");
                LoadCampaign(context, pathFile + "Campaign.xml");
                LoadUnit(context, pathFile + "Unit.xml");
                LoadUbigeo(context, pathFile + "Ubigeo.xml");
                LoadSupplier(context, pathFile + "Unit.xml");
                LoadRoles(context, pathFile + "Role.xml");
                LoadDocumentType(context, pathFile + "DocumentType.xml");
                LoadPersons(context, pathFile + "User.xml");
               // Export(pathFile + "Company.xml");
                LoadCompany(context, pathFile + "Company.xml");

                var pbs = new List<ProductBase>
                {
                    new ProductBase {ProductName = "Football", ProductDescription = "", ProductBasePrice = 25M, ProductSalePrice =2.5M, ProductCampaignId = 1, ProductCategoryId = 1, ProductUnitId = 1, ProductSupplierId = 1 },
                    new ProductBase {ProductName = "Surf board", ProductDescription = "", ProductBasePrice = 179M, ProductSalePrice =2.5M,ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1, ProductSupplierId = 1 },
                    new ProductBase {ProductName = "Running shoes", ProductDescription = "", ProductBasePrice = 95M,ProductSalePrice =2.5M, ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1, ProductSupplierId = 1 },
                    new ProductBase {ProductName = "Tshirt", ProductDescription = "", ProductBasePrice = 95M, ProductSalePrice =2.5M,ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1, ProductSupplierId = 1 },
                    new ProductBase {ProductName = "Shorts ", ProductDescription = "",ProductBasePrice = 95M,ProductSalePrice =2.5M, ProductCampaignId = 1, ProductCategoryId = 1,ProductUnitId = 1, ProductSupplierId = 1 }
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
                    new ProductBase {ProductName = "Football", ProductBasePrice = 25M, ProductSupplierId = 1},
                    new ProductBase {ProductName = "Surf board", ProductBasePrice = 179M, ProductSupplierId = 1},
                    new ProductBase {ProductName = "Running shoes", ProductBasePrice = 95M, ProductSupplierId = 1}
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
                e.IsStatus = true;
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
                e.IsStatus = true;
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
                e.IsStatus = true;
                context.Units.Add(e);
            }
            context.SaveChanges();
        }

        private static void LoadUbigeo(StoreOnLineContext context, String str)
        {
            var elemt = XmlSerialization<List<Ubigeo>>.Deserialize(str);
            foreach (var pb in elemt)
            {
                context.Ubigeos.Add(pb);
            }
            context.SaveChanges();
        }

        private static void LoadSupplier(StoreOnLineContext context, String str)
        {
            string[] rootx1 = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory);

            var elemt = XmlSerialization<List<Unit>>.Deserialize(str);

            var pbs = new List<Supplier>
                {
                    new Supplier {SupplierName = "SupplierName" },
                };

            foreach (var pb in pbs)
            {
                context.Suppliers.Add(pb);
            }
            context.SaveChanges();
        }
        private static void LoadRoles(StoreOnLineContext context, string str)
        {
            var elemt = XmlSerialization<List<Role>>.Deserialize(str);
            foreach (var pb in elemt)
            {
                context.Roles.Add(pb);
            }
            context.SaveChanges();
        }
        private static void LoadPersons(StoreOnLineContext context, string str)
        {
            var elemt = XmlSerialization<List<Person>>.Deserialize(str);
            foreach (var pb in elemt)
            {
                context.Persons.Add(pb);
            }
            context.SaveChanges();
        }

        private static void LoadDocumentType(StoreOnLineContext context, string str)
        {
            var elemt = XmlSerialization<List<DocumentType>>.Deserialize(str);
            foreach (var pb in elemt)
            {
                context.DocumentTypes.Add(pb);
            }
            context.SaveChanges();
        }

        private static void LoadCompany(StoreOnLineContext context, string str)
        {
            var elemt = XmlSerialization<List<Company>>.Deserialize(str);
            foreach (var pb in elemt)
            {
                context.Companies.Add(pb);
            }
            context.SaveChanges();
        }

        private static void LoadImagen(StoreOnLineContext context, String str)
        {
            var directory = new DirectoryInfo(str);
            foreach (var dir in directory.EnumerateFiles("*.*", SearchOption.AllDirectories).Where(s => s.Extension.EndsWith(".jpg") || s.Extension.EndsWith(".JPG")))
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
                    dir.Name.Contains("Supplier") ? Const.ObjectName.SupplierName :
                    dir.Name.Contains("ProductConcrete") ? Const.ObjectName.ProductCompositeName : Const.ObjectName.Default;

                imagen.IsPrincipal = true;
                imagen.IsStatus = true;
                context.Imagens.Add(imagen);
            }
            context.SaveChanges();
        }

        private static void Export(String str)
        {
            var pbs = new List<Company>
                {
                    new Company { 
                        CompanyName = "BBVA", 
                        CompanyActivity= "Financiero", 
                        CompanyCif="0001",
                        CompanySecurityNumber="Ab123456789",
                        CompanyDocumentRuc="45645645645", 
                        Address = new Address(0,"Av Republica de panama 256","","javier prado con via expresa",1425),
                        ContactNumber=new ContactNumber(0,"12457896","154785","curichpedro@gmail.com"),
                        Person = new Person(0,true,"Luis  Manuel","Gonzales",new DateTime(1986,04,17),new Document(0,"12458785",1),
                            new ContactNumber(0,"541165265","5161561","hjfdchjufchuf@jhfbuhfbuf.com"),     
                            new Address(0,"su casa","al ladito","",1405),new User(0,"emp","emp","emp"),4), 
                            IsStatus = true}
                    
                };
            XmlSerialization<List<Company>>.Serialize(pbs, str);
        }

        #endregion
    }
}

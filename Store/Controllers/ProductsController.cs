using AuthenticationPlugin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Store.Models.Context;
using Store.Models.Entities;
using Store.Models.Entities.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private StoreDbContext _storeDbContext;
        public ProductsController(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        ///////////////////////////////////////////// Post Methods For Products /////////////////////////////////////////////////////

        //Post Products
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public IActionResult PostProduct([FromForm] Product product)
        {
            var guid = Guid.NewGuid();
            var filepath = Path.Combine("wwwroot", guid + ".jpg");
            if (product.Images != null)
            {
                var filestream = new FileStream(filepath, FileMode.Create);
                product.Images.CopyTo(filestream);
            }
            product.ImageUrl = filepath.Remove(0, 7);
            _storeDbContext.Products.Add(product);
            _storeDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        //Post Product Gallery
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public IActionResult ProductsGallery([FromForm] ProductsGallery products)
        {
            var guid = Guid.NewGuid();
            var filepath = Path.Combine("wwwroot", guid + ".jpg");
            if (products.Images != null)
            {
                var filestream = new FileStream(filepath, FileMode.Create);
                products.Images.CopyTo(filestream);
            }
            products.ImageUrl = filepath.Remove(0, 7);
            var galleryobj = new ProductsGallery
            {
                ProductID = products.ProductID,
                ImageUrl = products.ImageUrl
            };
            _storeDbContext.ProductsGalleries.Add(galleryobj);
            _storeDbContext.SaveChanges();
            return Ok("Image Added.");
        }

        //Post Product Sizes
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public IActionResult ProductSize([FromForm] OtherSizes size)
        {
            var sizeobj = new OtherSizes
            {
                ProductID = size.ProductID,
                SizesID = size.SizesID
            };
            _storeDbContext.OtherSizes.Add(sizeobj);
            _storeDbContext.SaveChanges();
            return Ok("Size Added.");
        }

        //Post Product Colors
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public IActionResult ProductColor([FromForm] OtherColors color)
        {
            var colorobj = new OtherColors
            {
                ProductID = color.ProductID,
                ColorsID = color.ColorsID
            };
            _storeDbContext.OtherColors.Add(colorobj);
            _storeDbContext.SaveChanges();
            return Ok("Color Added.");
        }

        //Post Product Categories
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public IActionResult ProductCategory([FromForm] OtherCategories category)
        {
            var categoryobj = new OtherCategories
            {
                ProductID = category.ProductID,
                CategoriesID = category.CategoriesID
            };
            _storeDbContext.OtherCategories.Add(categoryobj);
            _storeDbContext.SaveChanges();
            return Ok("Category Added.");
        }

        ///////////////////////////////////////////// Get Methods For Products /////////////////////////////////////////////////////

        //Get All Products
        [HttpGet]
        public IActionResult AllProducts(string sort, int? pageNumber, int? pageSize)
        {
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;
            var products = from p in _storeDbContext.Products
                           join pg in _storeDbContext.ProductsGalleries
                           on p.ID equals pg.ProductID into ps1
                           from pg in ps1.DefaultIfEmpty()

                           join os in _storeDbContext.OtherSizes
                           on p.ID equals os.ProductID into ps2
                           from os in ps2.DefaultIfEmpty()

                           join oc in _storeDbContext.OtherColors
                           on p.ID equals oc.ProductID into ps3
                           from oc in ps3.DefaultIfEmpty()

                           join oca in _storeDbContext.OtherCategories
                           on p.ID equals oca.ProductID into ps4
                           from oca in ps4.DefaultIfEmpty()

                           join c in _storeDbContext.Colors
                           on oc.ColorsID equals c.ID into ps5
                           from c in ps5.DefaultIfEmpty()

                           join ca in _storeDbContext.Categories
                           on oca.CategoriesID equals ca.ID into ps6
                           from ca in ps6.DefaultIfEmpty()

                           join s in _storeDbContext.Sizes
                           on os.SizesID equals s.ID into ps7
                           from s in ps7.DefaultIfEmpty()
                           select new
                           {
                               ID = p.ID,
                               Name = p.Name,
                               ProductCode = p.ProductCode,
                               Weight = p.Wieght,
                               Content = p.Content,
                               Quantity = p.Quantity,
                               Price = p.Price,
                               ImageUrl = p.ImageUrl,
                               Description = p.Description,
                               ImageUrlGallery = pg.ImageUrl,

                               Color = c.Color,
                               ColorsID = c.ID,

                               Category = ca.Category,

                               Size = s.Size,
                               SizesID = s.ID
                           };

            switch (sort) //Sorting and Paging
            {
                case "desc":
                    return Ok(products.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize).OrderByDescending(m => m.Quantity));
                case "asc":
                    return Ok(products.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize).OrderBy(m => m.Quantity));
                default:
                    return Ok(products.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }
        }

        //Get Products Details
        [HttpGet("{id}")]
        public IActionResult ProductDetail(int id)
        {
            var product = _storeDbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //Search Products
        [HttpGet]
        public IActionResult Search(string productName)
        {
            var products = from p in _storeDbContext.Products
                         where p.Name.StartsWith(productName)
                         select new
                         {
                             Id = p.ID,
                             Name = p.Name,
                             ImageUrl = p.ImageUrl
                         };
            return Ok(products);
        }

        //Get Categories and Products
        [HttpGet]
        public IActionResult Category(string cat)
        {
            var products = from oc in _storeDbContext.OtherCategories
                           join c in _storeDbContext.Categories
                           on oc.CategoriesID equals c.ID

                           join p in _storeDbContext.Products
                           on oc.ProductID equals p.ID

                           where (c.Category == cat)

                           select new
                           {
                               Name = p.Name,
                               ProductCode = p.ProductCode,
                               Weight = p.Wieght,
                               Content = p.Content,
                               Quantity = p.Quantity,
                               Price = p.Price,
                               ImageUrl = p.ImageUrl,
                               Description = p.Description,
                           };
            return Ok(products);
        }

        ///////////////////////////////////////////// Delete Methods For Products /////////////////////////////////////////////////////

        //Delete Products
        [Authorize(Roles = "Admin,Editor")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _storeDbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _storeDbContext.Products.Remove(product);
                _storeDbContext.SaveChanges();
                return Ok("Product deleted");
            }
        }

        //Delete ProductsGallery
        [Authorize(Roles = "Admin,Editor")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProductsGallery(int id)
        {
            var productgallery = _storeDbContext.ProductsGalleries.Find(id);
            if (productgallery == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _storeDbContext.ProductsGalleries.Remove(productgallery);
                _storeDbContext.SaveChanges();
                return Ok("Product Photo deleted");
            }
        }

        //Delete ProductsSizes
        [Authorize(Roles = "Admin,Editor")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProductSizes(int id)
        {
            var productsize = _storeDbContext.OtherSizes.Find(id);
            if (productsize == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _storeDbContext.OtherSizes.Remove(productsize);
                _storeDbContext.SaveChanges();
                return Ok("Product Size deleted");
            }
        }

        //Delete ProductsColors
        [Authorize(Roles = "Admin,Editor")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProductColors(int id)
        {
            var productcolor = _storeDbContext.OtherColors.Find(id);
            if (productcolor == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _storeDbContext.OtherColors.Remove(productcolor);
                _storeDbContext.SaveChanges();
                return Ok("Product Color deleted");
            }
        }

        //Delete ProductsCategories
        [Authorize(Roles = "Admin,Editor")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProductCategories(int id)
        {
            var productcategory = _storeDbContext.OtherCategories.Find(id);
            if (productcategory == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _storeDbContext.OtherCategories.Remove(productcategory);
                _storeDbContext.SaveChanges();
                return Ok("Product Category deleted");
            }
        }

        ///////////////////////////////////////////// Update Methods For Products /////////////////////////////////////////////////////

        //Update Products
        [Authorize(Roles = "Admin,Editor,Author")]
        [HttpPut("{id}")]
        public IActionResult EditProduct(int id, [FromForm] Product productobj)
        {
            var product = _storeDbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                var guid = Guid.NewGuid();
                var filePath = Path.Combine("wwwroot", guid + ".jpg");
                if (productobj.Images != null)
                {
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    productobj.Images.CopyTo(fileStream);
                    product.ImageUrl = filePath.Remove(0, 7);
                }

                product.Name = productobj.Name;
                product.ProductCode = productobj.ProductCode;
                product.Wieght = productobj.Wieght;
                product.Description = productobj.Description;
                product.Content = productobj.Content;
                product.Quantity = productobj.Quantity;
                product.Price = productobj.Price;

                _storeDbContext.SaveChanges();
                return Ok("Product updated successfully");
            }
        }

        //Update ProductsGallery
        [Authorize(Roles = "Admin,Editor,Author")]
        [HttpPut("{id}")]
        public IActionResult EditProductGallery(int id, [FromForm] ProductsGallery productobj)
        {
            var product = _storeDbContext.ProductsGalleries.Find(id);
            if (product == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                var guid = Guid.NewGuid();
                var filePath = Path.Combine("wwwroot", guid + ".jpg");
                if (productobj.Images != null)
                {
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    productobj.Images.CopyTo(fileStream);
                    product.ImageUrl = filePath.Remove(0, 7);
                }
                _storeDbContext.SaveChanges();
                return Ok("Gallery updated successfully");
            }
        }

        //Update ProductsSizes
        [Authorize(Roles = "Admin,Editor,Author")]
        [HttpPut("{id}")]
        public IActionResult EditProductSize(int id, [FromForm] OtherSizes sizeobj)
        {
            var size = _storeDbContext.OtherSizes.Find(id);
            if (size == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                size.SizesID = sizeobj.SizesID;
                _storeDbContext.SaveChanges();
                return Ok("Sizes updated successfully");
            }
        }

        //Update ProductsColors
        [Authorize(Roles = "Admin,Editor,Author")]
        [HttpPut("{id}")]
        public IActionResult EditProductColor(int id, [FromForm] OtherColors colorobj)
        {
            var color = _storeDbContext.OtherColors.Find(id);
            if (color == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                color.ColorsID = colorobj.ColorsID;
                _storeDbContext.SaveChanges();
                return Ok("Sizes updated successfully");
            }
        }

        //Update ProductsCategories
        [Authorize(Roles = "Admin,Editor,Author")]
        [HttpPut("{id}")]
        public IActionResult EditProductCategory(int id, [FromForm] OtherCategories categoryobj)
        {
            var category = _storeDbContext.OtherCategories.Find(id);
            if (category == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                category.CategoriesID = categoryobj.CategoriesID;
                _storeDbContext.SaveChanges();
                return Ok("Sizes updated successfully");
            }
        }
    }
}

using Northwind.Business.Abstract;
using Northwind.Business.Validation.FluentValidation;
using FluentValidation;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;
using Northwind.Business.Utilities;

namespace Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            try
            {
                ValidationTool.Validate(new ProductValidator(), product);
                _productDal.Add(product);
            }
            catch (Exception exception)
            {

                throw new Exception("Ekleme İşlemi Yapılamadı");
            }
            
        }

        public void Delete(Product product)
        {
            try
            {
                
                _productDal.Delete(product);
            }
            catch 
            {
                throw new Exception("Silme İşlemi Gerçekleşemedi");
            }
            
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productDal.GetAll(p => p.CategoryId == categoryId);
        }

        public List<Product> GetProductsByProductName(string productName)
        {
            return _productDal.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));
        }

        public void Update(Product product)
        {

            try
            {
                ValidationTool.Validate(new ProductValidator(), product);
                _productDal.Update(product);
            }
            catch 
            {
                throw new Exception("Güncelleme İşlemi Yapılamadı");
            }
           
        }
    }
}

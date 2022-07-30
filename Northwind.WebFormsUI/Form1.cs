using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.Business.DependencyResolvers.Ninject;
using Northwind.DataAccess.Concrete;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind.WebFormsUI
{
    public partial class Form1 : Form
    {
        IProductService _productManager;
        ICategoryService _categoryManager;
        public Form1()
        {
            InitializeComponent();
            _productManager = InstanceFactory.GetInstance<IProductService>();
            _categoryManager = InstanceFactory.GetInstance<ICategoryService>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();
            
        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryManager.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";

            cbxCategoryId.DataSource = _categoryManager.GetAll();
            cbxCategoryId.DisplayMember = "CategoryName";
            cbxCategoryId.ValueMember = "CategoryId";

            cbxCategoryUpdate.DataSource = _categoryManager.GetAll();
            cbxCategoryUpdate.DisplayMember = "CategoryName";
            cbxCategoryUpdate.ValueMember = "CategoryId";

        }

        private void LoadProducts()
        {
            dgwProduct.DataSource = _productManager.GetAll();
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productManager.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch 
            {
                
            }
            
        }

        private void tbxProductName_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxProductName.Text))
            {
                dgwProduct.DataSource = _productManager.GetProductsByProductName(tbxProductName.Text);
            }
            else
            {
                LoadProducts();
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _productManager.Add(new Product
                {
                    CategoryId = Convert.ToInt32(cbxCategory.SelectedValue),
                    ProductName = tbxProductName2.Text,
                    QuantityPerUnit = tbxQuantityPerUnit.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxStock.Text)
                });
                LoadProducts();
                MessageBox.Show("Ürün Eklendi");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgwProduct.CurrentRow != null)
            {
                try
                {
                    _productManager.Update(new Product
                    {
                        ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                        ProductName = tbxUpdateProductName.Text,
                        CategoryId = Convert.ToInt32(cbxCategoryUpdate.SelectedValue),
                        UnitsInStock = Convert.ToInt16(tbxUnitsInStockUpdate.Text),
                        QuantityPerUnit = tbxQuantityPerUnitUpdate.Text,
                        UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text)

                    });
                    LoadProducts();
                    MessageBox.Show("Ürün Güncellendi");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    
                }
            }
            
        }

        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
           
                    tbxUpdateProductName.Text = dgwProduct.CurrentRow.Cells[1].Value.ToString();
                    cbxCategoryUpdate.SelectedValue = dgwProduct.CurrentRow.Cells[2].Value;
                    tbxUnitPriceUpdate.Text = dgwProduct.CurrentRow.Cells[3].Value.ToString();
                    tbxQuantityPerUnitUpdate.Text = dgwProduct.CurrentRow.Cells[4].Value.ToString();
                    tbxUnitsInStockUpdate.Text = dgwProduct.CurrentRow.Cells[5].Value.ToString();
            
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgwProduct.CurrentRow !=null)
            {
                try
                {
                    _productManager.Delete(new Product
                    {
                        ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value)
                    });
                    LoadProducts();
                    MessageBox.Show("Ürün Silindi");
                }
                catch (Exception exception)
                {

                    MessageBox.Show(exception.Message);
                }
            }
            
            
        }
    }
}

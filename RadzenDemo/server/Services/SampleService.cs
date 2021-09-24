using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using RadzenDemo.Data;

namespace RadzenDemo
{
    public partial class SampleService
    {
        SampleContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly SampleContext context;
        private readonly NavigationManager navigationManager;

        public SampleService(SampleContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportOrdersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sample/orders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sample/orders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOrdersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sample/orders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sample/orders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOrdersRead(ref IQueryable<Models.Sample.Order> items);

        public async Task<IQueryable<Models.Sample.Order>> GetOrders(Query query = null)
        {
            var items = Context.Orders.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOrdersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOrderCreated(Models.Sample.Order item);
        partial void OnAfterOrderCreated(Models.Sample.Order item);

        public async Task<Models.Sample.Order> CreateOrder(Models.Sample.Order order)
        {
            OnOrderCreated(order);

            Context.Orders.Add(order);
            Context.SaveChanges();

            OnAfterOrderCreated(order);

            return order;
        }
        public async Task ExportOrderDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sample/orderdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sample/orderdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOrderDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sample/orderdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sample/orderdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOrderDetailsRead(ref IQueryable<Models.Sample.OrderDetail> items);

        public async Task<IQueryable<Models.Sample.OrderDetail>> GetOrderDetails(Query query = null)
        {
            var items = Context.OrderDetails.AsQueryable();

            items = items.Include(i => i.Order);

            items = items.Include(i => i.Product);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOrderDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOrderDetailCreated(Models.Sample.OrderDetail item);
        partial void OnAfterOrderDetailCreated(Models.Sample.OrderDetail item);

        public async Task<Models.Sample.OrderDetail> CreateOrderDetail(Models.Sample.OrderDetail orderDetail)
        {
            OnOrderDetailCreated(orderDetail);

            Context.OrderDetails.Add(orderDetail);
            Context.SaveChanges();

            OnAfterOrderDetailCreated(orderDetail);

            return orderDetail;
        }
        public async Task ExportProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sample/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sample/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sample/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sample/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProductsRead(ref IQueryable<Models.Sample.Product> items);

        public async Task<IQueryable<Models.Sample.Product>> GetProducts(Query query = null)
        {
            var items = Context.Products.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnProductsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductCreated(Models.Sample.Product item);
        partial void OnAfterProductCreated(Models.Sample.Product item);

        public async Task<Models.Sample.Product> CreateProduct(Models.Sample.Product product)
        {
            OnProductCreated(product);

            Context.Products.Add(product);
            Context.SaveChanges();

            OnAfterProductCreated(product);

            return product;
        }

        partial void OnOrderDeleted(Models.Sample.Order item);
        partial void OnAfterOrderDeleted(Models.Sample.Order item);

        public async Task<Models.Sample.Order> DeleteOrder(int? id)
        {
            var itemToDelete = Context.Orders
                              .Where(i => i.Id == id)
                              .Include(i => i.OrderDetails)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOrderDeleted(itemToDelete);

            Context.Orders.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOrderDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnOrderGet(Models.Sample.Order item);

        public async Task<Models.Sample.Order> GetOrderById(int? id)
        {
            var items = Context.Orders
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnOrderGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Sample.Order> CancelOrderChanges(Models.Sample.Order item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOrderUpdated(Models.Sample.Order item);
        partial void OnAfterOrderUpdated(Models.Sample.Order item);

        public async Task<Models.Sample.Order> UpdateOrder(int? id, Models.Sample.Order order)
        {
            OnOrderUpdated(order);

            var itemToUpdate = Context.Orders
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(order);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterOrderUpdated(order);

            return order;
        }

        partial void OnOrderDetailDeleted(Models.Sample.OrderDetail item);
        partial void OnAfterOrderDetailDeleted(Models.Sample.OrderDetail item);

        public async Task<Models.Sample.OrderDetail> DeleteOrderDetail(int? id)
        {
            var itemToDelete = Context.OrderDetails
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOrderDetailDeleted(itemToDelete);

            Context.OrderDetails.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOrderDetailDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnOrderDetailGet(Models.Sample.OrderDetail item);

        public async Task<Models.Sample.OrderDetail> GetOrderDetailById(int? id)
        {
            var items = Context.OrderDetails
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Order);

            items = items.Include(i => i.Product);

            var itemToReturn = items.FirstOrDefault();

            OnOrderDetailGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Sample.OrderDetail> CancelOrderDetailChanges(Models.Sample.OrderDetail item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOrderDetailUpdated(Models.Sample.OrderDetail item);
        partial void OnAfterOrderDetailUpdated(Models.Sample.OrderDetail item);

        public async Task<Models.Sample.OrderDetail> UpdateOrderDetail(int? id, Models.Sample.OrderDetail orderDetail)
        {
            OnOrderDetailUpdated(orderDetail);

            var itemToUpdate = Context.OrderDetails
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(orderDetail);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterOrderDetailUpdated(orderDetail);

            return orderDetail;
        }

        partial void OnProductDeleted(Models.Sample.Product item);
        partial void OnAfterProductDeleted(Models.Sample.Product item);

        public async Task<Models.Sample.Product> DeleteProduct(int? id)
        {
            var itemToDelete = Context.Products
                              .Where(i => i.Id == id)
                              .Include(i => i.OrderDetails)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductDeleted(itemToDelete);

            Context.Products.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProductDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnProductGet(Models.Sample.Product item);

        public async Task<Models.Sample.Product> GetProductById(int? id)
        {
            var items = Context.Products
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnProductGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Sample.Product> CancelProductChanges(Models.Sample.Product item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnProductUpdated(Models.Sample.Product item);
        partial void OnAfterProductUpdated(Models.Sample.Product item);

        public async Task<Models.Sample.Product> UpdateProduct(int? id, Models.Sample.Product product)
        {
            OnProductUpdated(product);

            var itemToUpdate = Context.Products
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(product);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterProductUpdated(product);

            return product;
        }
    }
}

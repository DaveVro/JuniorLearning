using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RadzenDemo.Models.Sample;
using Microsoft.EntityFrameworkCore;

namespace RadzenDemo.Pages
{
    public partial class AddOrderDetailComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SampleService Sample { get; set; }

        IEnumerable<RadzenDemo.Models.Sample.Order> _getOrdersResult;
        protected IEnumerable<RadzenDemo.Models.Sample.Order> getOrdersResult
        {
            get
            {
                return _getOrdersResult;
            }
            set
            {
                if (!object.Equals(_getOrdersResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOrdersResult", NewValue = value, OldValue = _getOrdersResult };
                    _getOrdersResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<RadzenDemo.Models.Sample.Product> _getProductsResult;
        protected IEnumerable<RadzenDemo.Models.Sample.Product> getProductsResult
        {
            get
            {
                return _getProductsResult;
            }
            set
            {
                if (!object.Equals(_getProductsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getProductsResult", NewValue = value, OldValue = _getProductsResult };
                    _getProductsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        RadzenDemo.Models.Sample.OrderDetail _orderdetail;
        protected RadzenDemo.Models.Sample.OrderDetail orderdetail
        {
            get
            {
                return _orderdetail;
            }
            set
            {
                if (!object.Equals(_orderdetail, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "orderdetail", NewValue = value, OldValue = _orderdetail };
                    _orderdetail = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var sampleGetOrdersResult = await Sample.GetOrders();
            getOrdersResult = sampleGetOrdersResult;

            var sampleGetProductsResult = await Sample.GetProducts();
            getProductsResult = sampleGetProductsResult;

            orderdetail = new RadzenDemo.Models.Sample.OrderDetail(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(RadzenDemo.Models.Sample.OrderDetail args)
        {
            try
            {
                var sampleCreateOrderDetailResult = await Sample.CreateOrderDetail(orderdetail);
                DialogService.Close(orderdetail);
            }
            catch (System.Exception sampleCreateOrderDetailException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new OrderDetail!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}

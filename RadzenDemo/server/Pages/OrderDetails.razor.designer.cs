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
    public partial class OrderDetailsComponent : ComponentBase
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
        protected RadzenDataGrid<RadzenDemo.Models.Sample.OrderDetail> grid0;

        IEnumerable<RadzenDemo.Models.Sample.OrderDetail> _getOrderDetailsResult;
        protected IEnumerable<RadzenDemo.Models.Sample.OrderDetail> getOrderDetailsResult
        {
            get
            {
                return _getOrderDetailsResult;
            }
            set
            {
                if (!object.Equals(_getOrderDetailsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOrderDetailsResult", NewValue = value, OldValue = _getOrderDetailsResult };
                    _getOrderDetailsResult = value;
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
            var sampleGetOrderDetailsResult = await Sample.GetOrderDetails(new Query() { Expand = "Order,Product" });
            getOrderDetailsResult = sampleGetOrderDetailsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddOrderDetail>("Add Order Detail", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(RadzenDemo.Models.Sample.OrderDetail args)
        {
            var dialogResult = await DialogService.OpenAsync<EditOrderDetail>("Edit Order Detail", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var sampleDeleteOrderDetailResult = await Sample.DeleteOrderDetail(data.Id);
                    if (sampleDeleteOrderDetailResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception sampleDeleteOrderDetailException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete OrderDetail" });
            }
        }
    }
}

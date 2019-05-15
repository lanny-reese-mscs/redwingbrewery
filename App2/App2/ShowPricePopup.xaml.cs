using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowPricePopup : PopupPage
	{
        private string sampJson = @"{'prices': {'lsp':'24.95', 'lsetp':'2.50', 'ssp':'13.95', 'ssetp':'1.50', 'lbp':'17.95', 'lbetp':'2.50', 'sbp':'9.95', 'sbetp':'1.50', 'cbp':'7.95', 'bsp':'7.95', 'ssaladp':'5.95', 'lsaladp':'10.95', 'taxrate':'0.07375'}}";
        public ShowPricePopup ()
		{
			InitializeComponent ();
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"RedWingBreweryPrices.json");
            if (File.Exists(fileName))
            {
                sampJson = File.ReadAllText(fileName);
            }
            Entry priceEntry = this.FindByName<Entry>("PriceEntry");
            priceEntry.Text = sampJson;
		}

        private async void OnClose(object sender, EventArgs e)
        {
            Entry priceEntry = this.FindByName<Entry>("PriceEntry");
            sampJson = priceEntry.Text;
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"RedWingBreweryPrices.json");
            File.WriteAllText(fileName, sampJson);
            await PopupNavigation.Instance.PopAsync();
        }
               
        protected override Task OnAppearingAnimationEndAsync()
        {
            return Content.FadeTo(1);
        }
               
        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return Content.FadeTo(1);
        }
    }
}
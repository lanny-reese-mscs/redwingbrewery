using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Newtonsoft.Json.Linq;
using System.IO;
using Rg.Plugins.Popup.Services;

namespace App2
{

    public partial class MainPage : ContentPage
    {
        private ShowPricePopup showPricePopup;

        private string sampJson = @"{'prices': {'lsp':'24.95', 'lsetp':'2.50',
                                                'ssp':'13.95', 'ssetp':'1.50',
                                                'lbp':'17.95', 'lbetp':'2.50',
                                                'sbp':'9.95', 'sbetp':'1.50',
                                                'cbp':'7.95', 'bsp':'7.95',
                                                'ssaladp':'5.95', 'lsaladp':'10.95',
                                                'taxrate':'0.07375'}}";

        public MainPage()
        {
            InitializeComponent();

            this.showPricePopup = new ShowPricePopup();

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"RedWingBreweryPrices.json");
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, sampJson);
            }
            this.LoadPrices(fileName);
        }

        private void LoadPrices(string fileName)
        {
            using (System.IO.StreamReader reader = File.OpenText(fileName))
            {
                //JObject o = JObject.Parse(sampJson);
                JObject o = (JObject)JToken.ReadFrom(new Newtonsoft.Json.JsonTextReader(reader));
                Decimal inpPrice = 0.0m;
                Decimal.TryParse((string)o["prices"]["lsp"], out inpPrice);
                (this).FindByName<Label>("lsp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["lsetp"], out inpPrice);
                (this).FindByName<Label>("lsetp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["ssp"], out inpPrice);
                (this).FindByName<Label>("ssp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["ssetp"], out inpPrice);
                (this).FindByName<Label>("ssetp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["lbp"], out inpPrice);
                (this).FindByName<Label>("lbp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["lbetp"], out inpPrice);
                (this).FindByName<Label>("lbetp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["sbp"], out inpPrice);
                (this).FindByName<Label>("sbp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["sbetp"], out inpPrice);
                (this).FindByName<Label>("sbetp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["cbp"], out inpPrice);
                (this).FindByName<Label>("cbp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["bsp"], out inpPrice);
                (this).FindByName<Label>("bsp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["ssaladp"], out inpPrice);
                (this).FindByName<Label>("ssaladp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["lsaladp"], out inpPrice);
                (this).FindByName<Label>("lsaladp").Text = inpPrice.ToString("n2");
                Decimal.TryParse((string)o["prices"]["taxrate"], out inpPrice);
                (this).FindByName<Label>("taxrate").Text = inpPrice.ToString("n6");
            }
        }

        private void Lsa_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry lsa = this.FindByName<Entry>("lsa");
            Decimal amount = 0.0m;
            Decimal.TryParse(lsa.Text, out amount);
            Label lsp = this.FindByName<Label>("lsp");
            Decimal cost = 0.0m;
            Decimal.TryParse(lsp.Text, out cost);
            Decimal subtotalpizza = cost * amount;
            Label lsc = this.FindByName<Label>("lsc");
            lsc.Text = subtotalpizza.ToString("n2");
            if (amount > 0)
            {
                Label lss = this.FindByName<Label>("lss");
                Label lsetc = this.FindByName<Label>("lsetc");
                Decimal subtotaltopping = 0.0m;
                Decimal.TryParse(lsetc.Text, out subtotaltopping);
                Decimal subtotal = subtotalpizza + subtotaltopping;
                lss.Text = subtotal.ToString("n2");
                this.calcTotal();
            }
            else
            {
                Entry lset = this.FindByName<Entry>("lset");
                lset.Text = "";
                Label lss = this.FindByName<Label>("lss");
                lss.Text = "0";
            }
        }

        private void Lset_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label lsc = this.FindByName<Label>("lsc");
            Decimal subtotalpizza = 0.0m;
            Decimal.TryParse(lsc.Text, out subtotalpizza);
            if (subtotalpizza > 0)
            {
                Label lsetp = this.FindByName<Label>("lsetp");
                Decimal cost = 0.0m;
                Decimal.TryParse(lsetp.Text, out cost);
                Entry lset = this.FindByName<Entry>("lset");
                Decimal amount = 0.0m;
                Decimal.TryParse(lset.Text, out amount);
                Decimal subtotaltopping = cost * amount;
                Label lsetc = this.FindByName<Label>("lsetc");
                Label lss = this.FindByName<Label>("lss");
                lsetc.Text = subtotaltopping.ToString("n2");
                Decimal subtotal = subtotaltopping + subtotalpizza;
                lss.Text = subtotal.ToString("n2");
            }
            else
            {
                lsetc.Text = 0.0.ToString("n0");
            }
            this.calcTotal();
        }

        private void Ssa_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label ssp = this.FindByName<Label>("ssp");
            Decimal cost = 0.0m;
            Decimal.TryParse(ssp.Text, out cost);
            Entry ssa = this.FindByName<Entry>("ssa");
            Decimal amount = 0.0m;
            Decimal.TryParse(ssa.Text, out amount);
            Decimal subtotalpizza = cost * amount;
            Label ssc = this.FindByName<Label>("ssc");
            ssc.Text = subtotalpizza.ToString("n2");
            Label sss = this.FindByName<Label>("sss");
            Label ssetc = this.FindByName<Label>("ssetc");
            Decimal subtotaltopping = 0.0m;
            Decimal.TryParse(ssetc.Text, out subtotaltopping);
            Decimal subtotal = subtotalpizza + subtotaltopping;
            sss.Text = subtotal.ToString("n2");
            this.calcTotal();
        }

        private void Sset_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label ssetp = this.FindByName<Label>("ssetp");
            Decimal cost = 0.0m;
            Decimal.TryParse(ssetp.Text, out cost);
            Entry sset = this.FindByName<Entry>("sset");
            Decimal amount = 0.0m;
            Decimal.TryParse(sset.Text, out amount);
            Decimal subtotaltopping = cost * amount;
            Label ssetc = this.FindByName<Label>("ssetc");
            ssetc.Text = subtotaltopping.ToString("n2");
            Label sss = this.FindByName<Label>("sss");
            Label ssc = this.FindByName<Label>("ssc");
            Decimal subtotalpizza = 0.0m;
            Decimal.TryParse(ssc.Text, out subtotalpizza);
            Decimal subtotal = subtotalpizza + subtotaltopping;
            sss.Text = subtotal.ToString("n2");
            this.calcTotal();
        }

        private void Lba_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label lbp = this.FindByName<Label>("lbp");
            Decimal cost = 0.0m;
            Decimal.TryParse(lbp.Text, out cost);
            Entry lba = this.FindByName<Entry>("lba");
            Decimal amount = 0.0m;
            Decimal.TryParse(lba.Text, out amount);
            Decimal subtotalpizza = cost * amount;
            Label lbc = this.FindByName<Label>("lbc");
            lbc.Text = subtotalpizza.ToString("n2");
            Label lbs = this.FindByName<Label>("lbs");
            Label lbetc = this.FindByName<Label>("lbetc");
            Decimal subtotaltopping = 0.0m;
            Decimal.TryParse(lbetc.Text, out subtotaltopping);
            Decimal subtotal = subtotalpizza + subtotaltopping;
            lbs.Text = subtotal.ToString("n2");
            this.calcTotal();
        }

        private void Lbet_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label lbetp = this.FindByName<Label>("lbetp");
            Decimal cost = 0.0m;
            Decimal.TryParse(lbetp.Text, out cost);
            Entry lbet = this.FindByName<Entry>("lbet");
            Decimal amount = 0.0m;
            Decimal.TryParse(lbet.Text, out amount);
            Decimal subtotaltopping = cost * amount;
            Label lbetc = this.FindByName<Label>("lbetc");
            lbetc.Text = subtotaltopping.ToString("n2");
            Label lbs = this.FindByName<Label>("lbs");
            Label lbc = this.FindByName<Label>("lbc");
            Decimal subtotalpizza = 0.0m;
            Decimal.TryParse(lbc.Text, out subtotalpizza);
            Decimal subtotal = subtotaltopping + subtotalpizza;
            lbs.Text = subtotal.ToString("n2");
            this.calcTotal();
        }

        private void Sba_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label sbp = this.FindByName<Label>("sbp");
            Decimal cost = 0.0m;
            Decimal.TryParse(sbp.Text, out cost);
            Entry sba = this.FindByName<Entry>("sba");
            Decimal amount = 0.0m;
            Decimal.TryParse(sba.Text, out amount);
            Decimal subtotalpizza = cost * amount;
            Label sbc = this.FindByName<Label>("sbc");
            sbc.Text = subtotalpizza.ToString("n2");
            Label sbs = this.FindByName<Label>("sbs");
            Label sbetc = this.FindByName<Label>("sbetc");
            Decimal subtotaltopping = 0.0m;
            Decimal.TryParse(sbetc.Text, out subtotaltopping);
            Decimal subtotal = subtotalpizza + subtotaltopping;
            sbs.Text = subtotal.ToString("n2");
            this.calcTotal();
        }

        private void Sbet_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label sbetp = this.FindByName<Label>("sbetp");
            Decimal cost = 0.0m;
            Decimal.TryParse(sbetp.Text, out cost);
            Entry sbet = this.FindByName<Entry>("sbet");
            Decimal amount = 0.0m;
            Decimal.TryParse(sbet.Text, out amount);
            Decimal subtotaltopping = cost * amount;
            Label sbetc = this.FindByName<Label>("sbetc");
            sbetc.Text = subtotaltopping.ToString("n2");
            Label sbs = this.FindByName<Label>("sbs");
            Label sbc = this.FindByName<Label>("sbc");
            Decimal subtotalpizza = 0.0m;
            Decimal.TryParse(sbc.Text, out subtotalpizza);
            Decimal subtotal = subtotaltopping + subtotalpizza;
            sbs.Text = subtotal.ToString("n2");
            this.calcTotal();
        }

        private void Cba_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label cbp = this.FindByName<Label>("cbp");
            Decimal cost = 0.0m;
            Decimal.TryParse(cbp.Text, out cost);
            Entry cba = this.FindByName<Entry>("cba");
            Decimal amount = 0.0m;
            Decimal.TryParse(cba.Text, out amount);
            Decimal subtotal = cost * amount;
            Label cbc = this.FindByName<Label>("cbc");
            cbc.Text = subtotal.ToString("n2");
            Label cbs = this.FindByName<Label>("cbs");
            cbs.Text = subtotal.ToString("n2");
            this.calcTotal();
        }

        private void Bsa_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label bsp = this.FindByName<Label>("bsp");
            Decimal cost = 0.0m;
            Decimal.TryParse(bsp.Text, out cost);
            Entry bsa = this.FindByName<Entry>("bsa");
            Decimal amount = 0.0m;
            Decimal.TryParse(bsa.Text, out amount);
            Decimal subtotal = cost * amount;
            Label bsc = this.FindByName<Label>("bsc");
            bsc.Text = subtotal.ToString("n2");
            Label bss = this.FindByName<Label>("bss");
            bss.Text = subtotal.ToString("n2");
            this.calcTotal();
        }

        private void Ssalada_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label ssaladp = this.FindByName<Label>("ssaladp");
            Decimal cost = 0.0m;
            Decimal.TryParse(ssaladp.Text, out cost);
            Entry ssalada = this.FindByName<Entry>("ssalada");
            Decimal amount = 0.0m;
            Decimal.TryParse(ssalada.Text, out amount);
            Decimal subtotal = cost * amount;
            Label ssaladc = this.FindByName<Label>("ssaladc");
            ssaladc.Text = subtotal.ToString("n2");
            Label ssalads = this.FindByName<Label>("ssalads");
            ssalads.Text = subtotal.ToString("n2");
            this.calcTotal();
        }

        private void Lsalada_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label lsaladp = this.FindByName<Label>("lsaladp");
            Decimal cost = 0.0m;
            Decimal.TryParse(lsaladp.Text, out cost);
            Entry lsalada = this.FindByName<Entry>("lsalada");
            Decimal amount = 0.0m;
            Decimal.TryParse(lsalada.Text, out amount);
            Decimal subtotal = cost * amount;
            Label lsaladc = this.FindByName<Label>("lsaladc");
            lsaladc.Text = subtotal.ToString("n2");
            Label lsalads = this.FindByName<Label>("lsalads");
            lsalads.Text = subtotal.ToString("n2");
            this.calcTotal();
        }

        private void sep_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.calcTotal();
        }
        private void calcTotal()
        {
            Decimal total = 0.0m;

            Label lsc = this.FindByName<Label>("lsc");
            Decimal decLsc = 0.0m;
            Decimal.TryParse(lsc.Text, out decLsc);
            Label ssc = this.FindByName<Label>("ssc");
            Decimal decSsc = 0.0m;
            Decimal.TryParse(ssc.Text, out decSsc);
            Label lbc = this.FindByName<Label>("lbc");
            Decimal decLbc = 0.0m;
            Decimal.TryParse(lbc.Text, out decLbc);
            Label sbc = this.FindByName<Label>("sbc");
            Decimal decSbc = 0.0m;
            Decimal.TryParse(sbc.Text, out decSbc);
            Label lsetc = this.FindByName<Label>("lsetc");
            Decimal decLsetc = 0.0m;
            Decimal.TryParse(lsetc.Text, out decLsetc);
            Label ssetc = this.FindByName<Label>("ssetc");
            Decimal decSsetc = 0.0m;
            Decimal.TryParse(ssetc.Text, out decSsetc);
            Label lbetc = this.FindByName<Label>("lbetc");
            Decimal decLbetc = 0.0m;
            Decimal.TryParse(lbetc.Text, out decLbetc);
            Label sbetc = this.FindByName<Label>("sbetc");
            Decimal decSbetc = 0.0m;
            Decimal.TryParse(sbetc.Text, out decSbetc);
            Label cbc = this.FindByName<Label>("cbc");
            Decimal decCbc = 0.0m;
            Decimal.TryParse(cbc.Text, out decCbc);
            Label bsc = this.FindByName<Label>("bsc");
            Decimal decBsc = 0.0m;
            Decimal.TryParse(bsc.Text, out decBsc);
            Label ssaladc = this.FindByName<Label>("ssaladc");
            Decimal decSsaladc = 0.0m;
            Decimal.TryParse(ssaladc.Text, out decSsaladc);
            Label lsaladc = this.FindByName<Label>("lsaladc");
            Decimal decLsaladc = 0.0m;
            Decimal.TryParse(lsaladc.Text, out decLsaladc);
            total = decLsc + decSsc + decLbc + decSbc + decLsetc + decSsetc + decLbetc + decSbetc + decCbc + decBsc + decSsaladc + decLsaladc;

            Label lblTotal = this.FindByName<Label>("total");
            lblTotal.Text = total.ToString("n2");

            Decimal sep = 0.0m;

            Entry seperation = this.FindByName<Entry>("seperation");
            Decimal decSep = 0.0m;
            Decimal.TryParse(seperation.Text, out decSep);
            Label septotal = this.FindByName<Label>("total");
            decimal decTotal = 0.0m;
            decimal.TryParse(septotal.Text, out decTotal);
            if (decSep > 0)
            {
                sep = decTotal / decSep;
            }
            Label lblsep = this.FindByName<Label>("newtotal");
            lblsep.Text = sep.ToString("n2");

            Label taxrate = this.FindByName<Label>("taxrate");
            decimal taxnumber = 0.0m;
            decimal.TryParse(taxrate.Text, out taxnumber);
            decimal tax = 0.0m;
            
            Label taxabletotal = this.FindByName<Label>("newtotal");
            decimal taxtotal = 0.0m;
            decimal.TryParse(taxabletotal.Text, out taxtotal);
            tax = taxtotal * taxnumber;

            Label lbltax = this.FindByName<Label>("tax");
            lbltax.Text = tax.ToString("n2");

            Label lbltotalplustax = this.FindByName<Label>("totalplustax");
            lbltotalplustax.Text = (tax + sep).ToString("n2");

            Switch discountSwitch = this.FindByName<Switch>("discountswitch");
            discountSwitch.IsToggled = false;



        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Decimal discountnumber = .8m;
            decimal finaltotal = 0.0m;

            Label totalplustax = this.FindByName<Label>("totalplustax");
            decimal lbltotalplustax = 0.0m;
            decimal.TryParse(totalplustax.Text, out lbltotalplustax);

            Switch discountSwitch = this.FindByName<Switch>("discountswitch");
            if (discountSwitch.IsToggled)
            {
                finaltotal = lbltotalplustax * discountnumber;
            }
            else
            {
                finaltotal = lbltotalplustax;
            }     

            Label lblgrandtotal = this.FindByName<Label>("grandtotal");
            lblgrandtotal.Text = finaltotal.ToString("n2");
        }

        private void Clearall_Clicked(object sender, EventArgs e)
        {
            Decimal clearall= 0m;
            decimal reset = 0m;
            clearall = reset;

            Entry lsa = this.FindByName<Entry>("lsa");
            lsa.Text = clearall.ToString("n0");
        }
        //private void Oneditpricebuttonclicked(object sender, EventArgs e)
        //{
        //    PopupNavigation.PushAsync(new ShowPricePopup());
        //}

        private async void EnterPriceButton_Clicked(Object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(showPricePopup);
        }
        private void RefreshButton_Clicked(Object sender, EventArgs e)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"RedWingBreweryPrices.json");
            this.LoadPrices(fileName);
        }
    }
}
//Decimal sep = 0.0m;

//Entry seperation = this.FindByName<Entry>("seperation");
//Decimal decSep = 0.0m;
//Decimal.TryParse(seperation.Text, out decSep);
//            Label septotal = this.FindByName<Label>("total");
//decimal decTotal = 0.0m;
//decimal.TryParse(septotal.Text, out decTotal);
//            if (decSep > 0)
//            {
//                sep = decTotal / decSep;
//            }
//            Label lblsep = this.FindByName<Label>("newtotal");
//lblsep.Text = sep.ToString("n2");

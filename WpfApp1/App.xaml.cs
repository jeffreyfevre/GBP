using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.modeles;
using WpfApp1.modèles;
using WpfApp1.wrapper;
using WpfApp1.wrappers;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            manageBDD  datas = new manageBDD();
            //atas.test();
            WrapChantier test  = new WrapChantier();
            WrapCompagnon compagnon = new WrapCompagnon();
            //compagnon.deleteCompagnon(1);
            //Chantier c1 = new Chantier(2, "579 avenue adolphe alphand", "chantier jeffrey", "ontestdestrucs");
            //Chantier c2 = new Chantier(4, "bezier", "chantier florent", "ontestdestrucs");
            //test.createChantier(c1);
            //test.createChantier(c1);
            //test.createChantier(c2);
            //test.getAllChantier();
            //Facture f = new Facture(0, 156, 35000, "cest pas cher");
            //Facture f2 = new Facture(1, 15, 3500, "cest  cher");
            //WrapFacture wrapFacture = new WrapFacture();
            //wrapFacture.createFacture(f);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.modeles;

using WpfApp1.wrapper;
using WpfApp1.wrappers;
using static WpfApp1.modeles.Chantier;
using static WpfApp1.modeles.TraceComptable;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //testChantierWrap();
            //testCompagnon();
            testTraceCompagnon();
            ManageBDD bdd =  new ManageBDD();
            bdd.initBDD();
        }
        private void testChantierWrap()
        {
            WrapChantier wrapChantier = new WrapChantier();
            DateTime dateTime = DateTime.Now;
            int id = 0;
            List<Compagnon> c1 = new List<Compagnon>();
            List<Materiaux> m1 = new List<Materiaux>();
            List<Chantier> ch1 = new List<Chantier>();
            TraceComptable tc1 = new TraceComptable(1500,25f,25f,dateTime,(Types)1,"jaime les pattes",c1,m1, ch1);
            TraceComptable tc2 = new TraceComptable(1500,25f,25f,dateTime,(Types)1,"jaime les pattes",c1,m1, ch1);
            TraceComptable tc3 = new TraceComptable(1500,25f,25f,dateTime,(Types)1,"jaime les pattes",c1,m1,ch1);
            
            List<TraceComptable> jeffTrace = new List<TraceComptable>();
            jeffTrace.Add(tc1);
            jeffTrace.Add(tc2);
            jeffTrace.Add(tc3);
            List<TraceComptable> traces = new List<TraceComptable>();
            traces.Add(tc1);
            traces.Add(tc2);
            traces.Add(tc3);
            List<Compagnon> compagnon = new List<Compagnon>();
            Chantier ch= new Chantier(1500,"1500", "0778792507", dateTime,dateTime,(State)1,"jeffrey adresse","13200","jeffrey chantier","chantier test",jeffTrace,traces,compagnon);
            Chantier ch2= new Chantier(1500,"1500", "0778792507", dateTime,dateTime,(State)1,"jeffrey adresse","13200","jeffrey chantier2","chantier test",jeffTrace,traces,compagnon);
            wrapChantier.createChantier(ch);
            List<Chantier> ch3 = wrapChantier.searchChantierByName("jeffrey chantier");
            wrapChantier.getAllChantier();
            wrapChantier.updateChantier(ch2,ch3[0]._Id);
            List<Chantier> ch4 = wrapChantier.searchChantierByName("jeffrey chantier2");
            Chantier chantier = wrapChantier.readChantier(id);
            for (int i = 0; i < ch3.Count; i++)
            {
                Console.WriteLine(ch3[i]._Id);
                wrapChantier.deleteChantier(ch3[i]);

            }
            for (int i = 0; i < ch4.Count; i++)
            {
                wrapChantier.deleteChantier(ch4[i]);

            }

        }
        private void testCompagnon()
        {
            DateTime dateTime = DateTime.Now;
            WrapCompagnon wrapCompagnon = new WrapCompagnon();
            List<Chantier> chantiers  =new List<Chantier>();
            List<TraceComptable> tcs  =new List<TraceComptable>();
            Compagnon compagnon = new Compagnon(1500, "jeffrey","0778792507",0,dateTime,"jesaipas","paul",chantiers,tcs,tcs);
            Compagnon compagnon2 = new Compagnon(1500, "jeffrey2","0778792507",0,dateTime,"jesaipas","paul",chantiers,tcs,tcs);
            wrapCompagnon.createCompagnon(compagnon);
            List<Compagnon> lc= wrapCompagnon.searchCompagnonByName("jeffrey2");
            wrapCompagnon.updateCompagnon(compagnon2, lc[0]._Id);

            Compagnon c =  wrapCompagnon.readCompagnon(18);
            Console.WriteLine(c.ToString());

            wrapCompagnon.getAllCompagnon();
            for (int i = 0; i < lc.Count; i++)
            {

                Console.WriteLine(lc[i].ToString());
                wrapCompagnon.deleteCompagnon(lc[i]._Id);

            }

        }
        private void testTraceCompagnon()
        {
            DateTime dateTime = DateTime.Now;

            WrapTraceComptable wrapTraceComptable = new WrapTraceComptable();
            List<Compagnon> c1 = new List<Compagnon>();
            List<Materiaux> m1 = new List<Materiaux>();
            List<Chantier> ch1 = new List<Chantier>();
            TraceComptable tc1 = new TraceComptable(1500, 25f, 25f, dateTime, (Types)1, "jaime les pattes", c1, m1,ch1);
            TraceComptable tc2 = new TraceComptable(1500, 26f, 25f, dateTime, (Types)1, "jaime les pattes", c1, m1,ch1);
            wrapTraceComptable.createTraceComptable(tc1);
            TraceComptable tc = wrapTraceComptable.readTraceComptable(10);
            

            List<TraceComptable> ltc =  wrapTraceComptable.searchChantierByName(25);

            for (int i = 0; i < ltc.Count; i++)
            {
                Console.WriteLine(ltc[i].ToString());

                wrapTraceComptable.deleteFacture(ltc[i]);
            }

        }

    }
}

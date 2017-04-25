using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Izomorfizm_grafów_skierowanych
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Graf graf_pierwszy;
        Graf graf_drugi;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_zatwierdz_pierwszy_Click(object sender, RoutedEventArgs e)
        {
            int wierzch;
            bool czy = int.TryParse(ilosc_wierzcholkow_pierwszy.Text, out wierzch);
            if (czy)
            {
                graf_pierwszy = new Graf(wierzch);
                label1.Content = "Ilość wierzchołków grafu: " + ilosc_wierzcholkow_pierwszy.Text + Environment.NewLine +
                    "Wierzchołki są etykietowane kolejnymi liczbami naturalnymi" + Environment.NewLine + "zaczynająć od 1.";
                comboBox_z_pierwszy.Items.Clear();
                comboBox_do_pierwszy.Items.Clear();
                listBox.Items.Clear();

                for (int i = 0; i < wierzch; i++)
                {
                    comboBox_z_pierwszy.Items.Add(i+1);
                    comboBox_do_pierwszy.Items.Add(i+1);
                }
            }
            label10.Content = "";
        }

        private void button_dodaj_pierwszy_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add(comboBox_z_pierwszy.SelectedItem.ToString() + " -> " + comboBox_do_pierwszy.SelectedItem.ToString());
            graf_pierwszy.Dodaj_krawedz((int)comboBox_z_pierwszy.SelectedItem, (int)comboBox_do_pierwszy.SelectedItem);
            comboBox_z_pierwszy.SelectedIndex = -1;
            comboBox_do_pierwszy.SelectedIndex = -1;
            label10.Content = "";
        }

        private void button_zatwierdz_drugi_Click(object sender, RoutedEventArgs e)
        {
            int wierzch;
            bool czy = int.TryParse(ilosc_wierzcholkow_drugi.Text, out wierzch);
            if (czy)
            {
                graf_drugi = new Graf(wierzch);
                label6.Content = "Ilość wierzchołków grafu: " + ilosc_wierzcholkow_drugi.Text + Environment.NewLine +
                    "Wierzchołki są etykietowane kolejnymi liczbami naturalnymi" + Environment.NewLine + "zaczynająć od 1.";
                comboBox_z_drugi.Items.Clear();
                comboBox_do_drugi.Items.Clear();
                listBox2.Items.Clear();

                for (int i = 0; i < wierzch; i++)
                {
                    comboBox_z_drugi.Items.Add(i + 1);
                    comboBox_do_drugi.Items.Add(i + 1);
                }
            }
            label10.Content = "";
        }

        private void button_dodaj_drugi_Click(object sender, RoutedEventArgs e)
        {
            listBox2.Items.Add(comboBox_z_drugi.SelectedItem.ToString() + " -> " + comboBox_do_drugi.SelectedItem.ToString());
            graf_drugi.Dodaj_krawedz((int)comboBox_z_drugi.SelectedItem, (int)comboBox_do_drugi.SelectedItem);
            comboBox_z_drugi.SelectedIndex = -1;
            comboBox_do_drugi.SelectedIndex = -1;
            label10.Content = "";
        }

        private void button_sprawdz_Click(object sender, RoutedEventArgs e)
        {
            bool izomorfizm = true;
            if (graf_pierwszy == null || graf_drugi == null)
            {
                izomorfizm = false;
            }
            else
            {
                if (graf_pierwszy.ilosc_wierzcholkow != graf_drugi.ilosc_wierzcholkow || graf_pierwszy.ilosc_krawedzi != graf_drugi.ilosc_krawedzi)
                {
                    izomorfizm = false;
                }
                else
                {
                    graf_pierwszy.stworz_macierz();
                    graf_drugi.stworz_macierz();
                    if (!Sprawdz_izomorfizm(graf_pierwszy.macierz_incydencji, graf_drugi.macierz_incydencji))
                    {
                        izomorfizm = false;
                    }
                    if (graf_pierwszy.ilosc_wierzcholkow == graf_drugi.ilosc_wierzcholkow && graf_pierwszy.ilosc_krawedzi == 0 && graf_drugi.ilosc_krawedzi == 0)
                    {
                        izomorfizm = true;
                    }
                }
            }
            


            if (izomorfizm)
            {
                label10.Content = "Pierwszy graf oraz drugi graf są grafami izomorficznymi";
            }
            else
            {
                label10.Content = "Pierwszy graf oraz drugi graf nie są grafami izomorficznymi";
            }
        }
        public static bool Sprawdz_izomorfizm(int[,] macierz_pierwsza, int[,] macierz_druga)
        {
            bool flaga = false;
            List<int[]> lista1 = new List<int[]>();
            for (int i = 0; i < macierz_druga.GetLength(0); i++)
            {
                int[] tab = new int[macierz_druga.GetLength(1)];
                for (int j = 0; j < macierz_druga.GetLength(1); j++)
                {
                    tab[j] = macierz_druga[i, j];
                }
                lista1.Add(tab);
            }
            foreach (var item in Znajdź_permutacje(lista1))
            {
                List<int[]> lista2 = new List<int[]>();
                for (int i = 0; i < item[0].Length; i++)
                {
                    int[] tab = new int[item.Count];
                    for (int j = 0; j < item.Count; j++)
                    {
                        tab[j] = item[j][i];
                    }
                    lista2.Add(tab);
                }
                foreach (var item2 in Znajdź_permutacje(lista2))
                {
                    if (Porównaj(macierz_pierwsza, item2))
                    {
                        flaga = true;
                    }

                }

            }
            return flaga;
        }
        public static bool Porównaj(int[,] macierz_pierwsza, List<int[]> lista_druga)
        {
            bool flaga = true;
            for (int i = 0; i < macierz_pierwsza.GetLength(0); i++)
            {
                for (int j = 0; j < macierz_pierwsza.GetLength(1); j++)
                {
                    if (macierz_pierwsza[i, j] != lista_druga[j][i])
                    {
                        flaga = false;
                    }
                }
            }
            return flaga;
        }
        private static List<List<int[]>> Znajdź_permutacje(List<int[]> macierz)
        {
            var wynik = new List<List<int[]>>();
            if (macierz.Count == 1)
            {
                wynik.Add(macierz);
            }
            else
            {
                for (int i = 0; i < macierz.Count; i++)
                {
                    List<int[]> tail = new List<int[]>(macierz);
                    tail.RemoveAt(i);
                    foreach (var item in Znajdź_permutacje(tail))
                    {
                        item.Insert(0, macierz[i]);
                        wynik.Add(item);
                    }
                }
            }
            return wynik;
        }
    }
}

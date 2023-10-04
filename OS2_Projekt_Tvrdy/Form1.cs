using System.IO.Compression;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OS2_Projekt_Tvrdy
{
    public partial class Form1 : Form
    {
        string odabrani = "";
        RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        RSA rsa = RSA.Create();

        RSAParameters privatni;
        RSAParameters javni;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnDekriptiraj.Visible = false;
            btnKriptiraj.Visible = false;
            btnPotpisi.Visible = false;
            btnProvjeriPotpis.Visible = false;

            List<string> algoritmi = new List<string>();
            algoritmi.Add("AES");
            algoritmi.Add("RSA");
            algoritmi.Add("SHA256");

            cbAlgoritam.DataSource = algoritmi;
        }

        private void btnOdaberi_Click(object sender, EventArgs e)
        {
            if (cbAlgoritam.SelectedIndex == 0)
            {
                odabrani = "AES";

                btnDekriptiraj.Visible = true;
                btnKriptiraj.Visible = true;
                KreiranjeKljucaAES();
            }
            else if (cbAlgoritam.SelectedIndex == 1)
            {
                odabrani = "RSA";
                btnDekriptiraj.Visible = true;
                btnKriptiraj.Visible = true;
                KreiranjeKljucevaRSA();
            }
            else if (cbAlgoritam.SelectedIndex == 2)
            {
                odabrani = "SHA256";
                IzracunSazetka();
            }
        }

        public void KreiranjeKljucaAES()
        {
            using (Aes aes = Aes.Create())
            {
                byte[] kljucAES = aes.Key;
                byte[] ivAES = aes.IV;

                File.WriteAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\tajni.txt", kljucAES);
                File.WriteAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\IV.txt", ivAES);
                MessageBox.Show("Tajni kljuè je kreiran!");
            }
        }

        public void EnkriptirajAES()
        {
            byte[] kljucAES = File.ReadAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\tajni.txt");
            byte[] IVAES = File.ReadAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\IV.txt");
            string tekst = File.ReadAllText(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\tekst.txt");
            byte[] enkriptirano;

            using (Aes aes = Aes.Create())
            {
                ICryptoTransform enkriptor = aes.CreateEncryptor(kljucAES, IVAES);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, enkriptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(tekst);
                        }
                        enkriptirano = ms.ToArray();
                    }
                }
            }
            File.WriteAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\enkripcijaaes.txt", enkriptirano);
            MessageBox.Show("Enkripcija AES je dovršena");
        }


        private void DekriptirajAES()
        {
            byte[] kljucAES = File.ReadAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\tajni.txt");
            byte[] IVAES = File.ReadAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\IV.txt");
            byte[] fileAES = File.ReadAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\enkripcijaaes.txt");

            string dekriptirano = "";

            using (Aes aes = Aes.Create())
            {
                ICryptoTransform dekriptor = aes.CreateDecryptor(kljucAES, IVAES);

                using (MemoryStream ms = new MemoryStream(fileAES))
                {
                    using (CryptoStream cs = new CryptoStream(ms, dekriptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                            dekriptirano = reader.ReadToEnd();
                    }
                }
            }
            File.WriteAllText(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\dekripcijaaes.txt", dekriptirano);
            MessageBox.Show("Dekripcija AES je dovršena");
        }
        private void KreiranjeKljucevaRSA()
        {
            privatni = csp.ExportParameters(true);
            javni = csp.ExportParameters(false);

            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, javni);
            string javniString = sw.ToString();
            File.WriteAllText(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\javni.txt", javniString);

            var sw2 = new StringWriter();
            var xs2 = new XmlSerializer(typeof(RSAParameters));
            xs2.Serialize(sw2, privatni);
            string privatniString = sw2.ToString();
            File.WriteAllText(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\privatni.txt", privatniString);
            MessageBox.Show("Par kljuèeva je kreiran!");
        }

        public void EnkriptirajRSA()
        {
            string javniKljuc = File.ReadAllText(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\javni.txt");
            StringReader sr = new StringReader(javniKljuc);
            XmlSerializer xs = new XmlSerializer(typeof(RSAParameters));
            RSAParameters javni = (RSAParameters)xs.Deserialize(sr);

            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(javni);
            string tekst = File.ReadAllText(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\tekst.txt");
            byte[] zipano = Zip(tekst);
            var kriptirano = csp.Encrypt(zipano, false);
            File.WriteAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\enkripcijarsa.txt", kriptirano);
            MessageBox.Show("Enkripcija RSA izvršena!");
        }

        public void DekriptirajRSA()
        {
           csp = new RSACryptoServiceProvider();
           csp.ImportParameters(privatni);

           byte[] tekst = File.ReadAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\enkripcijarsa.txt");
           var dekriptirano = csp.Decrypt(tekst, false);
           string unzipano = Unzip(dekriptirano);
           File.WriteAllText(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\dekripcijarsa.txt", unzipano);
            MessageBox.Show("Dekripcija RSA izvršena!"); 
        }

        public static byte[] Zip(string str)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(str);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    CopyTo(msi, gs);
                }
                return mso.ToArray();
            }
        }
        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    CopyTo(gs, mso);
                }
                return System.Text.Encoding.UTF8.GetString(mso.ToArray());
            }
        }
        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        private void btnKriptiraj_Click(object sender, EventArgs e)
        {
            if (odabrani == "AES")
            {
                EnkriptirajAES();
            }
            else if (odabrani == "RSA")
            {
                EnkriptirajRSA();
            }
            else
            {
                MessageBox.Show("Odaberite AES ili RSA algoritam!");
            }
        }

        private void btnDekriptiraj_Click(object sender, EventArgs e)
        {
            if (odabrani == "AES")
            {
                DekriptirajAES();
            }
            else if (odabrani == "RSA")
            {
                DekriptirajRSA();
            }
            else
            {
                MessageBox.Show("Odaberite AES ili RSA algoritam!");
            }
        }

        private void IzracunSazetka()
        {
            using (SHA256 SHA256 = SHA256.Create())
            {
                btnPotpisi.Visible = true;
                btnProvjeriPotpis.Visible = true;

                byte[] tekst = File.ReadAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\tekst.txt");
                byte[] sazetak = SHA256.ComputeHash(tekst);
                File.WriteAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\sazetak.txt", sazetak);
                MessageBox.Show("Sažetak poruke uspješno kreiran!");
            }
        }

        public RSAParameters rsaParameters { get; set; }
        
        private void btnPotpisi_Click(object sender, EventArgs e)
        {
            byte[] tekst = File.ReadAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\tekst.txt");
            using (var sha256 = SHA256.Create())
            {
                try
                {
                    byte[] sazetak = sha256.ComputeHash(tekst);
                    rsaParameters = rsa.ExportParameters(true);
                    RSAPKCS1SignatureFormatter rsaFormatter = new(rsa);
                    rsaFormatter.SetHashAlgorithm(nameof(sha256));

                    var potpis = rsaFormatter.CreateSignature(sazetak);
                    File.WriteAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\potpis.txt", potpis);
                    MessageBox.Show("Digitalni potpis je uspješan!");
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Digitalni potpis nije uspješan!");
                }
            }
        }

        private void btnProvjeriPotpis_Click(object sender, EventArgs e)
        {
            byte[] tekst = File.ReadAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\tekst.txt");
            byte[] prijasnjiPotpisBitovi = File.ReadAllBytes(@"C:\Users\Veronika\Desktop\OS2_Projekt_Tvrdy\Dokumenti\potpis.txt");

            using (var sha256 = SHA256.Create())
            {
                try
                {
                    var sazetakTeksta = sha256.ComputeHash(tekst);

                    rsaParameters = rsa.ExportParameters(true);
                    RSAPKCS1SignatureFormatter rsaFormatter = new(rsa);
                    rsaFormatter.SetHashAlgorithm(nameof(sha256));

                    var potpisaniTekstBitovi = rsaFormatter.CreateSignature(sazetakTeksta);

                    string prijasnjiPotpis = Convert.ToBase64String(prijasnjiPotpisBitovi);
                    string potpisaniTekst = Convert.ToBase64String(potpisaniTekstBitovi);

                    if (prijasnjiPotpis == potpisaniTekst)
                    {
                        MessageBox.Show("Valjan digitalni potpis!");

                    }
                    else
                    {
                        MessageBox.Show("Digitalni potpis nije valjan!");
                    }
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Greška provjere digitalnog potpisa!");
                }
            }
        }
    }
}
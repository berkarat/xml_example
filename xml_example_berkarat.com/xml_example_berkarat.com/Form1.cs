using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace xml_example_berkarat.com
{
    public partial class Form1 : Form
    {
        Product prd = new Product();
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        public Form1()
        {
            InitializeComponent();
            for (int i = 1; i < Get_Datas().Rows.Count; i++)
            {
                comboBox1.Items.Add(new ComboboxItem { Text = Get_Datas().Rows[i][1].ToString(), Value = Get_Datas().Rows[i][0].ToString() });
            }
            comboBox1.SelectedIndex = 0;
            prd = test(Get_Datas());
           richTextBox1.Text = XMLCreator();
            XMLDeserialize(XMLCreator());
         string []asd=  xml_parser(richTextBox1.Text);
          

        }
        #region EVENTS 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prd.ProductDetay!=null)
            {
                MessageBox.Show(prd.ProductDetay[Convert.ToInt32((comboBox1.SelectedItem as ComboboxItem).Value) - 1].ProductName.ToString());


            }
            //MessageBox.Show(prd.ProductDetay[Convert.ToInt32((comboBox1.SelectedItem as ComboboxItem).Value)].ProductSerialNumber.ToString());
        }
        #endregion
        #region Methods
        public void XMLDeserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Product));
            using (TextReader reader = new StringReader(xml))
            {
                Product result = (Product)serializer.Deserialize(reader);
                //MessageBox.Show(result.ProductDetay[0].ProductionPlace);
            }

        }
        public Product test(DataTable dt)
        {
            prd = new Product();
            prd.ProductDetay = new ProductDetails[dt.Rows.Count];
            Random rnd = new Random();
            prd.ID = rnd.Next();
            prd.Type = "Hardware";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProductDetails de = new ProductDetails();
                de.ProductName = dt.Rows[i]["NAME"].ToString();
                de.ProductSerialNumber = dt.Rows[i]["TYPE"].ToString();
                prd.ProductDetay[i] = de;
            }
            return prd;
        }
        public string XMLCreator()
        {
            XmlSerializer XML = new XmlSerializer(typeof(Product));
            using (var StringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(StringWriter))
                {
                    XML.Serialize(writer, prd);
                    return StringWriter.ToString();
                }
            }
        }
        public DataTable Get_Datas()
        {
            try
            {
                SqlConnection sqlcon = new SqlConnection("Server=DESKTOP-U34S2C6; Database=test; User Id=berk;Password = 123456; ");
                sqlcon.Open();
                SqlCommand _cmd = new SqlCommand(@"SELECT TOP 1000 [ID] ,[NAME] ,[TYPE]  FROM [test].[dbo].[tbl_hardware]", sqlcon);
                SqlDataAdapter dap = new SqlDataAdapter(_cmd);
                DataTable dt = new DataTable();
                dap.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string[] xml_parser(string xmlmessage)
        {
            string[] foundvalue;

            if (xmlmessage != null)
            {
                string _ProductName = "<ProductSerialNumber>";


                int[,] foundindex = new int[11, 2];
                foundvalue = new string[12];


                #region parse xml message


                if (xmlmessage.Length > 0) xmlmessage = xmlmessage.Replace("&lt;", "<");
                if (xmlmessage.Length > 0) xmlmessage = xmlmessage.Replace("&gt;", ">");

                //MERCHANT_ID
                foundindex[0, 0] = xmlmessage.IndexOf(_ProductName) + _ProductName.Length;
                foundindex[0, 1] = xmlmessage.IndexOf("</ProductSerialNumber>");
                if (foundindex[0, 1] != -1) foundvalue[0] = xmlmessage.Substring(foundindex[0, 0], foundindex[0, 1] - foundindex[0, 0]);


                #endregion
                return foundvalue;
            }

            else
            {
                return null;
            }
        }
    }
    #endregion
}




using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Security.Cryptography;

namespace CORNCommon.Classes
{
    public class clsWindowsFormUtil
    {
        public static void FillComboBox(ComboBox pCboControl, DataTable pObjTable, int pColKey, int pColValue, bool pBClearListItems)
        {
            if (pObjTable != null)
            {
                // Clear list items if asked.
                if (pBClearListItems)
                {
                    pCboControl.Items.Clear();
                }
                // Add the list elements to the ComboBox.
                for (int nI = 0; nI < pObjTable.Rows.Count; nI++)
                {
                    // Add each element as clsListItem.
                    pCboControl.Items.Add(new clsListItems(pObjTable.Rows[nI][pColValue].ToString(), pObjTable.Rows[nI][pColKey].ToString()));
                }
            }
            else
            {
                pCboControl.Items.Clear();
            }
        }

        public static void FillListBox(ListBox pLstControl, DataTable pObjTable, int pColKey, int pColValue, bool pBClearListItems)
        {
            if (pLstControl != null)
            {
                if (pObjTable != null)
                {
                    if (pBClearListItems)
                    {
                        pLstControl.DataSource = null;
                        pLstControl.Items.Clear();
                    }
                    // Add the list elements to the ListBox.
                    for (int nI = 0; nI < pObjTable.Rows.Count; nI++)
                    {
                        // Add each element as clsListItem.
                        pLstControl.Items.Add(new clsListItems(pObjTable.Rows[nI][pColValue].ToString(), pObjTable.Rows[nI][pColKey].ToString()));
                    }
                }
                else
                {
                    pLstControl.Items.Clear();
                }
            }
        }

        public static bool DeleteConfirmation(string _message)
        {
            DialogResult _dialogResult = MessageBox.Show(_message,"Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (_dialogResult == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public static bool Are_Rows_Selected(DataGridView dg, string _message)
        {
            bool _Is_Selected = false;
            foreach (DataGridViewRow dr in dg.Rows)
            {
                if (Convert.ToBoolean(dr.Cells["Select"].Value))
                {
                    if (DeleteConfirmation(_message))
                    {
                        _Is_Selected = true;
                    }
                    break;
                }
            }
            return _Is_Selected;
        }
        
        public static string Encrypt(string input, string key)
         {
             byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
             TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
             tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
             tripleDES.Mode = CipherMode.ECB;
             tripleDES.Padding = PaddingMode.PKCS7;
             ICryptoTransform cTransform = tripleDES.CreateEncryptor();
             byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
             tripleDES.Clear();
             return Convert.ToBase64String(resultArray, 0, resultArray.Length);
         }
        
        public static string Decrypt(string input, string key)
         {
             byte[] inputArray = Convert.FromBase64String(input);
             TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
             tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
             tripleDES.Mode = CipherMode.ECB;
             tripleDES.Padding = PaddingMode.PKCS7;
             ICryptoTransform cTransform = tripleDES.CreateDecryptor();
             byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
             tripleDES.Clear();
             return UTF8Encoding.UTF8.GetString(resultArray);
         }

        public class SetComboboxItem
         {
             public string Text { get; set; }
             public object Value { get; set; }

             public override string ToString()
             {
                 return Text;
             }
         }

        public static void InputNum(KeyPressEventArgs e)
         {
             if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
             {
                 e.Handled = true;
             }
         }

        public class HoverColoers
        {
            public void MouseEnter()
            {

            }
            public void MouseLeave()
            {

            }
        }

        public static int[] GetLocation(int Parent_width, int Parent_height, int x, int y)
        {
            int _width = Parent_width;
            int x1 = (_width / 2) - x;

            int _height = Parent_height;
            int y1 = (_height / 2) - y;

            int[] _location = { x1, y1 };
            return _location;
        }
    }
}

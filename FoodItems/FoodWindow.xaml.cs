using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WildlifeTrackerV3.FoodItems;

namespace WildlifeTrackerV3
{
    /// <summary>
    /// Interaction logic for FoodWindow.xaml
    /// </summary>
    public partial class FoodWindow : Window
    {
        private FoodItem foodItem = new FoodItem();

        public FoodWindow()
        {
            InitializeComponent();
        }

        public FoodItem FoodItem
        {
            get { return foodItem; }
            set { foodItem = value; }
        }

      
        private bool ReadName ()
        {
            bool ok = false;
            string name = txtFoodName.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                foodItem.Name = name;
                ok = true;
            }
            return ok;
        }

        private bool ReadIngredient()
        {
            bool okIngredient = false;
            string ingredient = txtIngredient.Text.Trim();
            if (!string.IsNullOrEmpty(ingredient)) 
            {
                foodItem.Ingredients.Add(ingredient);
                okIngredient = true;
            } 

            return okIngredient;
        }

        /// <summary>
        /// Updates the list of ingredients. Needed when an ingredient is added, changed and deleted. 
        /// </summary>
        private void UpdateGUI()
        {
            lbxIngredients.ItemsSource = null;
            lbxIngredients.ItemsSource = foodItem.Ingredients.ToStringArray();
            txtIngredient.Text = string.Empty;
        }

        /// <summary>
        /// With clicking on the Add button, the ingredient is validated and set in the FoodItem object.
        /// The GUI is updated with the ingredient. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddIngredient_Click (object sender, RoutedEventArgs e)
        {
           bool ok = ReadIngredient();
            if (ok)
            {
                UpdateGUI();
            }
        }

        /// <summary>
        /// When clicking the delete button, the selected ingredient is removed from the list of ingredients and the GUI is updated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteIngredient_Click (Object sender, RoutedEventArgs e)
        {
            int selected = lbxIngredients.SelectedIndex;
            if (selected == -1)
                MessageBox.Show("Please select an igredient to delete.");
            else
            {
                bool success = foodItem.Ingredients.RemoveAt(selected);
                if (!success)
                    MessageBox.Show("Failed to delete ingredient.");
                else
                    UpdateGUI();
            }
                
        }


        /// <summary>
        /// When clicking the change button the selected ingredient is replaced with the ingredient in the textbox and the GUI is updated. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeIngredient_Click (object sender, RoutedEventArgs e)
        {

            int selected = lbxIngredients.SelectedIndex;
            string changedIngredient = txtIngredient.Text.Trim();
            if ((!string.IsNullOrEmpty(changedIngredient)) && (selected >= 0))
            {
                bool success = foodItem.Ingredients.ChangeAt(selected, changedIngredient);
                if (!success)
                    MessageBox.Show("Failed to change ingredient.");
                else
                    UpdateGUI();
            }
            else
                MessageBox.Show("Provide valid input or select a valid ingredient to change.");
        }


        /// <summary>
        /// When clicking on the OK  button the name of the FoodItem is set (the igredients are set previously) and the FoodWindow closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            bool ok = ReadName();
            if (ok)
            {
                this.DialogResult = true;
            }
            else
                MessageBox.Show("Please provide a name.");

        }


        /// <summary>
        /// When clicking on the Cancel button the FoodWindow closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click (object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

      

    }
}

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;  
using System.IO;
using WildlifeTrackerSystem.Animals;
using WildlifeTrackerSystem.Mammals;
using WildlifeTrackerSystem.Birds;
using WildlifeTrackerSystem.Fish;
using WildlifeTrackerV2.Animals;
using WildlifeTrackerV2.Nutrition;
using WildlifeTrackerV3;
using WildlifeTrackerV3.FoodItems;
using WildlifeTrackerV4.Utilities;
using Microsoft.VisualBasic.FileIO;
using WildlifeTrackerV3.List;

namespace WildlifeTrackerSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AnimalManager manager = new AnimalManager();

        bool isSaved = true; //track if data is saved
        string filePath = string.Empty;
        string currentFormat = string.Empty; //text or JSON
        

        public MainWindow()
        {
            InitializeComponent();
            InitializeGUI();
        }

        // 1. This part is for GUI code 

        /// <summary>
        /// Setting a title and positioning the window as well as binding the ComboBox and ListBoxes to the corresponding enum.
        /// </summary>
        private void InitializeGUI()
        {
            this.Title = "Wildlife Tracking System - V4 by Nikolina Vasikj";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            cmbGender.ItemsSource = Enum.GetValues(typeof(Gender));
            cmbGender.SelectedItem = Gender.Unknown;

            lbxCategoryType.ItemsSource = Enum.GetValues(typeof(Category));
            lbxCategoryType.SelectedItem = Category.Mammal;
            Category selectedCategory = (Category)lbxCategoryType.SelectedItem;
            UpdateCategorySpecificationsBox(selectedCategory);

            lbxSpecies.ItemsSource = Enum.GetValues(typeof(MammalSpecies));
            lbxSpecies.SelectedItem = MammalSpecies.Cat;
            UpdateSpeciesSpecificationsBox(MammalSpecies.Cat);
        }

        /// <summary>
        /// Updates the GroupBox with attributes specific to the selected animal category.
        /// </summary>
        /// <param name="selectedCategory"></param>
        private void UpdateCategorySpecificationsBox(Category selectedCategory)
        {
            switch (selectedCategory)
            {
                case Category.Mammal:
                    gbxCategorySpecificData.Header = "Mammal specifications";
                    lblCategoryData1.Content = "Habitat";
                    lblCategoryData2.Content = "Teeth count";
                    break;

                case Category.Bird:
                    gbxCategorySpecificData.Header = "Bird specifications";
                    lblCategoryData1.Content = "Wing span(m)";
                    lblCategoryData2.Content = "Flight speed(km/h)";
                    break;

                case Category.Fish:
                    gbxCategorySpecificData.Header = "Fish specifications";
                    lblCategoryData1.Content = "Water type";
                    lblCategoryData2.Content = "Fin count";
                    break;
            }
        }

        /// <summary>
        /// The following 3 methods are overloaded and they can be called with each of the species. 
        /// These methods are used to update the GroupBox with arrtibutes specific to each species. 
        /// </summary>
        /// <param name="species"></param>
        private void UpdateSpeciesSpecificationsBox(MammalSpecies species)
        {
            switch (species)
            {
                case MammalSpecies.Cat:
                    gbxSpeciesSpecifications.Header = "Cat specifications";
                    lblSpeciesData1.Content = "Breed";
                    lblSpeciesData2.Content = "Fur color";
                    break;
                case MammalSpecies.Otter:
                    gbxSpeciesSpecifications.Header = "Otter specifications";
                    lblSpeciesData1.Content = "Dive depth(m)";
                    lblSpeciesData2.Content = "Body length(cm)";
                    break;
                case MammalSpecies.Tiger:
                    gbxSpeciesSpecifications.Header = "Tiger specifications";
                    lblSpeciesData1.Content = "Land of origin";
                    lblSpeciesData2.Content = "Conservation status";
                    break;
            }
        }

        private void UpdateSpeciesSpecificationsBox(BirdSpecies species)
        {
            switch (species)
            {
                case BirdSpecies.Falcon:
                    gbxSpeciesSpecifications.Header = "Falcon specifications";
                    lblSpeciesData1.Content = "Nesting season";
                    lblSpeciesData2.Content = "Beak length(cm)";
                    break;
                case BirdSpecies.Stork:
                    gbxSpeciesSpecifications.Header = "Stork specifications";
                    lblSpeciesData1.Content = "Migration months";
                    lblSpeciesData2.Content = "Nest size(m)";
                    break;

            }
        }

        private void UpdateSpeciesSpecificationsBox(FishSpecies species)
        {
            switch (species)
            {
                case FishSpecies.Goldfish:
                    gbxSpeciesSpecifications.Header = "Goldfish specifications";
                    lblSpeciesData1.Content = "Water temp(C)";
                    lblSpeciesData2.Content = "Color";
                    break;
                case FishSpecies.White_shark:
                    gbxSpeciesSpecifications.Header = "White shark specifications";
                    lblSpeciesData1.Content = "Swimming depth(m)";
                    lblSpeciesData2.Content = "Swimming speed(km/h)";
                    break;
                case FishSpecies.Orange_clownfish:
                    gbxSpeciesSpecifications.Header = "Orange clownfish specifications";
                    lblSpeciesData1.Content = "Size at maturity(cm)";
                    lblSpeciesData2.Content = "Eggs layed";
                    break;
            }
        }


        /// <summary>
        /// Binds the species ListBox with species that fall under the category that is given as a parameter.
        /// </summary>
        /// <param name="category"></param>
        private void UpdateSpeciesList(Category category)
        {
            switch (category)
            {
                case Category.Mammal:
                    lbxSpecies.ItemsSource = Enum.GetValues(typeof(MammalSpecies));
                    lbxSpecies.SelectedItem = MammalSpecies.Cat;
                    break;
                case Category.Bird:
                    lbxSpecies.ItemsSource = Enum.GetValues(typeof(BirdSpecies));
                    lbxSpecies.SelectedItem = BirdSpecies.Falcon;
                    break;
                case Category.Fish:
                    lbxSpecies.ItemsSource = Enum.GetValues(typeof(FishSpecies));
                    lbxSpecies.SelectedItem = FishSpecies.Goldfish;
                    break;
            }
        }


        /// <summary>
        /// Event that handles the UI changes when a Category is selected in the Category ListBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxCategoryType_SelectionChanged(object sender, EventArgs e)
        {
            Category selectedCategory = (Category)lbxCategoryType.SelectedItem;
            lbxSpecies.SelectionChanged -= lbxSpecies_SelectionChanged; //Unsubscribe from the event to prevent a NullReference exception.
            UpdateSpeciesList(selectedCategory);
            UpdateCategorySpecificationsBox(selectedCategory);



            if (lbxSpecies.SelectedItem != null)
            {
                if (selectedCategory == Category.Mammal)
                {
                    MammalSpecies mammalSpecies = (MammalSpecies)lbxSpecies.SelectedItem;
                    UpdateSpeciesSpecificationsBox(mammalSpecies);
                }
                else if (selectedCategory == Category.Bird)
                {
                    BirdSpecies birdSpecies = (BirdSpecies)lbxSpecies.SelectedItem;
                    UpdateSpeciesSpecificationsBox(birdSpecies);
                }
                else
                {
                    FishSpecies fishSpecies = (FishSpecies)lbxSpecies.SelectedItem;
                    UpdateSpeciesSpecificationsBox(fishSpecies);
                }

            }

            lbxSpecies.SelectionChanged += lbxSpecies_SelectionChanged; //Attach the event handler method to the event again.
        }



        /// <summary>
        /// Event that handles the UI changes when an item in the the Species ListBox is selected.
        /// Updates the GroupBox for species attributes and also populates the ListBox with species from each category when the checkbox is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxSpecies_SelectionChanged(object sender, EventArgs e)
        {
            Category selectedCategory = (Category)lbxCategoryType.SelectedItem;

            if (lbxSpecies != null)
            {
                if (selectedCategory == Category.Mammal)
                {
                    MammalSpecies mammalSpecies = (MammalSpecies)lbxSpecies.SelectedItem;
                    UpdateSpeciesSpecificationsBox(mammalSpecies);
                }
                else if (selectedCategory == Category.Fish)
                {
                    FishSpecies fishSpecies = (FishSpecies)lbxSpecies.SelectedItem;
                    UpdateSpeciesSpecificationsBox(fishSpecies);
                }
                else
                {
                    BirdSpecies birdSpecies = (BirdSpecies)lbxSpecies.SelectedItem;
                    UpdateSpeciesSpecificationsBox(birdSpecies);
                }
            }
            else
            {
                MessageBox.Show("Please select valid species");
            }

            if (checkDisplayAllAnimals.IsChecked == true)
            {
                lbxCategoryType.SelectionChanged -= lbxCategoryType_SelectionChanged;// Unsubscribe from the event handler for changing the selected category to prevent its instructions from interfering with the UI when the checkbox is checked.
                AllAnimalsDisplayedUIChanges();
                lbxCategoryType.SelectionChanged += lbxCategoryType_SelectionChanged;// Attach the event handler again.
            }

        }


        /// <summary>
        /// Event that handles the UI when the checkbox is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkDisplayAllAnimals_Checked(object sender, EventArgs e)
        {
            DisplayAllAnimals();

            AllAnimalsDisplayedUIChanges();
        }

        /// <summary>
        /// Crates an enum for storing every animal and binds it to the Species ListBox. 
        /// Disables the Category ListBox.
        /// </summary>
        private void DisplayAllAnimals()
        {
            List<Enum> animals = new List<Enum>();

            animals.AddRange(Enum.GetValues(typeof(MammalSpecies)).Cast<Enum>());
            animals.AddRange(Enum.GetValues(typeof(BirdSpecies)).Cast<Enum>());
            animals.AddRange(Enum.GetValues(typeof(FishSpecies)).Cast<Enum>());

            lbxSpecies.ItemsSource = animals;

            lbxCategoryType.IsEnabled = false;
        }

        /// <summary>
        /// Makes sure that the right atributes are displayed in the GroupBoxes.
        /// </summary>
        private void AllAnimalsDisplayedUIChanges()
        {
            if (checkDisplayAllAnimals.IsChecked == true)
            {
                if (lbxSpecies.SelectedItem is MammalSpecies)
                {
                    lbxCategoryType.SelectedItem = Category.Mammal;
                    UpdateCategorySpecificationsBox(Category.Mammal);
                    MammalSpecies selectedSpecies = (MammalSpecies)lbxSpecies.SelectedItem;
                    UpdateSpeciesSpecificationsBox(selectedSpecies);
                }
                else if (lbxSpecies.SelectedItem is BirdSpecies)
                {
                    lbxCategoryType.SelectedItem = Category.Bird;
                    UpdateCategorySpecificationsBox(Category.Bird);
                    BirdSpecies selectedSpecies = (BirdSpecies)lbxSpecies.SelectedItem;
                    UpdateSpeciesSpecificationsBox(selectedSpecies);
                }
                else if (lbxSpecies.SelectedItem is FishSpecies)
                {
                    lbxCategoryType.SelectedItem = Category.Fish;
                    UpdateCategorySpecificationsBox(Category.Fish);
                    FishSpecies selectedSpecies = (FishSpecies)lbxSpecies.SelectedItem;
                    UpdateSpeciesSpecificationsBox(selectedSpecies);
                }
            }
        }

        /// <summary>
        /// Event handler for getting the UI back to "normal" when the checkbox is unchecked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkDisplayAllAnimals_Unchecked(object sender, RoutedEventArgs e)
        {
            lbxCategoryType.IsEnabled = true;
            Category selectedCategory = (Category)lbxCategoryType.SelectedItem;
            UpdateSpeciesList(selectedCategory);
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                image.Source = bitmap;
            }
        }



        // 2.This part is for reading user input

        private string ReadString(string text, out bool ok)
        {
            ok = false;
            if (!string.IsNullOrEmpty(text))
                ok = true;
            else
            {
                ok = false;
            }

            return text;
        }

        private double ReadDouble(string text, out bool ok)
        {
            ok = false;
            double number = 0.0;
            if (double.TryParse(text, out number))
                ok = true;
            else
            {
                ok = false;
            }
            return number;
        }

        private int ReadInt(string text, out bool ok)
        {
            ok = false;
            int number = 0;
            if (int.TryParse(text, out number))
                ok = true;
            else
            {
                ok = false;
            }

            return number;
        }

        private bool ReadCommonValues(Animal animal)
        {
            bool okName = false;
            bool okAge = false;
            string name = ReadString(txtName.Text.Trim(), out okName);
            double age = ReadDouble(txtAge.Text.Trim(), out okAge);

            bool success = false;
            if (okName && okAge)
            {
                animal.Name = name;
                animal.Age = age;
                animal.Gender = (Gender)cmbGender.SelectedItem;
                animal.Category = (Category)lbxCategoryType.SelectedItem;

                success = true;
            }
            return success;
        }

        /// <summary>
        /// Read input specific to the mammal category.
        /// </summary>
        /// <returns></returns>
        private (string, int) ReadMammalInput(out bool ok)
        {
            string habitat = txtCategoryData1.Text.Trim();
            int teethCount = 0;

            ok = true;

            if (string.IsNullOrEmpty(habitat))
            {
                MessageBox.Show("Please provide valid habitat.");
                ok = false;
            }

            if (!int.TryParse(txtCategoryData2.Text, out teethCount))
            {
                MessageBox.Show("Please privide valid number for teeth count.");
                ok = false;
            }

            return (habitat, teethCount);
        }


        /// <summary>
        /// Read input specific to the bird category.
        /// </summary>
        /// <returns></returns>
        private (double, double) ReadBirdInput(out bool ok)
        {
            double wingSpan = 0.0;
            double flightSpeed = 0.0;

            ok = true;

            if (!double.TryParse(txtCategoryData1.Text.Trim(), out wingSpan))
            {
                MessageBox.Show("Please provide valid number for wing span.");
                ok = false;
            }

            if (!double.TryParse(txtCategoryData2.Text.Trim(), out flightSpeed))
            {
                MessageBox.Show("Please provide valid number for flight speed.");
                ok = false;
            }

            return (wingSpan, flightSpeed);
        }


        /// <summary>
        /// Read input specific to the fish category.
        /// </summary>
        /// <returns></returns>
        private (string, int) ReadFishInput(out bool ok)
        {
            string waterType = txtCategoryData1.Text.Trim();
            int finCount = 0;

            ok = true;

            if (string.IsNullOrEmpty(waterType))
            {
                MessageBox.Show("Please provide valid water type.");
                ok = false;
            }

            if (!int.TryParse(txtCategoryData2.Text.Trim(), out finCount))
            {
                MessageBox.Show("Please provide a valid number for fin count.");
                ok = false;
            }

            return (waterType, finCount);
        }


        //3. This part is for creating the object

        private Animal CreateMammal()
        {
            Animal animal = null;

            string habitat = string.Empty;
            int teethCount = 0;

            bool okMammal = false;
            (habitat, teethCount) = ReadMammalInput(out okMammal);  //Save the values from mammal input

            MammalSpecies species = (MammalSpecies)lbxSpecies.SelectedItem;

            //Create an object depending on the selected species.
            switch (species)
            {
                case MammalSpecies.Cat:
                    bool okBreed = false;
                    bool okFurColor = false;
                    string breed = ReadString(txtSpeciesData1.Text.Trim(), out okBreed);
                    string furColor = ReadString(txtSpeciesData2.Text.Trim(), out okFurColor);
                    if (okMammal && okBreed && okFurColor)
                    {
                        animal = new Cat(habitat, teethCount); //Create an object of the selected species by calling the base class constructor to save the values in the baseclass.
                        ((Cat)animal).Breed = breed; //Cast the animal object to Cat and read and save the input from the species specifications GroupBox.
                        ((Cat)animal).FurColor = furColor;
                    }
                    break;
                case MammalSpecies.Otter:
                    bool okDiveDepth = false;
                    bool okBodyLength = false;
                    double diveDepth = ReadDouble(txtSpeciesData1.Text.Trim(), out okDiveDepth);
                    double bodyLength = ReadDouble(txtSpeciesData2.Text.Trim(), out okBodyLength);
                    if (okMammal && okDiveDepth && okBodyLength)
                    {
                        animal = new Otter(habitat, teethCount);
                        ((Otter)animal).DiveDepth = diveDepth;
                        ((Otter)animal).BodyLength = bodyLength;
                    }
                    break;
                case MammalSpecies.Tiger:
                    bool okLand = false;
                    bool okStatus = false;
                    string land = ReadString(txtSpeciesData1.Text.Trim(), out okLand);
                    string status = ReadString(txtSpeciesData2.Text.Trim(), out okStatus);
                    if (okMammal && okLand && okStatus)
                    {
                        animal = new Tiger(habitat, teethCount);
                        ((Tiger)animal).LandOfOrigin = land;
                        ((Tiger)animal).ConservationStatus = status;
                    }
                    break;

            }

            return animal;
        }

        private Animal CreateBird()
        {
            Animal animal = null;

            double wingSpan = 0.0;
            double flightSpeed = 0.0;

            bool okBird = false;
            (wingSpan, flightSpeed) = ReadBirdInput(out okBird);
            BirdSpecies species = (BirdSpecies)lbxSpecies.SelectedItem;

            switch (species)
            {
                case BirdSpecies.Falcon:
                    bool okSeason = false;
                    bool okBeakLength = false;
                    string season = ReadString(txtSpeciesData1.Text.Trim(), out okSeason);
                    double beakLength = ReadDouble(txtSpeciesData2.Text.Trim(), out okBeakLength);
                    if (okBird && okSeason && okBeakLength)
                    {
                        animal = new Falcon(wingSpan, flightSpeed);
                        ((Falcon)animal).NestingSeason = season;
                        ((Falcon)animal).BeakLength = beakLength;
                    }
                    break;
                case BirdSpecies.Stork:
                    bool okMonths = false;
                    bool okNestSize = false;
                    string months = ReadString(txtSpeciesData1.Text.Trim(), out okMonths);
                    double nestSize = ReadDouble(txtSpeciesData2.Text.Trim(), out okNestSize);
                    if (okBird && okMonths && okNestSize)
                    {
                        animal = new Stork(wingSpan, flightSpeed);
                        ((Stork)animal).MigrationMonths = months;
                        ((Stork)animal).NestSize = nestSize;
                    }
                    break;
            }

            return animal;
        }

        private Animal CreateFish()
        {
            Animal animal = null;

            string waterType = string.Empty;
            int finCount = 0;

            bool okFish = false;
            (waterType, finCount) = ReadFishInput(out okFish);
            FishSpecies species = (FishSpecies)lbxSpecies.SelectedItem;

            switch (species)
            {
                case FishSpecies.Goldfish:
                    bool okTemp = false;
                    bool okColor = false;
                    double temp = ReadDouble(txtSpeciesData1.Text.Trim(), out okTemp);
                    string color = ReadString(txtSpeciesData2.Text.Trim(), out okColor);
                    if (okFish && okTemp && okColor)
                    {
                        animal = new Goldfish(waterType, finCount);
                        ((Goldfish)animal).WaterTeperature = temp;
                        ((Goldfish)animal).Color = color;
                    }
                    break;
                case FishSpecies.White_shark:
                    bool okDepth = false;
                    bool okSpeed = false;
                    double depth = ReadDouble(txtSpeciesData1.Text.Trim(), out okDepth);
                    double speed = ReadDouble(txtSpeciesData2.Text.Trim(), out okSpeed);
                    if (okFish && okDepth && okSpeed)
                    {
                        animal = new WhiteShark(waterType, finCount);
                        ((WhiteShark)animal).SwimmingDepth = depth;
                        ((WhiteShark)animal).SwimmingSpeed = speed;
                    }
                    break;
                case FishSpecies.Orange_clownfish:
                    bool okSize = false;
                    bool okEggs = false;
                    double size = ReadDouble(txtSpeciesData1.Text.Trim(), out okSize);
                    int eggs = ReadInt(txtSpeciesData2.Text.Trim(), out okEggs);
                    if (okFish && okSize && okEggs)
                    {
                        animal = new OrangeClownfish(waterType, finCount);
                        ((OrangeClownfish)animal).SizeAtMaturity = size;
                        ((OrangeClownfish)animal).EggsLayed = eggs;
                    }
                    break;
            }

            return animal;
        }


        /// <summary>
        /// Creates an animal object based on the selected category in the listbox.
        /// </summary>
        /// <returns></returns>
        private Animal CreateAnimal()
        {
            Category category = (Category)lbxCategoryType.SelectedItem;

            Animal animal = null;

            //Use a switch statement to create an object based on the selected category.
            switch (category)
            {
                case Category.Mammal:
                    animal = CreateMammal();
                    break;
                case Category.Bird:
                    animal = CreateBird();
                    break;
                case Category.Fish:
                    animal = CreateFish();
                    break;
            }

            //If the object is created, read the common values for animal (name, age, gender)
            if (animal != null)
            {
                bool ok = ReadCommonValues(animal);
                if (ok)
                    return animal;
            }

            return animal;
        }

        /// <summary>
        /// Empty the input fields to get the UI ready for the next object.
        /// </summary>
        private void UpdateGUI()
        {
            txtName.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtCategoryData1.Text = string.Empty;
            txtCategoryData2.Text = string.Empty;
            txtSpeciesData1.Text = string.Empty;
            txtSpeciesData2.Text = string.Empty;
            cmbGender.SelectedItem = Gender.Unknown;
            listViewAnimals.ItemsSource = null;
            listViewAnimals.ItemsSource = manager.List;
        }



        /// <summary>
        /// Create an object when the Add animal button is clicked and clear the textboxes. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAnimal_Click(object sender, EventArgs e)
        {
            Animal animal = CreateAnimal();
            bool added = manager.AddAnimal(animal);
            if (added)
            {
                UpdateGUI();
                isSaved = false;
            }
            else
                MessageBox.Show("Provide valid numerical or textual input");
        }

        private void listViewAnimals_SelectionChanged(object sender, EventArgs e)
        {
            int selectedIndex = listViewAnimals.SelectedIndex;
            if (selectedIndex == -1)
                return;

            Animal animal = manager.GetAt(selectedIndex);

            lbxFoodSchedule.ItemsSource = null;
            DisplayFoodItems(animal);

            txbRegisteredAnimal.Text = string.Empty;
            txbRegisteredAnimal.Text = animal.GetExtraInfo();
        }

        /// <summary>
        /// Looks for the selected animal in the list of values and adds the key(food item) to the list right of the animal list.
        /// </summary>
        /// <param name="animal"></param>
        private void DisplayFoodItems (Animal animal)
        {
            lbxFoodSchedule.Items.Clear();
            if (animal != null)
            {
                foreach (KeyValuePair<FoodItem, List<Animal>> kvp in manager.AnimalFood)
                {
                    if (kvp.Value.Contains(animal))
                        lbxFoodSchedule.Items.Add(kvp.Key.ToString());
                }
            }
            else
                MessageBox.Show("Please select an animal from the list.");
        }


        private void rbtnSortByName_Checked(object sender, EventArgs e)
        {
            manager.SortByName();
            listViewAnimals.SelectedIndex = -1;
            listViewAnimals.ItemsSource = null;
            listViewAnimals.ItemsSource = manager.List;
        }

        private void rbtnSortByCategory_Checked(object sender, EventArgs e)
        {
            manager.SortByCategory();
            listViewAnimals.SelectedIndex = -1;
            listViewAnimals.ItemsSource = null;
            listViewAnimals.ItemsSource = manager.List;
        }


        /// <summary>
        /// Open the Food Items Window, add the FoodItem object to the list of FoodItems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFoodWindow_Click(object sender, EventArgs e)
        {
            FoodWindow foodWindow = new FoodWindow();
            bool? result = foodWindow.ShowDialog();

            if (result == true)
            {
                manager.FoodItems.Add(foodWindow.FoodItem);
                lbxFood.ItemsSource = null;
                lbxFood.ItemsSource = manager.FoodItems;
            }
        }

        /// <summary>
        /// The selected animal is changed according to the new input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChange_Click(object sender, EventArgs e)
        {
            int selected = listViewAnimals.SelectedIndex;
            Animal animal = manager.GetAt(selected);
            Animal changedAnimal = CreateAnimal();
            if ((changedAnimal != null) && (selected >= 0)) 
            {
                bool ok = manager.ChangeAt(selected, changedAnimal);
                bool okID = manager.PreserveAnimalID(changedAnimal, selected);
                if (ok && okID)
                {
                    UpdateGUI();
                    listViewAnimals.SelectedIndex = selected;
                    isSaved = false;
                }
                else
                    MessageBox.Show("Changing was unsuccessfull.");
            }

        }

        /// <summary>
        /// The selected animal is removed from the animal list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selected = listViewAnimals.SelectedIndex;
            bool ok = manager.RemoveAt(selected);
            if (ok)
            {
                UpdateGUI();
                isSaved = false;
            }
            else
                MessageBox.Show("Deleting was unsuccessful.");
        } 

        /// <summary>
        /// Adds the selected food item as a key in the dictionary and adds the selected animal to the list of values.
        /// Then displays the food items that the selected animal is connected to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnimalAndFood_Click(object sender, EventArgs e)
        {
            FoodItem foodItem = lbxFood.SelectedItem as FoodItem;
            Animal animal = listViewAnimals.SelectedItem as Animal;

            try
            {
                manager.AddKeyValuePair(foodItem, animal);
                DisplayFoodItems(animal);
            }
            catch (FoodItemNotAssignedException ex)
            {
                ShowErrorMessage(ex);
            }
            catch (Exception ex) 
            {
                ShowErrorMessage(ex);
            }
           
        }

        /// <summary>
        /// The application is reinitialized, but first it prompts the user to save unsaved changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                MessageBoxResult result = MessageBox.Show(
                    "You have unsaved changes. Do you want to continue without saving?",
                    "Confirm",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                    return;
            }

            manager.RemoveAll();
            manager.FoodItems.Clear();
            lbxFood.ItemsSource = null;
            txbRegisteredAnimal.Text = string.Empty;
            lbxFoodSchedule.Items.Clear();
            filePath = string.Empty;
            currentFormat = string.Empty;
            isSaved = true;
            UpdateGUI();

        }

        /// <summary>
        /// An error message is displayed when catching an exception, the message depends on the exception that is being thrown.
        /// </summary>
        /// <param name="ex"></param>
        private void ShowErrorMessage(Exception ex)
        {
            MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Reads the data from text file and displays it on the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openTxtFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt) | *.txt";

            if(openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                currentFormat = "text";

                try
                {
                    manager = FileHandler.ReadFromTextFile(filePath);
                }
                catch(Exception ex)
                {
                    ShowErrorMessage(ex);
                }
              
                isSaved = true;
                UpdateGUI();
            }

        }

        /// <summary>
        /// Reads the data from a JSON file and displays it on the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openJson_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";

            if(openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                currentFormat = "JSON";

                try
                {
                    manager = FileHandler.ReadFromJson(filePath);
                    isSaved = true;
                    UpdateGUI();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex);
                }

            }
        }

        /// <summary>
        /// The user gets to choose between text or JSON file, the currentFormat is changed based on the user's choice
        /// and a proper method from FileHandler is called.
        /// </summary>
        private void SaveDataAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|JSON files (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                filePath = saveFileDialog.FileName;

                if (filePath.EndsWith(".txt"))
                {
                    currentFormat = "text";

                    try
                    {
                        FileHandler.SaveToTextFile(filePath, manager);
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage(ex);
                    }
                  
                    isSaved = true;
                }
                else if (filePath.EndsWith(".json"))
                {
                    currentFormat = "JSON";

                    try
                    {
                        FileHandler.SaveToJson(filePath, manager);
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage(ex);
                    }
                   
                    isSaved = true;
                }
            }

        }


        /// <summary>
        /// Checks the file format that was used last before clicking on Save and saves the data in that format. 
        /// SaveDataAs() method hanldes the situation if there is no file path i.e., no previously saved data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                SaveDataAs(); 
            }
            else
            {
                if (currentFormat == "text")
                {
                    try
                    {
                        FileHandler.SaveToTextFile(filePath, manager);
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage(ex);
                    }
                  
                    isSaved = true;
                }
                else if (currentFormat == "JSON")
                {
                    try
                    {
                        FileHandler.SaveToJson(filePath, manager);
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage(ex);
                    }
                  
                    isSaved = true;
                }
            }
        }

        /// <summary>
        /// Saves the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsTxtFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                filePath = saveFileDialog.FileName;
                currentFormat = "text";

                try
                {
                    FileHandler.SaveToTextFile(filePath, manager);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex);
                }

                isSaved = true;
            } 
        }

      
        private void saveAsJson_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                filePath = saveFileDialog.FileName;
                currentFormat = "JSON";

                try
                {
                    FileHandler.SaveToJson(filePath, manager);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex);
                }
               
                isSaved = true;   
            }
        }


        /// <summary>
        /// If all changes are saved the application terminates directly after clicking Exit, otherwise it prompts
        /// the user if the want to continue without saving the changes or go back and save them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileExit_Click(object sender, RoutedEventArgs e)
        {
            if (!isSaved)
            {
                MessageBoxResult result = MessageBox.Show(
                    "You have unsaved changes. Do you want to exit without saving?",
                    "Unsaved data",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
                else return;
            }
            else
            {
                // No unsaved changes — safe to close directly
                Application.Current.Shutdown();
            }

        }

      
        private void xmlSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    FileHandler.ExportToXml(filePath, manager.FoodItems);
                }
                catch (Exception ex) 
                {
                    ShowErrorMessage(ex);
                }
            }
        }

        private void xmlOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";

            if(openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    List<FoodItem> foodItems = FileHandler.LoadFromXml(filePath);
                    manager.FoodItems = foodItems;
                    lbxFood.ItemsSource = null;
                    lbxFood.ItemsSource = manager.FoodItems;
                }
                catch(Exception ex)
                {
                    ShowErrorMessage(ex);
                }
            }
        }



    }
}


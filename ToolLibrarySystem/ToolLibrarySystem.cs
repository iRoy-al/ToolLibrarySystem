using System;
using System.Collections.Generic;
using System.Text;

namespace ToolLibrarySystem
{
    public class ToolLibrarySystem : iToolLibrarySystem
    {
        private ToolCollection[][] toolLibrary;
        private MemberCollection members;
        private string[] toolCategoryNames;
        private string[][] toolTypeNames;
        private ToolCollection borrowedTools;

        public ToolLibrarySystem(MemberCollection members)
        {
            InitialiseToolCategory();
            InitialiseToolType();
            this.members = members;
            int size = 5;
            borrowedTools = new ToolCollection(size);
            PrePopulate();
        }

        // add a new tool to the system
        public void add(Tool tool)
        {
            Console.Clear();
            int categoryNo = -1;
            SelectToolCategories(ref categoryNo);
            if (categoryNo != 0)
            {
                int typeNo = -1;
                Console.Clear();
                SelectToolTypes(categoryNo - 1, ref typeNo);
                if (typeNo != 0)
                {
                    if (!toolLibrary[categoryNo - 1][typeNo - 1].search(tool))
                    {
                        toolLibrary[categoryNo - 1][typeNo - 1].add(tool);
                        Console.WriteLine("Tool successfully added to library\n");
                        Console.Write("Hit any key to continue");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Error: Tool already in the library\n");
                        Console.Write("Hit any key to continue");
                        Console.ReadKey();
                    }
                }
            }
        }

        //add new pieces of an existing tool to the system
        public void add(Tool tool, int quantity)
        {
            Console.Clear();
            int categoryNo = -1;
            SelectToolCategories(ref categoryNo);
            if (categoryNo != 0)
            {
                int typeNo = -1;
                Console.Clear();
                SelectToolTypes(categoryNo - 1, ref typeNo);
                if (typeNo != 0)
                {
                    int toolNo = -1;
                    Console.Clear();
                    SelectTool(categoryNo - 1, typeNo - 1, ref toolNo);
                    if (toolNo != 0)
                    {
                        bool repeat = true;
                        while (repeat)
                        {
                            Console.Write("Enter the quantity to add into the library - ");
                            if (Int32.TryParse(Console.ReadLine().Replace(" ", ""), out quantity))
                            {
                                if (quantity >= 0)
                                {
                                    toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].Quantity += quantity;
                                    toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].AvailableQuantity += quantity;
                                    Console.WriteLine($"Updated the quantity of " +
                                        $"{toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].Name}" +
                                        $" in the library to {toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].Quantity}\n");
                                    Console.Write("Hit any key to continue");
                                    Console.ReadKey();
                                    repeat = false;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input: Please enter a value >= 0");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input: Please enter a value >= 0");
                            }
                        }
                    }
                }
            }
        }

        //delete a given tool from the system
        public void delete(Tool tool)
        {
            for (int i = 0; i < toolLibrary.Length; i++)
            {
                for (int j = 0; j < toolLibrary[i].Length; j++)
                {
                    for (int k = 0; k < toolLibrary[i][j].toArray().Length; k++)
                    {
                        if (tool.Name == toolLibrary[i][j].toArray()[k].Name)
                        {
                            toolLibrary[i][j].delete(tool);
                        }
                    }
                }
            }
        }

        //remove some pieces of a tool from the system
        public void delete(Tool tool, int quantity)
        {
            Console.Clear();
            int categoryNo = -1;
            SelectToolCategories(ref categoryNo);
            if (categoryNo != 0)
            {
                int typeNo = -1;
                Console.Clear();
                SelectToolTypes(categoryNo - 1, ref typeNo);
                if (typeNo != 0)
                {
                    int toolNo = -1;
                    Console.Clear();
                    SelectTool(categoryNo - 1, typeNo - 1, ref toolNo);
                    if (toolNo != 0)
                    {
                        bool repeat = true;
                        while (repeat)
                        {
                            Console.Write("Enter the quantity to remove from the library - ");
                            if (Int32.TryParse(Console.ReadLine().Replace(" ", ""), out quantity))
                            {
                                if (quantity >= 0)
                                {
                                    if (toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].AvailableQuantity - quantity >= 0)
                                    {
                                        toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].Quantity -= quantity;
                                        toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].AvailableQuantity -= quantity;
                                        Console.WriteLine($"Updated the quantity of " +
                                            $"{toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].Name}" +
                                            $" in the library to {toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].Quantity}\n");
                                        Console.Write("Hit any key to continue");
                                        Console.ReadKey();
                                        repeat = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Cannot remove more than what is available.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input: Please enter a value >= 0");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input: Please enter a value >= 0");
                            }
                        }
                    }
                }
            }
        }

        //add a new member to the system
        public void add(Member member)
        {
            members.add(member);
            Console.WriteLine($"Added {member.FirstName} {member.LastName} successfully as a new member\n");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        //delete a member from the system
        public void delete(Member member)
        {
            bool delete = false;
            // determines if the member still has any tools borrowed
            // if they don't then they are allowed to be deleted
            for (int i = 0; i < members.toArray().Length; i++)
            {
                if (member.CompareTo(members.toArray()[i]) == 0)
                {
                    if (members.toArray()[i].Borrowed == 0)
                    {
                        delete = true;
                    }
                }
            }

            if (delete)
            {
                members.delete(member);
                Console.WriteLine($"Successfully deleted {member.FirstName} {member.LastName} from the system\n");
            }
            else
            {
                Console.WriteLine("Sorry, cannot delete members with outstanding tools");
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        //get a list of tools that are currently held by a given member
        public string[] listTools(Member member)
        {
            return member.Tools;
        }

        // display all the tools of a tool type selected by a member
        public void displayTools(string aToolType)
        {
            Console.Clear();
            Console.WriteLine("Tool Library System - Display Tools by Type");
            Console.WriteLine("===========================================\n");
            int categoryNo = -1;
            SelectToolCategories(ref categoryNo);
            if (categoryNo != 0)
            {
                int typeNo = -1;
                SelectToolTypes(categoryNo - 1, ref typeNo);
                if (typeNo != 0)
                {
                    Console.WriteLine("Tool Type List of Tools");
                    Console.WriteLine("================================================================================");
                    Console.WriteLine("               Tool Name                               Available        Total");
                    Console.WriteLine("================================================================================");
                    for (int i = 0; i < toolLibrary[categoryNo - 1][typeNo - 1].Number; i++)
                    {
                        Console.WriteLine(String.Format("{0}. {1, -25} {2, 32} {3, 14}", (i + 1),
                            toolLibrary[categoryNo - 1][typeNo - 1].toArray()[i].Name,
                            toolLibrary[categoryNo - 1][typeNo - 1].toArray()[i].AvailableQuantity,
                            toolLibrary[categoryNo - 1][typeNo - 1].toArray()[i].Quantity));
                    }
                    Console.WriteLine(" 0. Exit");
                    Console.WriteLine("================================================================================");
                    Console.Write("\nHit any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        //a member borrows a tool from the tool library
        public void borrowTool(Member member, Tool tool)
        {
            Console.Clear();
            Console.WriteLine("Tool Library System - Borrow Tool from Tool Library");
            Console.WriteLine("===================================================\n");
            int categoryNo = -1;
            SelectToolCategories(ref categoryNo);
            if (categoryNo != 0)
            {
                int typeNo = -1;
                SelectToolTypes(categoryNo - 1, ref typeNo);
                if (typeNo != 0)
                {
                    int toolNo = -1;
                    Console.Clear();
                    SelectTool(categoryNo - 1, typeNo - 1, ref toolNo);
                    if (toolNo != 0)
                    {
                        if (member.Borrowed != 3)
                        {
                            if (toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].AvailableQuantity != 0)
                            {
                                member.addTool(toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1]);
                                toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].addBorrower(member);

                                if (!borrowedTools.search(toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1]))
                                {
                                    borrowedTools.add(toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1]);
                                }
                                Console.WriteLine($"Borrowed " +
                                    $"{toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].Name} from the library");
                            }
                            else
                            {
                                Console.WriteLine($"Sorry, There aren't any " +
                                    $"{toolLibrary[categoryNo - 1][typeNo - 1].toArray()[toolNo - 1].Name} available right now");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, you can only borrow a maximum of 3 tools at a time");
                        }
                        Console.Write("\nHit any key to continue");
                        Console.ReadKey();
                    }
                }
            }
        }

        //a member return a tool to the tool library
        public void returnTool(Member member, Tool tool)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine($"Tool Library System - Return a Tool to the Library");
                Console.WriteLine("===================================================\n");
                for (int i = 0; i < member.Borrowed; i++)
                {
                    Console.WriteLine($"{i + 1}. {listTools(member)[i]}");
                }
                Console.WriteLine(" 0. Exit");
                Console.Write("\n Select the tool that you would like to return (0 to exit) - ");
                int toolNo = -1;
                Int32.TryParse(Console.ReadLine().Replace(" ", ""), out toolNo);
                if (toolNo >= 0 && toolNo <= member.Borrowed)
                {
                    if (toolNo > 0)
                    {
                        string toolName = listTools(member)[toolNo - 1];
                        for (int i = 0; i < borrowedTools.Number; i++)
                        {
                            if (toolName == borrowedTools.toArray()[i].Name)
                            {
                                borrowedTools.toArray()[i].deleteBorrower(member);
                                member.deleteTool(borrowedTools.toArray()[i]);
                                Console.WriteLine($"{borrowedTools.toArray()[i].Name} was returned to the library");
                                Console.Write("\nHit any key to continue");
                                Console.ReadKey();
                            }
                        }
                    }
                    exit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Invalid Selection. Please enter a value between " +
                        $"0-{member.Borrowed}");
                }
            }
            Console.Clear();
        }

        //given a member, display all the tools that the member are currently renting
        public void displayBorrowingTools(Member member)
        {
            Console.Clear();
            Console.WriteLine($"Tool Library System - Tools on Loan to {member.FirstName} {member.LastName}");
            Console.WriteLine("======================================================================\n");
            for (int i = 0; i < member.Borrowed; i++)
            {
                Console.WriteLine($"{i + 1}. {listTools(member)[i]}");
            }
            Console.Write("\nHit any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        //Display top three most frequently borrowed tools by the members 
        //in the descending order by the number of times each tool has been borrowed.
        public void displayTopThree()
        {
            Console.Clear();
            Console.WriteLine("Tool Library System - Top 3 Most Borrowed Tools");
            Console.WriteLine("===============================================");
            if (borrowedTools.Number > 0)
            {
                Tool[] tools = borrowedTools.toArray();
                HeapSort(tools);
                int number = 0;
                int length = 0;
                if (borrowedTools.Number >= 3)
                {
                    length = (borrowedTools.Number - 1) - 3;
                }
                else
                {
                    length = -1;
                }

                for (int i = borrowedTools.Number - 1; i > length; i--)
                {
                    number++;
                    Console.WriteLine($"{number}. {tools[i].Name} --- Borrowed {tools[i].NoBorrowings} times");
                }
            }
            else
            {
                Console.WriteLine("No tools have been borrowed");
            }
            Console.Write("\n Hit any key to continue");
            Console.ReadLine();
        }

        // sort the elements in an array 
        private void HeapSort(Tool[] tools)
        {
            //Use the HeapBottomUp procedure to convert the array, data, into a heap
            HeapBottomUp(tools);


            //repeatly remove the maximum key from the heap and then rebuild the heap
            for (int i = 0; i <= borrowedTools.Number - 2; i++)
            {
                MaxKeyDelete(tools, borrowedTools.Number - i);
            }
        }

        // convert a complete binary tree into a heap
        private void HeapBottomUp(Tool[] tools)
        {
            int n = borrowedTools.Number;
            for (int i = (n - 1) / 2; i >= 0; i--)
            {
                int k = i;
                Tool v = tools[i];
                bool heap = false;
                while ((!heap) && ((2 * k + 1) <= (n - 1)))
                {
                    int j = 2 * k + 1;  //the left child of k
                    if (j < (n - 1))   //k has two children
                        if (tools[j].NoBorrowings < tools[j + 1].NoBorrowings)
                            j = j + 1;  //j is the larger child of k
                    if (v.NoBorrowings >= tools[j].NoBorrowings)
                        heap = true;
                    else
                    {
                        tools[k] = tools[j];
                        k = j;
                    }
                }
                tools[k] = v;
            }
        }

        //delete the maximum key and rebuild the heap
        private void MaxKeyDelete(Tool[] tools, int size)
        {
            //1. Exchange the root’s key with the last key K of the heap;
            Tool temp = tools[0];
            tools[0] = tools[size - 1];
            tools[size - 1] = temp;

            //2. Decrease the heap’s size by 1;
            int n = size - 1;

            //3. “Heapify” the complete binary tree.
            bool heap = false;
            int k = 0;
            Tool v = tools[0];
            while ((!heap) && ((2 * k + 1) <= (n - 1)))
            {
                int j = 2 * k + 1; //the left child of k
                if (j < (n - 1))   //k has two children
                    if (tools[j].NoBorrowings < tools[j + 1].NoBorrowings)
                        j = j + 1;  //j is the larger child of k
                if (v.NoBorrowings >= tools[j].NoBorrowings)
                    heap = true;
                else
                {
                    tools[k] = tools[j];
                    k = j;
                }
            }
            tools[k] = v;
        }

        //Prepopulates the tools in the garden category
        // also creates 3 members
        private void PrePopulate()
        {
            toolLibrary[0][0].add(new Tool("Trimmers of Trimming"));
            toolLibrary[0][0].add(new Tool("Trimmers of Trimming good"));
            toolLibrary[0][0].add(new Tool("Trimmers of Trimming best"));

            toolLibrary[0][1].add(new Tool("Generic Mower"));
            toolLibrary[0][1].add(new Tool("Extreme Mower"));
            toolLibrary[0][1].add(new Tool("Mow of the Gods"));

            toolLibrary[0][2].add(new Tool("Big Hand Tool"));
            toolLibrary[0][2].add(new Tool("Medium Hand Tool"));
            toolLibrary[0][2].add(new Tool("Small Hand Tool"));

            toolLibrary[0][3].add(new Tool("Water Wheelbarrow"));
            toolLibrary[0][3].add(new Tool("Earth Wheelbarrow"));
            toolLibrary[0][3].add(new Tool("Fire Wheelbarrow"));
            toolLibrary[0][3].add(new Tool("Air Wheelbarrow"));

            //increase quantity of most of the tools
            int quantity = 5;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    toolLibrary[0][i].toArray()[j].Quantity += quantity;
                    toolLibrary[0][i].toArray()[j].AvailableQuantity += quantity;
                }
            }

            members.add(new Member("ill", "b", "12354968", "bill"));
            members.add(new Member("ane", "j", "12354968", "jane"));
            members.add(new Member("ary", "m", "12354968", "mary"));
        }

        private void InitialiseToolCategory()
        {
            //Initialise Tool Category Jagged array
            toolLibrary = new ToolCollection[9][];
            toolLibrary[0] = new ToolCollection[5]; //gardening
            toolLibrary[1] = new ToolCollection[6]; //flooring
            toolLibrary[2] = new ToolCollection[5]; //fencing
            toolLibrary[3] = new ToolCollection[6]; //measuring
            toolLibrary[4] = new ToolCollection[6]; //cleaning
            toolLibrary[5] = new ToolCollection[6]; //painting
            toolLibrary[6] = new ToolCollection[5]; //electronic
            toolLibrary[7] = new ToolCollection[5]; //electricity
            toolLibrary[8] = new ToolCollection[6]; //automotive
            toolCategoryNames = new string[] { "Gardening Tools", "Flooring Tools", "Fencing Tools",
                    "Measuring Tools", "Cleaning Tools", "Painting Tools",
                    "Electronic Tools", "Electricity Tools", "Automotive Tools"};

            //Initialise Tool category
            int defaultSize = 5;
            for (int i = 0; i < 6; i++)
            {
                if (i < 5)
                {
                    toolLibrary[0][i] = new ToolCollection(defaultSize);
                    toolLibrary[2][i] = new ToolCollection(defaultSize);
                    toolLibrary[6][i] = new ToolCollection(defaultSize);
                    toolLibrary[7][i] = new ToolCollection(defaultSize);
                }
                toolLibrary[1][i] = new ToolCollection(defaultSize);
                toolLibrary[3][i] = new ToolCollection(defaultSize);
                toolLibrary[4][i] = new ToolCollection(defaultSize);
                toolLibrary[5][i] = new ToolCollection(defaultSize);
                toolLibrary[8][i] = new ToolCollection(defaultSize);
            }
        }

        private void InitialiseToolType()
        {
            //Initialise Tool Type Names Jagged array
            toolTypeNames = new string[9][];
            toolTypeNames[0] = new string[] { "Line Trimmers", "Lawn Mowers", "Hand Tools",
                "Wheelbarrows", "Garden Power Tools" }; //gardening
            toolTypeNames[1] = new string[] { "Scrapers", "Floor Lasers", "Floor Levelling Tools",
                "Floor Levelling Materials", "Floor Hand Tools", "Tiling Tools" }; //flooring
            toolTypeNames[2] = new string[] { "Hand Tools", "Electric Fencing", "Steel Fencing Tools",
                "Power Tools", "Fencing Accessories" }; //fencing
            toolTypeNames[3] = new string[] { "Distance Tools", "Laser Measurer", "Measuring Jugs",
                "Temperature & Humidity Tools", "Levelling Tools", "Markers" }; //measuring
            toolTypeNames[4] = new string[] { "Draining", "Car Cleaning", "Vacuum",
                "Pressure Cleaners", "Pool Cleaning", "Floor Cleaning" }; //cleaning
            toolTypeNames[5] = new string[] { "Sanding Tools", "Brushes", "Rollers",
                "Paint Removal Tools", "Paint Scrapers", "Sprayers" }; //painting
            toolTypeNames[6] = new string[] { "Voltage Tester", "Oscilloscopes", "Thermal Imaging",
                "Data Test Tool", "Insulation Testers" }; //electronic
            toolTypeNames[7] = new string[] { "Test Equipment", "Safety Equipment", "Basic Hand Tools",
                "Circuit Protection", "Cable Tools" }; //electricity
            toolTypeNames[8] = new string[] { "Jacks", "Air Compressors", "Battery Chargers",
                "Socket Tools", "Braking", "Drivetrain" }; //automotive
        }

        private void SelectToolCategories(ref int categoryNo)
        {
            //infinite loop so the only way to exit is to select 0 or select a category
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Select the category:");
                Console.WriteLine("====================");
                for (int i = 0; i < toolCategoryNames.Length; i++)
                {
                    Console.WriteLine($" {i + 1}. {toolCategoryNames[i]}");
                }
                Console.WriteLine(" 0. Exit");
                Console.Write("Select option from menu (0 to exit) - ");
                int categorySelect = -1;
                Int32.TryParse(Console.ReadLine().Replace(" ", ""), out categorySelect);
                if ((categorySelect <= toolCategoryNames.Length)
                        && (categorySelect >= 0))
                {
                    categoryNo = categorySelect;
                    exit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Selection. Please enter a value between 0-9");
                }
            }
        }

        private void SelectToolTypes(int categoryNo, ref int typeNo)
        {
            //infinite loop so the only way to exit is to select 0 or select a tool type
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Select the Tool Type:");
                Console.WriteLine("======================");
                for (int i = 0; i < toolTypeNames[categoryNo].Length; i++)
                {
                    Console.WriteLine($" {i + 1}. {toolTypeNames[categoryNo][i]}");
                }
                Console.WriteLine(" 0. Exit");
                Console.Write("Select option from menu (0 to exit) - ");
                int typeSelect = -1;
                Int32.TryParse(Console.ReadLine().Replace(" ", ""), out typeSelect);

                if ((typeSelect <= toolTypeNames[categoryNo].Length)
                        && (typeSelect >= 0))
                {
                    typeNo = typeSelect;
                    exit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Invalid Selection. Please enter a value between " +
                        $"0-{toolTypeNames[categoryNo].Length}");
                }
            }
        }

        private void SelectTool(int categoryNo, int typeNo, ref int toolNo)
        {
            //infinite loop so the only way to exit is to select 0 or select a tool
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Tool Type List of Tools");
                Console.WriteLine("================================================================================");
                Console.WriteLine("               Tool Name                               Available        Total");
                Console.WriteLine("================================================================================");
                for (int i = 0; i < toolLibrary[categoryNo][typeNo].Number; i++)
                {
                    Console.WriteLine(String.Format("{0}. {1, -25} {2, 32} {3, 14}", (i + 1),
                        toolLibrary[categoryNo][typeNo].toArray()[i].Name,
                        toolLibrary[categoryNo][typeNo].toArray()[i].AvailableQuantity,
                        toolLibrary[categoryNo][typeNo].toArray()[i].Quantity));
                }
                Console.WriteLine(" 0. Exit");
                Console.WriteLine("================================================================================");
                Console.Write("Select option from menu (0 to exit) - ");
                int toolSelect = -1;
                Int32.TryParse(Console.ReadLine().Replace(" ", ""), out toolSelect);
                if ((toolSelect <= toolLibrary[categoryNo][typeNo].Number)
                        && (toolSelect >= 0))
                {
                    toolNo = toolSelect;
                    exit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Invalid Selection. Please enter a value between " +
                        $"0-{toolLibrary[categoryNo][typeNo].Number}");
                }
            }
        }
    }
}

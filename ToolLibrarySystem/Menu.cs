using System;
using System.Collections.Generic;
using System.Text;

namespace ToolLibrarySystem
{
    public class Menu
    {
        private ToolLibrarySystem system;
        private MemberCollection members;

        public Menu()
        {
            members = new MemberCollection();
            system = new ToolLibrarySystem(members);
            MainMenu();
        }

        public void MainMenu()
        {
            //Infinite loop so only way to exit is 0 or select something
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Tool Library!");
                Console.WriteLine("===========Main Menu===========");
                Console.WriteLine("1. Staff Login");
                Console.WriteLine("2. Member Login");
                Console.WriteLine("0. Exit");
                Console.WriteLine("===============================");
                Console.WriteLine();
                Console.Write("Please make a selection (1-2, or 0 to exit): ");
                string selection = Console.ReadLine().Replace(" ", "");
                switch (selection)
                {
                    case "1":
                        Console.Clear();
                        StaffLogin();
                        break;
                    case "2":
                        Console.Clear();
                        MemberLogin();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Selection. Please enter a value between 0-2");
                        Console.Write("\nHit any key to continue");
                        Console.ReadKey();
                        break;
                }
            }

        }

        public void StaffLogin()
        {
            Console.Clear();
            Console.WriteLine("Tool Library System - Staff Login Page");
            Console.WriteLine("======================================\n\n");
            Console.Write("Enter staff login - ");
            string staffUser = Console.ReadLine();
            Console.Write("Enter staff password - ");
            string staffPass = Console.ReadLine();

            if (staffUser == "staff" && staffPass == "today123")
            {
                Console.Clear();
                StaffMenu();
            }
            else
            {
                Console.WriteLine("Invalid Username or Password!");
                Console.Write("\nHit any key to continue");
                Console.ReadKey();
            }
        }

        public void MemberLogin()
        {
            Console.Clear();
            Console.WriteLine("Tool Library System - Member Login Page");
            Console.WriteLine("=======================================\n\n");
            Console.Write("Enter member login - ");
            string enterUser = Console.ReadLine();
            Console.Write("Enter member password - ");
            string enterPass = Console.ReadLine();

            bool correctInfo = false;
            int memberIndex = -1;
            for (int i = 0; i < members.toArray().Length; i++)
            {
                string memberUser = members.toArray()[i].LastName + members.toArray()[i].FirstName;
                string memberPass = members.toArray()[i].PIN;
                if (enterUser == memberUser && enterPass == memberPass)
                {
                    correctInfo = true;
                    memberIndex = i;
                }
            }
            if (correctInfo)
            {
                Console.Clear();
                MemberMenu(members.toArray()[memberIndex]);
            }
            else
            {
                Console.WriteLine("Invalid Username or Password!");
                Console.Write("\nHit any key to continue");
                Console.ReadKey();
            }
        }

        public void StaffMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Tool Library!");
                Console.WriteLine("================Staff Menu================");
                Console.WriteLine("1. Add a new tool");
                Console.WriteLine("2. Add new pieces of an existing tool");
                Console.WriteLine("3. Remove some pieces of a tool");
                Console.WriteLine("4. Register a new member");
                Console.WriteLine("5. Remove a member");
                Console.WriteLine("6. Find the contact number of a member");
                Console.WriteLine("0. Return to main menu");
                Console.WriteLine("==========================================");
                Console.WriteLine();
                Console.Write("Please make a selection (1-6, or 0 to return to the main menu): ");
                string selection = Console.ReadLine().Replace(" ", "");
                switch (selection)
                {
                    case "1":
                        AddNewTool();
                        break;
                    case "2":
                        system.add(new Tool("placeholder"), -1);
                        break;
                    case "3":
                        system.delete(new Tool("placeholder"), -1);
                        break;
                    case "4":
                        RegisterNewMember();
                        break;
                    case "5":
                        RemoveMember();
                        break;
                    case "6":
                        FindMemberContactNo();
                        break;
                    case "0":
                        Console.Clear();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Selection. Please enter a value between 0-6");
                        Console.Write("\nHit any key to continue");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public void MemberMenu(Member member)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Tool Library!");
                Console.WriteLine("===============Member Menu===============");
                Console.WriteLine("1. Display all the tools of a tool type");
                Console.WriteLine("2. Borrow a tool");
                Console.WriteLine("3. Return a tool");
                Console.WriteLine("4. List all the tools that I am renting");
                Console.WriteLine("5. Display top three (3) most frequently rented tools");
                Console.WriteLine("0. Return to main menu");
                Console.WriteLine("=========================================");
                Console.WriteLine();
                Console.Write("Please make a selection (1-5, or 0 to return to the main menu): ");
                string selection = Console.ReadLine().Replace(" ", "");
                switch (selection)
                {
                    case "1":
                        system.displayTools("tooltype");
                        break;
                    case "2":
                        system.borrowTool(member, new Tool("placeholder"));
                        break;
                    case "3":
                        system.returnTool(member, new Tool("placeholder"));
                        break;
                    case "4":
                        system.displayBorrowingTools(member);
                        break;
                    case "5":
                        system.displayTopThree();
                        break;
                    case "0":
                        Console.Clear();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Selection. Please enter a value between 0-5");
                        Console.Write("\nHit any key to continue");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AddNewTool()
        {
            Console.Clear();
            Console.WriteLine("Tool Library System - Add New Tool To Library");
            Console.WriteLine("=============================================\n");
            Console.Write("Enter the name of the new Tool (0 to exit) - ");
            string toolName = Console.ReadLine();
            if (toolName != "0")
            {
                Tool newTool = new Tool(toolName);
                system.add(newTool);
            }
        }

        private void RegisterNewMember()
        {
            Console.Clear();
            Console.WriteLine("Tool Library System - Register New Member with Library");
            Console.WriteLine("======================================================\n");
            Console.Write("Enter the first name of the new member (0 to exit) - ");
            string firstName = Console.ReadLine().Replace(" ", "");
            if (firstName != "0")
            {
                Console.Write("Enter the last name of the new member (0 to exit) - ");
                string lastName = Console.ReadLine().Replace(" ", "");
                if (lastName != "0")
                {
                    Console.Write("Enter the mobile number of the new member (0 to exit) - ");
                    string mobileNo = Console.ReadLine().Replace(" ", "");
                    if (mobileNo != "0")
                    {
                        Console.Write("Enter the PIN (0 to exit) - ");
                        string PIN = Console.ReadLine().Replace(" ", "");
                        if (PIN != "0")
                        {
                            Member newMember = new Member(firstName, lastName, mobileNo, PIN);
                            system.add(newMember);
                        }
                    }
                }
            }
            Console.Clear();
        }

        private void RemoveMember()
        {
            Console.Clear();
            Console.WriteLine("Tool Library System - Remove Member with Library");
            Console.WriteLine("================================================\n");
            Console.Write("Enter the first name of the member (0 to exit) - ");
            string firstName = Console.ReadLine().Replace(" ", "");
            if (firstName != "0")
            {
                Console.Write("Enter the last name of the member (0 to exit) - ");
                string lastName = Console.ReadLine().Replace(" ", "");
                if (lastName != "0")
                {
                    string mobileNo = "0000"; // placeholders
                    string PIN = "0000";
                    Member newMember = new Member(firstName, lastName, mobileNo, PIN);
                    system.delete(newMember);
                }
            }
            Console.Clear();
        }

        private void FindMemberContactNo()
        {
            Console.Clear();
            Console.WriteLine("Tool Library System - Find Member Contact Number");
            Console.WriteLine("================================================\n");
            Console.Write("Enter the first name of the member (0 to exit) - ");
            string firstName = Console.ReadLine().Replace(" ", "");
            if (firstName != "0")
            {
                Console.Write("Enter the last name of the member (0 to exit) - ");
                string lastName = Console.ReadLine().Replace(" ", "");
                if (lastName != "0")
                {
                    string mobileNo = "0000";
                    string PIN = "0000";
                    Member member = new Member(firstName, lastName, mobileNo, PIN);
                    FindMemberContactNo(member);
                }
            }
            Console.Clear();
        }

        private void FindMemberContactNo(Member member)
        {
            bool found = false;
            for (int i = 0; i < members.toArray().Length; i++)
            {
                if (member.CompareTo(members.toArray()[i]) == 0)
                {
                    found = true;
                    Console.WriteLine($"The contact number of {member.FirstName} {member.LastName} " +
                        $"is {members.toArray()[i].ContactNumber}\n");
                }
            }
            if (!found)
            {
                Console.WriteLine($"Sorry, we cannot find {member.FirstName} {member.LastName} in the system\n");
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

    }
}

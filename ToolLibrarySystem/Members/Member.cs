using System;
using System.Collections.Generic;
using System.Text;

namespace ToolLibrarySystem
{
    public class Member : iMember
    {
        private string firstName;
        private string lastName;
        private string contactNumber;
        private string pin;
        private string[] toolsName;
        private ToolCollection tools;
        private int borrowed;
        private int maxBorrow;

        public Member(string firstName, string lastName, string contactNumber, string pin)
        {
            maxBorrow = 3;
            borrowed = 0;
            tools = new ToolCollection(maxBorrow);
            toolsName = new string[maxBorrow];
            this.firstName = firstName;
            this.lastName = lastName;
            this.contactNumber = contactNumber;
            this.pin = pin;
        }

        public string FirstName
        { get { return firstName; } set { this.firstName = value; } }
        public string LastName
        { get { return lastName; } set { this.lastName = value; } }
        public string ContactNumber
        { get { return contactNumber; } set { this.contactNumber = value; } }
        public string PIN
        { get { return pin; } set { this.pin = value; } }
        public int Borrowed
        { get { return borrowed; } }
        public string[] Tools
        { get { return toolsName; } }

        public void addTool(Tool tool)
        {
            // adds tool if max number or borrow hasnt been reached
            if (borrowed != maxBorrow)
            {
                tools.add(tool);
                toolsName[borrowed] = tool.Name;
                borrowed++;
            }
        }

        public void deleteTool(Tool tool)
        {
            //loops through the toolName array to check if the tool
            //is in the toolName array and if it is and its not the last
            //in the array, then the array shifts to the left 1 space.
            bool found = false;
            for (int i = 0; i < borrowed; i++)
            {
                if (tool.Name == toolsName[i])
                {
                    found = true;
                }
                if (found && i != borrowed - 1)
                {
                    toolsName[i] = toolsName[i + 1];
                }
            }
            if (found)
            {
                tools.delete(tool);
                borrowed--;
            }
        }

        public int CompareTo(Member other)
        {
            if (this.lastName.CompareTo(other.lastName) < 0)
            {
                return -1;
            }
            else if ((this.lastName.CompareTo(other.lastName) == 0) && (this.firstName.CompareTo(other.firstName) == 0))
            {
                return 0;
            }
            else if ((this.lastName.CompareTo(other.lastName) == 0) && (this.firstName.CompareTo(other.firstName) < 0))
            {
                return -1;
            }
            return 1;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {ContactNumber}";
        }
    }
}

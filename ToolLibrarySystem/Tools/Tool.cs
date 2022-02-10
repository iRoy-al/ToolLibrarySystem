using System;
using System.Collections.Generic;
using System.Text;

namespace ToolLibrarySystem
{
    public class Tool : iTool
    {
        private string name;
        private int quantity;
        private int availableQuantity;
        private int noBorrowings;
        private MemberCollection borrowers;

        public Tool(string Name)
        {
            int defaultQuantity = 1;
            name = Name;
            quantity = defaultQuantity;
            availableQuantity = defaultQuantity;
            noBorrowings = 0;
            borrowers = new MemberCollection();
        }

        public string Name
        { get { return name; } set { this.name = value; } }
        public int Quantity
        { get { return quantity; } set { this.quantity = value; } }
        public int AvailableQuantity
        { get { return availableQuantity; } set { this.availableQuantity = value; } }
        public int NoBorrowings
        { get { return noBorrowings; } set { this.noBorrowings = value; } }
        public MemberCollection GetBorrowers
        { get { return borrowers; } }

        public void addBorrower(Member member)
        {
            if (availableQuantity != 0)
            {
                borrowers.add(member);
                noBorrowings++;
                availableQuantity--;
            }
            else
            {
                Console.WriteLine($"Sorry, There aren't any {name} available right now");
            }
        }

        public void deleteBorrower(Member member)
        {
            if (GetBorrowers.search(member))
            {
                GetBorrowers.delete(member);
                availableQuantity++;
            }
        }

        public override string ToString()
        {
            return $"{name}({availableQuantity})";
        }
    }
}

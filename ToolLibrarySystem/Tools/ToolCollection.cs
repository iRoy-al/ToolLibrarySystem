using System;
using System.Collections.Generic;
using System.Text;

namespace ToolLibrarySystem
{
    public class ToolCollection : iToolCollection
    {
        private int number;
        private int size;
        private Tool[] tools;

        public ToolCollection(int size)
        {
            this.size = size;
            tools = new Tool[size];
            number = 0;
        }

        public int Number
        { get { return number; } }

        public void add(Tool tool)
        {
            //array resize if it gets full
            if (number == size)
            {
                size = size * 2;
                Tool[] temp = new Tool[size];
                for (int i = 0; i < number; i++)
                {
                    temp[i] = tools[i];
                }
                tools = temp;
            }
            tools[number] = tool;
            number++;
        }

        public void delete(Tool tool)
        {
            bool found = false;
            for (int i = 0; i < number; i++)
            {
                //looks for tool and shifts the entire array to overwrite it if found
                if (tools[i].Name == tool.Name)
                {
                    found = true;
                }
                if (found && i != number - 1)
                {
                    tools[i] = tools[i + 1];
                }
            }
            if (found)
            {
                number--;
            }
        }

        public bool search(Tool tool)
        {
            bool found = false;
            for (int i = 0; i < number; i++)
            {
                if (tools[i].Name == tool.Name)
                {
                    found = true;
                }
            }
            return found;
        }

        public Tool[] toArray()
        {
            return tools;
        }
    }
}
